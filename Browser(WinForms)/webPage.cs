using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser_WinForms_
{
    public class @string
    {
        public string URL { get; set; }= "https://www.google.com";
        public bool favorite = false;

        public override string ToString()
        {
            return this.URL;
        }
    }
}
