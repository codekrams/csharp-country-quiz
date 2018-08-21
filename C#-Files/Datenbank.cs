using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Schwerdtfeger_Laenderquiz
{
    class Datenbank
    {
        private MySqlConnection dbConnection;

        //Ein eigener User wurde angelegt, der vollen Zugriff auf die Tabelle mit den Usern hat für den Highsocre, Login und die Neu-Registrierung,
        //aber nur lesenden Zugriff auf die zweite Tabelle mit den Ländern, sodass da nichts geöndert werden kann
        public void dbOeffnen()
        {
            try
            {
                dbConnection = new MySqlConnection("SERVER=127.0.0.1; DATABASE=laenderquizms;UID=quizuser; PASSWORD=quizuser; SSLMode=None;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void dbSchliessen()
        {
            dbConnection.Close();
        }

        //Methode zum Abrufen der Userinformationen aus der Datenbank
        public List<Quizuser> getAllUser()
        {
            List<Quizuser> user = new List<Quizuser>();

            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE laenderquizms; SELECT * FROM laenderquizms.quizuser;";
            dbConnection.Open();
            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                Quizuser us = new Quizuser(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3));
                user.Add(us);
            }
            reader.Close();
            dbConnection.Close();
            dbSchliessen();

            return user;
        }

        ////Methode, um den Highscore am Ende des Spieles zu aktualisieren
        public void updateHighscore(int user, int highscore)
        {
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();

            comm.CommandText = "USE laenderquizms; UPDATE laenderquizms.quizuser SET highscore = '" + highscore + "' WHERE userid = '" + user + "';";

            dbConnection.Open();
            comm.ExecuteNonQuery();

            dbConnection.Close();
            dbSchliessen();
        }

        //Methode, um den User bei der Registrierung zu speichern
        public void safeUser(string username, string passwort)
        {
            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();

            comm.CommandText = "USE laenderquizms; INSERT INTO laenderquizms.quizuser VALUES (NULL, '" + username + "', '" + passwort + "', 0);";

            dbConnection.Open();
            comm.ExecuteNonQuery();

            dbConnection.Close();
            dbSchliessen();
        }

        //Methode zum Abrufen der Länderinformationen aus der Datenbank
        public List<Land> getAllLaender()
        {
            List<Land> laender = new List<Land>();

            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE laenderquizms; SELECT * FROM laenderquizms.land;";
            dbConnection.Open();
            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                Land la = new Land(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                laender.Add(la);
            }
            reader.Close();
            dbConnection.Close();
            dbSchliessen();

            return laender;
        }

        //Methode zum Abrufen der Highscoreinformationen aus der Datenbank
        public List<Highscore> getAllHighscore()
        {
            List<Highscore> high = new List<Highscore>();

            dbOeffnen();
            MySqlCommand comm = dbConnection.CreateCommand();
            comm.CommandText = "USE laenderquizms; SELECT laenderquizms.quizuser.username, laenderquizms.quizuser.highscore FROM laenderquizms.quizuser ORDER BY laenderquizms.quizuser.highscore DESC,  laenderquizms.quizuser.username LIMIT 5;";
            dbConnection.Open();
            MySqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                Highscore hs = new Highscore(reader.GetString(0), reader.GetInt32(1));
                high.Add(hs);
            }
            reader.Close();
            dbConnection.Close();
            dbSchliessen();

            return high;
        }


    }
}