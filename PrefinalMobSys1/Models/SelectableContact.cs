using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class SelectableContact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public bool IsSelected { get; set; }
    }
}
