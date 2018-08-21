using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schwerdtfeger_Laenderquiz
{
    class Highscore //Entscheidung für eine extra Klasse, damit die Informationen gebündelt sind
    {
        private string user;
        private int highscore;

        public Highscore(string user, int highscore)
        {
            this.user = user;
            this.highscore = highscore;
        }

        public string getUser()
        {
            return user;
        }

        public int getHighscore()
        {
            return highscore;
        }

    }
}
