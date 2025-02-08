using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sliv
{
    public class Lang
    {
        public string _language { get; set; }
        public string Formlang { get; set; }

        public Lang(string _language) {
            this._language = _language;
            Formlang = _language;
        }
    }
}
