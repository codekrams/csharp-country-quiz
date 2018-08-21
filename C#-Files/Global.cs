using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schwerdtfeger_Laenderquiz
{
    //Erreichbar aus beiden Spielmodus-Forms: Flaggen sowie Länder_Hauptstädte
    static class Global
    {
        public static int auswahl1 = 0;
        public static int auswahl2 = 0;
        public static int zaehler = 0;
        public static int highscore = 0;
        public static string benutzer = "";
        public static string richtigeantwort = "";
        public static string bildpfad = "";
        public static int auswahl = 0;

        //Anzeigen des Fortschritts, wieviele Fragen noch bleiben und wieviel Punkte bereits erreicht wurden
        public static void anzeigenFortschritt( Label f, Label p)
        {
           f.Text = "Frage " + Global.zaehler.ToString() + "/10";
           p.Text = "Punkte bisher: " + Global.highscore.ToString();
        }

        //Holen des Bildpfades, damit die Bilder auch bei den Spielern angezeigt werden, wenn der Pfad richtig gespeichert wurde
        public static void holeBildpfad()
        {
            string eingabe = "Bildpfad.txt";

            FileStream fs = new FileStream(eingabe, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);


            while (sr.Peek() != -1)
            {
                bildpfad = sr.ReadLine();
            }

            sr.Close();
            fs.Close();
        }

        //Speichern des Highscores, wenn er größer ist als der bisher erreichte
        public static void speichernHighscore(List<Quizuser> user)
        {
            Datenbank db = new Datenbank();

            int score = 0;
            int id = 0;

            foreach (Quizuser us in user)
            {
                if (us.getUsername() == Global.benutzer)
                {
                    score = us.getHighscore();
                    id = us.getID();
                    break;
                }
            }
            if (Global.highscore > score)
            {
                db.updateHighscore(id, Global.highscore);
                MessageBox.Show(Global.highscore.ToString() + " Punkte! Neuer Highscore!");
            }
            else
            {
                MessageBox.Show(Global.highscore.ToString() + " Punkte! Leider kein neuer Highscore!");
            }
        }

        //Feedback nach jeder Antwort, ob sie korrekt ist oder nicht
        public static void gibFeedback(bool checkAntwort)
        {

            if (checkAntwort == true)
            {
                Global.highscore += 2;
                MessageBox.Show("Die Antwort ist korrekt");
            }
            else
            {
                MessageBox.Show("Die Antwort ist leider falsch");
            }
        }

    }
}
