using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schwerdtfeger_Laenderquiz
{
    class Quizuser
    {
        private int userid;
        private string username;
        private string passwort;
        private int highscore;

        public Quizuser(int userid, string username, string passwort, int highscore)
        {
            this.userid = userid;
            this.username = username;
            this.passwort = passwort;
            this.highscore = highscore;
        }

        public int getID()
        {
            return userid;
        }

        public string getUsername()
        {
            return username;
        }

        public string getPasswort()
        {
            return passwort;
        }

        public int getHighscore()
        {
            return highscore;
        }


    }
}