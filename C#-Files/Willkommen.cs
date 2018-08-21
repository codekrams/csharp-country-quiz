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
    public partial class Willkommen : Form
    {
        public Willkommen()
        {
            InitializeComponent();
        }

        //Aufruf der Form zum Login_Registrierung mit Focus auf Registrierung
        private void registrieren_Click(object sender, EventArgs e)
        {
            try
            {
                Global.auswahl = 6;
                Registrierung_Login f2 = new Registrierung_Login();
                f2.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datenbank nicht erreichbar. Bitte Verbindung prüfen");
            }
        }

        //Aufruf der Form zum Login_Registrierung mit Focus auf Login
        private void login_Click(object sender, EventArgs e)
        {
            try
            {
                Global.auswahl = 5;
                Registrierung_Login f2 = new Registrierung_Login();
                f2.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Datenbank nicht erreichbar. Bitte Verbindung prüfen"); ;
            }
        }

        private void schliessen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
