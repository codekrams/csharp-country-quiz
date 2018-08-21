using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schwerdtfeger_Laenderquiz
{
    public partial class Flaggen : Form
    {

        static Random rand = new Random(); //Die Klassenvariabel-Variante wurde gewählt, damit nicht jedesmal eine neue Instanz des Random-Objekts aufgerufen werden muss

        List<Land> land = new List<Land>();
        List<Land> landantworten = new List<Land>();
        List<Quizuser> user = new List<Quizuser>();

        Datenbank db = new Datenbank();

        public Flaggen()
        {
            InitializeComponent();
            land = db.getAllLaender();
            user = db.getAllUser();
        }

        //Methoden zur Anzeige der Fragen und Antwortmöglichkeiten auf Grundlage der globalen Variabeln Auswahl1 und Auswahl2
        private void Flaggen_Load(object sender, EventArgs e)
        {
            try
            {
                Global.zaehler = 1;
                anzeigenFrage();
                weitereAntworten();
                anzeigenAntworten();
                Global.anzeigenFortschritt(Fortschritt, Punkte_User);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler aufgetreten: " + ex.Message);
            }
        }

        private void schließen_Click(object sender, EventArgs e)
        {
            Global.highscore = 0;
            Global.zaehler = 0;
            this.Close();
        }

        //Die Liste mit den Antwortmöglichkeiten wird wieder geleert und neu erstellt, dann wird die neue Frage mit den Antwortmöglichkeiten angezeigt
        //solange der Fragenzaehler mit der globalen Variable "zaehler" nicht den Wert 10 überschreitet
        //Die Methoden: gibFeedback(), anzeigenFortschritt() und speichernHighscore() wurden in die static class übernommen, da sie bei allen Spielmodi gleich sind
        private void weiter_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonAnwort1.Checked == true 
                    || radioButtonAntwort2.Checked == true
                    || radioButtonAntwort3.Checked == true
                    || radioButtonAntwort4.Checked == true)
                {
                    
                    Global.gibFeedback(checkAntwort());
                }
                else 
                {
                    MessageBox.Show("Bitte eine Antwort auswählen");
                }


                if (Global.zaehler < 10)
                {
                    landantworten.Clear();
                    anzeigenFrage();
                    weitereAntworten();
                    anzeigenAntworten();
                    Global.zaehler++;
                    Global.anzeigenFortschritt(Fortschritt, Punkte_User);
                }
                else
                {
                    Global.speichernHighscore(user);
                    Highscoreliste f2 = new Highscoreliste();
                    f2.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler aufgetreten: " + ex.Message);
            }
        }

        private void anzeigenFrage()
        {
            int indexrichtigeantwort;

            if (Global.auswahl2 == 1)
            {

                indexrichtigeantwort = rand.Next(0, 13);
                string hauptstadt = land[indexrichtigeantwort].getCapital();
                Frage.Text = "Welche Flagge gehört zu dem Land dessen Hautpstadt " + hauptstadt + " ist?";
                Global.richtigeantwort = land[indexrichtigeantwort].getLandname();
                landantworten.Add(new Land(Global.richtigeantwort));
            }
            else if (Global.auswahl2 == 2)
            {
                indexrichtigeantwort = rand.Next(0, 13);
                string landname = land[indexrichtigeantwort].getLandname();
                Frage.Text = "Welche Flagge gehört zu dem Land " + landname + " ?";
                Global.richtigeantwort = landname;
                landantworten.Add(new Land(landname));
            }
        }

        private void weitereAntworten()
        {

            for (int i = 0; i < 3; i++)
            {
                string landname = land[rand.Next(0, 13)].getLandname();
                int pruef = 1;

                foreach (Land land in landantworten)
                {
                    if (land.getLandname() == landname)
                    {
                        pruef = 0;
                        break;
                    }
                }

                if (pruef == 1)
                {
                    landantworten.Add(new Land(landname));
                }
                else
                {
                    i--;
                }
            }
        }

        //Um die richtige Antwort nicht immer an der ersten Stelle zu haben, wird die Liste bei der Anzeige neu und zufällig sortiert
        private void anzeigenAntworten()
        {
            Global.holeBildpfad();
            landantworten = landantworten.OrderBy(item => rand.Next()).ToList();

            pictureBoxAntwort1.ImageLocation = Global.bildpfad + "Flaggen\\" + landantworten[0].getLandname() + ".png";
            pictureBoxAntwort2.ImageLocation = Global.bildpfad + "Flaggen\\" + landantworten[1].getLandname() + ".png";
            pictureBoxAntwort3.ImageLocation = Global.bildpfad + "Flaggen\\" + landantworten[2].getLandname() + ".png";
            pictureBoxAntwort4.ImageLocation = Global.bildpfad + "Flaggen\\" + landantworten[3].getLandname() + ".png";
        }

        //Prüfung der Antwort
        private bool checkAntwort()
        {
            int pruef = 3;

            if (radioButtonAnwort1.Checked == true)
            {
                if (Global.richtigeantwort == landantworten[0].getLandname())
                {
                    pruef = 1;
                }
                else
                {
                    pruef = 0;
                }
            }

            if (radioButtonAntwort2.Checked == true)
            {
                if (Global.richtigeantwort == landantworten[1].getLandname())
                {
                    pruef = 1;
                }
                else
                {
                    pruef = 0;
                }
            }

            if (radioButtonAntwort3.Checked == true)
            {
                if (Global.richtigeantwort == landantworten[2].getLandname())
                {
                    pruef = 1;
                }
                else
                {
                    pruef = 0;
                }
            }

            if (radioButtonAntwort4.Checked == true)
            {
                if (Global.richtigeantwort == landantworten[3].getLandname())
                {
                    pruef = 1;
                }
                else
                {
                    pruef = 0;
                }
            }


            if (pruef == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
