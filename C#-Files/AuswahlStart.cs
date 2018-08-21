using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Schwerdtfeger_Laenderquiz
{
    public partial class AuswahlStart : Form
    {

        public AuswahlStart()
        {
            InitializeComponent();
        }

        //Setzt den Highscore und den Zähler wieder zurück, falls mehrere Spiele hintereinander gespielt werden
        private void AuswahlStart_Load(object sender, EventArgs e)
        {
            Global.highscore = 0;
            Global.zaehler = 0;
            Grundlage.Visible = false;
            radioButtonAuswahl2a.Visible = false;
            radioButtonAuswahl2b.Visible = false;
            Start.Visible = false;
        }

        private void zurück_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Laden der Highscorelisten-Form
        private void Highscoreliste_Click(object sender, EventArgs e)
        {
            Highscoreliste f2 = new Highscoreliste();
            f2.ShowDialog();
        }

        //Anzeigen und Speichern der ersten Auswahl
        private void Auswahl1_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonFlaggen.Checked == true || radioButtonLand.Checked == true || radioButtonStädte.Checked == true)
                {
                    anzeigenAuswahl();
                }
                else
                {
                    MessageBox.Show("Bitte Option auswählen!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler aufgetreten: " + ex.Message);
            }
        }

        //Anzeigen und Speichern der zweiten Auswahl und starten einer der drei Hauptspielmodi
        private void Start_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonAuswahl2a.Checked == true || radioButtonAuswahl2b.Checked == true)
                {
                    getAuswahl2();
                }
                else
                {
                    MessageBox.Show("Bitte Grundlage auswählen!");
                }

                if (Global.auswahl1 != 0 && Global.auswahl2 != 0)
                {
                    startSpiel();
                }
                else
                {
                    MessageBox.Show("Bitte Optionen auswählen");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler aufgetreten: " + ex.Message);
            }

        }

        //Die Variable "auswahl1" wurde global für alle Forms deklariert, damit in den nächsten Forms darauf zugegriffen werden kann
        private void anzeigenAuswahl()
        {
            if (radioButtonLand.Checked == true)
            {
                radioButtonAuswahl2a.Text = "Hauptstädte";
                radioButtonAuswahl2b.Text = "Flaggen";
                Global.auswahl1 = 1;
            }

            else if (radioButtonFlaggen.Checked == true)
            {
                radioButtonAuswahl2a.Text = "Hauptstädte";
                radioButtonAuswahl2b.Text = "Länder";
                Global.auswahl1 = 2;
            }
            else if (radioButtonStädte.Checked == true)
            {
                radioButtonAuswahl2a.Text = "Länder";
                radioButtonAuswahl2b.Text = "Flaggen";
                Global.auswahl1 = 3;
            }
            else
            {
                MessageBox.Show("Bitte eine Option auswählen");
            }

            Grundlage.Visible = true;
            radioButtonAuswahl2a.Visible = true;
            radioButtonAuswahl2b.Visible = true;
            Start.Visible = true;
        }

        //Die Variable "auswahl2" wurde global für alle Forms deklariert, damit in den nächsten Forms darauf zugegriffen werden kann
        private void getAuswahl2()
        {
                if (radioButtonAuswahl2a.Checked == true)
                {
                    Global.auswahl2 = 1;
                }
                if (radioButtonAuswahl2b.Checked == true)
                {
                    Global.auswahl2 = 2;
                }
        }

        private void startSpiel()
        {
            if (Global.auswahl1 == 1 || Global.auswahl1 == 3)
            {
                if (Global.auswahl2 == 1) //Auswahl1: Länder oder Hauptstädte erraten (je nach Auswahl1), Auswahl2: auf Grundlage von Hautpstädten
                { 
                    Länder_Hauptstädte f2 = new Länder_Hauptstädte();
                    f2.ShowDialog();
                }
                else if (Global.auswahl2 == 2) //Auswahl1: Länder oder Hauptstädte erraten (je nach Auswahl1), Auswahl2: auf Grundlage von Flaggen
                {
                    Länder_Hauptstädte f2 = new Länder_Hauptstädte();
                    f2.ShowDialog();
                }
            }

            if (Global.auswahl1 == 2)
            {
                if (Global.auswahl2 == 1) //Auswahl1: Flaggen erraten, Auswahl2: auf Grundlage von Hautpstädten
                {
                    Flaggen f2 = new Flaggen();
                    f2.ShowDialog();
                }
                else if (Global.auswahl2 == 2) //Auswahl1: Flaggen erraten, Auswahl2: auf Grundlage von Ländern
                {
                    Flaggen f2 = new Flaggen();
                    f2.ShowDialog();
                }
            }
        }

    }
}
