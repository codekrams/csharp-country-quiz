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
    public partial class Länder_Hauptstädte : Form
    {
       
        static Random rand = new Random(); //Die Klassenvariabel-Variante wurde gewählt, damit nicht jedesmal eine neue Instanz des Random-Objekts aufgerufen werden muss

        List<Land> land = new List<Land>();
        List<Land> landantworten = new List<Land>();
        List<Quizuser> user = new List<Quizuser>();
        List<Land> stadtantworten = new List<Land>();

        Datenbank db = new Datenbank();

        public Länder_Hauptstädte()
        {
            InitializeComponent();
            land = db.getAllLaender();
            user = db.getAllUser();
        }

        //Methoden zur Anzeige der Fragen und Antwortmöglichkeiten auf Grundlage der globalen Variabeln Auswahl1 und Auswahl2
        private void Länder_Load(object sender, EventArgs e)
        {
            try
            {
                if (Global.auswahl1 == 1)
                {
                    this.Text = "Länder erraten";
                }
                else if (Global.auswahl1 == 3) {
                    this.Text = "Hauptstädte erraten";
                }


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
                if (Global.auswahl1 == 1)
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
                else if (Global.auswahl1 == 3)
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
                        stadtantworten.Clear();
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

            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler aufgetreten: " + ex.Message);
            }
        }

        private void anzeigenFrage()
        {
            Global.holeBildpfad();
            int indexrichtigeantwort;
            indexrichtigeantwort = rand.Next(0, 13);

            if (Global.auswahl1 == 1)
            {
                if (Global.auswahl2 == 1)
                {
                    
                    string hauptstadt = land[indexrichtigeantwort].getCapital();
                    Frage.Text = "Wie heißt das Land, dessen Hauptstadt " + hauptstadt + " ist?";
                    Global.richtigeantwort = land[indexrichtigeantwort].getLandname();
                    landantworten.Add(new Land(Global.richtigeantwort));
                    pictureBoxFrage.ImageLocation = Global.bildpfad + "Staedte\\" + hauptstadt + ".jpg";
                }
                else if (Global.auswahl2 == 2)
                {
                    
                    Frage.Text = "Wie heißt das Land, zu dem diese Flagge gehört?";
                    Global.richtigeantwort = land[indexrichtigeantwort].getLandname();
                    landantworten.Add(new Land(Global.richtigeantwort));
                    pictureBoxFrage.ImageLocation = Global.bildpfad + "Flaggen\\" + Global.richtigeantwort + ".png";
                }
            }

            if (Global.auswahl1 == 3)
            {
                if (Global.auswahl2 == 1)
                {

                    string landname = land[indexrichtigeantwort].getLandname();
                    Frage.Text = "Wie heißt die Hauptstadt von " + landname + " ?";
                    Global.richtigeantwort = land[indexrichtigeantwort].getCapital();
                    stadtantworten.Add(new Land(landname, Global.richtigeantwort));
                    pictureBoxFrage.ImageLocation = Global.bildpfad + "Flaggen\\" + landname + ".png";
                }
                else if (Global.auswahl2 == 2)
                {

                    string landname = land[indexrichtigeantwort].getLandname();
                    Frage.Text = "Wie heißt die Hauptstadt des Landes, zu dem diese Flagge gehört?";
                    Global.richtigeantwort = land[indexrichtigeantwort].getCapital();
                    stadtantworten.Add(new Land(landname, Global.richtigeantwort));
                    pictureBoxFrage.ImageLocation = Global.bildpfad + "Flaggen\\" + landname + ".png";
                }
            }
        }

        private void weitereAntworten()
        {
            if (Global.auswahl1 == 1)
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

            else if (Global.auswahl1 == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    string capital = land[rand.Next(0, 13)].getCapital();
                    int pruef = 1;

                    foreach (Land land in stadtantworten)
                    {
                        if (land.getCapital() == capital)
                        {
                            pruef = 0;
                            break;
                        }
                    }

                    if (pruef == 1)
                    {
                        stadtantworten.Add(new Land(land[rand.Next(0, 13)].getLandname(), capital));
                    }
                    else
                    {
                        i--;
                    }
                }
            }

        }

        //Um die richtige Antwort nicht immer an der ersten Stelle zu haben, wird die Liste bei der Anzeige neu und zufällig sortiert
        private void anzeigenAntworten()
        {
            if (Global.auswahl1 == 1)
            {
                landantworten = landantworten.OrderBy(item => rand.Next()).ToList();

                radioButtonAnwort1.Text = landantworten[0].getLandname();
                radioButtonAntwort2.Text = landantworten[1].getLandname();
                radioButtonAntwort3.Text = landantworten[2].getLandname();
                radioButtonAntwort4.Text = landantworten[3].getLandname();
            }

            else if (Global.auswahl1 == 3)
            {
                stadtantworten = stadtantworten.OrderBy(item => rand.Next()).ToList();

                radioButtonAnwort1.Text = stadtantworten[0].getCapital();
                radioButtonAntwort2.Text = stadtantworten[1].getCapital();
                radioButtonAntwort3.Text = stadtantworten[2].getCapital();
                radioButtonAntwort4.Text = stadtantworten[3].getCapital();
            }
        }

        //Prüfung der Antwort
        private bool checkAntwort()
        {
            int pruef = 3;

            if (radioButtonAnwort1.Checked == true)
            {
                if (Global.richtigeantwort == radioButtonAnwort1.Text)
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
                if (Global.richtigeantwort == radioButtonAntwort2.Text)
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
                if (Global.richtigeantwort == radioButtonAntwort3.Text)
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
                if (Global.richtigeantwort == radioButtonAntwort4.Text)
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
