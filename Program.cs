using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecouverteSQL
{/// <summary>
/// Connection BDD==>1.Chaine de connexion
///                  2.using SQL connection
///                  3.Open()
///                  4.Creat Command
///                  5.Requete
///                  6.command text
///                  7.Execute==>DataReader(execute et recupere)
///                           ==>NonQuery(modification et recuperation)
/// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            AfficheLesVendeur();
            //InsererUneLigneDeVendeur();
            
            UpdateVendeur();
            AfficheLesVendeur();
        }

        static void UpdateVendeur()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = Properties.Settings.Default.MaChaineDeConnexionPourKartina;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand()) //.createCommand==cmd.connection=maConnection
                {

                    Console.WriteLine("le Prenom?");
                    string prenomVendeur = Console.ReadLine();

                    Console.WriteLine("le Nom?");
                    string nomVendeur = Console.ReadLine();

                    Console.WriteLine("ID du vendeur a modifier? (entre 1 et 8 )");
                    int idVendeur = int.Parse(Console.ReadLine());

                    string sql = "UPDATE Vendeur SET FirstName = '" + prenomVendeur + "', LastName = '" + nomVendeur + "' WHERE Vendeur.id = " + idVendeur;

                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }
        }

            static void InsererUneLigneDeVendeur()
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = Properties.Settings.Default.MaChaineDeConnexionPourKartina;
                connection.Open();
                using (SqlCommand command = connection.CreateCommand()) //.createCommand==cmd.connection=maConnection
                {
                    Console.WriteLine("le Prenom?" );
                    string prenomVendeur = Console.ReadLine();

                    Console.WriteLine("le Nom?");
                    string nomVendeur = Console.ReadLine();

                    string sql = "INSERT INTO Vendeur (FirstName, LastName) VALUES ('"+prenomVendeur+"', '"+nomVendeur+"')";
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void AfficheLesVendeur()
        {
            //méme chose que le using pour la destruction de la variable apres utilisation
            //var masecondConnection = new SqlConnection();
            //masecondConnection.Dispose();

            using (var maConnection = new SqlConnection())
            {
                maConnection.ConnectionString = Properties.Settings.Default.MaChaineDeConnexionPourKartina;
                maConnection.Open();

                using (SqlCommand maCommand = maConnection.CreateCommand()) //.createCommand==cmd.connection=maConnection
                {
                    string maRequete = "SELECT FirstName, LastName,Titre FROM Vendeur left join Photo on Vendeur.id = Photo.IDVendeur ";

                    maCommand.CommandText = maRequete;

                    using (var monLecteurDeDonnees = maCommand.ExecuteReader())
                    {
                        while (monLecteurDeDonnees.Read())
                        {
                            // Console.WriteLine(monLecteurDeDonnees["FirstName"].ToString()+","+ monLecteurDeDonnees["Titre"]);
                            //string m = monLecteurDeDonnees["FirstName"].ToString() + "," + monLecteurDeDonnees["Titre"];
                            Console.WriteLine(string.Format("{0},{1},{2}", monLecteurDeDonnees["FirstName"], monLecteurDeDonnees["LastName"], monLecteurDeDonnees["Titre"]));
                        }
                    }
                }
            }
        }
    }
}
