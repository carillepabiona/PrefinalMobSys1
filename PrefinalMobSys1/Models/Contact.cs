using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class Contact
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Number { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string Location { get; set; } = "";
        public string ProfileImage { get; set; } = "";  // Default empty, can be null if needed 
        public bool IsFavorite { get; set; } = false;
    }
}
