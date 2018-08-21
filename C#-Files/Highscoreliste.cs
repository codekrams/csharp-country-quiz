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
    public partial class Highscoreliste : Form
    {
        List<Highscore> score = new List<Highscore>();
        List<Quizuser> user = new List<Quizuser>();

        Datenbank db = new Datenbank();

        public Highscoreliste()
        {
            InitializeComponent();
            score = db.getAllHighscore();
            user = db.getAllUser();
        }

        //zwei verschiedene Anzeigenmodi: Highscoreliste mit den Top-5-Spielern (limitiert auf genau 5 Spieler) und die Auswahl für einen bestimmten Spieler
        private void Highscoreliste_Load(object sender, EventArgs e)
        {
            try
            {
                anzeigenTop5();
                anzeigenSpieler();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bitte Datenbankeinstellung prüfen");
            }
        }       

        //Auswahl zum Beenden des gesamten Programms
        private void Beenden_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //Auswahl für ein neues Spiel mit dem gleichen Benutzer
        private void Neues_Spiel_Click(object sender, EventArgs e)
        {
            try
            {
                Global.highscore = 0;
                Global.zaehler = 0;
                AuswahlStart f2 = new AuswahlStart();
                f2.ShowDialog();
            }
            catch (Exception ex) {
                MessageBox.Show("Zu viele Fenster geöffnet");
            }
        }

        private void anzeigen_Click(object sender, EventArgs e)
        {
            try
            {
                highscoreUser();
            }
            catch (Exception ex) {
                MessageBox.Show("Fehler aufgetreten: " + ex.Message);
            }
        }

        private void anzeigenTop5()
        {
            string Top5 = "";

            foreach (Highscore hs in score)
            {
                Top5 += hs.getUser() + ", " + hs.getHighscore() + Environment.NewLine;

            }
            top_5.Text = Top5;
        }

        private void anzeigenSpieler()
        {
            foreach (Quizuser us in user) {
                comboBoxSpieler.Items.Add(us.getUsername());
            }
        }

        private void highscoreUser()
        {
            Spieler.Text = user[comboBoxSpieler.SelectedIndex].getUsername() + ", " + user[comboBoxSpieler.SelectedIndex].getHighscore().ToString();
        }

    }
}
