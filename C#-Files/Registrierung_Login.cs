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
    public partial class Registrierung_Login : Form
    {
        List<Quizuser> user = new List<Quizuser>();

        Datenbank db = new Datenbank();

        public Registrierung_Login()
        {
            InitializeComponent();
            user = db.getAllUser();
        }

        //Je nach Auswahl wird das Registrierungsformular mit dem Button-Text "Registrierung" oder das Loginformular mit dem Button-Text "Login" aufgereufen
        private void Registrierung_Load(object sender, EventArgs e)
        {
            if (Global.auswahl == 5)
            {
                Passwortwiederholung.Visible = false;
                Userpasswort2.Visible = false;
                registrieren.Text = "login";
                this.Text = "Login";
            }
            else if (Global.auswahl == 6)
            {
                this.Text = "Registrierung";
            }
        }

        //Leeren der Textboxen
        private void Neu_Click(object sender, EventArgs e)
        {
            Usereingabe.Text = "";
            Userpasswort.Text = "";
            Userpasswort2.Text = "";
        }


        //Prüfen des Benuternamens und des Passwortes
        //die Variabel "benutzer" wurde ebenfalls global angelegt, damit sie aus allen Formen erreichbar ist
        private void registrieren_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.auswahl == 6)
                {
                    bool name = benutzernamePruefen(Usereingabe.Text);
                    bool passwort = passwortPruefen(Userpasswort.Text, Userpasswort2.Text);

                    if (name == true && passwort == true)
                    {
                        string pass = passwortVerschluesseln(Userpasswort.Text);
                        db.safeUser(Usereingabe.Text, pass);
                        MessageBox.Show("Registrierung erfolgreich");
                        Global.benutzer = Usereingabe.Text;
                        this.Hide();
                        AuswahlStart f2 = new AuswahlStart();
                        f2.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Benutzername oder Passwort fehlerhaft. \nBitte Eingaben ändern");
                    }
                }

                else if (Global.auswahl == 5)
                {
                    string passwort = passwortImportieren(Usereingabe.Text);
                    string eingabe = passwortVerschluesseln(Userpasswort.Text);

                    bool vergleich = passwortVergleichen(eingabe, passwort);

                    if (vergleich == true)
                    {
                        MessageBox.Show("Login erfolgreich");
                        Global.benutzer = Usereingabe.Text;
                        this.Hide();
                        AuswahlStart f2 = new AuswahlStart();
                        f2.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Passwort nicht korrekt");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler aufgetreten: " + ex.Message);
            }
        }

        //Maskiert die Eingabe des Passworts
        private void Userpasswort_TextChanged(object sender, EventArgs e)
        {
            Userpasswort.PasswordChar = '*';
        }

        //Maskiert die Eingabe des Passworts
        private void Userpasswort2_TextChanged(object sender, EventArgs e)
        {
            Userpasswort2.PasswordChar = '*';
        }

        //Methode zur Prüfung, ob der Benutzername bereits vergeben ist
        private bool benutzernamePruefen(string name)
        {

            int pruef = 0;

            if (!String.IsNullOrEmpty(name))
            {
                foreach (Quizuser qu in user)
                {
                    if (qu.getUsername() == name)
                    {
                        pruef = 0;
                        break;
                    }
                    else
                    {
                        pruef = 1;
                    }
                }
            }


            if (pruef == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //Prüfung, ob das Passwort und die Wiederholung übereinstimmen
        private bool passwortPruefen(string passwort, string passwortwiederh)
        {

            if (String.IsNullOrEmpty(passwort) || String.IsNullOrEmpty(passwortwiederh))
            {
                return false;
            }
            else if (passwort == passwortwiederh)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Algortihmus zum Verschlüsseln der Passwörter
        private string passwortVerschluesseln(string eingabe)
        {
            char[] passwortarr = eingabe.ToCharArray();


            for (int i = 0; i < passwortarr.Length; i++)
            {
                int wert = Convert.ToInt32(passwortarr[i]) + 1;
                passwortarr[i] = Convert.ToChar(wert);
            }

            string passwort = new string(passwortarr);
            return passwort;
        }

        private string passwortImportieren(string benutzer)
        {
            string passwort = null;

            foreach (Quizuser qu in user)
            {
                if (benutzer == qu.getUsername())
                {
                    passwort = qu.getPasswort();
                }
            }

            return passwort;
        }

        private bool passwortVergleichen(string eingabe, string passwort)
        {

            if (eingabe == passwort)
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
