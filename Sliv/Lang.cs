using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sliv
{
    public class Lang
    {
        public string lang { get; set; }
        public string Formlang { get; set; }

        public Lang(string lang) {
            this.lang = lang;
            Formlang = lang;
        }
    }
}
