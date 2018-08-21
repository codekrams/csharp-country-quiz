using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schwerdtfeger_Laenderquiz
{
    class Land
    {
        private int landid;
        private string landname;
        private string capital;

        public Land(int landid, string landname, string capital)
        {
            this.landid = landid;
            this.landname = landname;
            this.capital = capital;
        }

        //Überladung des Konstruktors, damit bei der Länder- und Städteeingabe nicht alle Informationen gespeichert werden müssen
        public Land(string landname, string capital)
        {
            this.landname = landname;
            this.capital = capital;
        }

        //Überladung des Konstruktors, damit bei der Länder- und Städteeingabe nicht alle Informationen gespeichert werden müssen
        public Land(string landname)
        {
            this.landname = landname;
        }

        public int getLandid()
        {
            return landid;
        }

        public string getLandname()
        {
            return landname;
        }

        public string getCapital()
        {
            return capital;
        }

    }
}


