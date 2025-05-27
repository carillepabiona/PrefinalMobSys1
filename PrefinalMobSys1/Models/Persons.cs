using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class Persons
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int UserID { get; set; } // <-- Add this line

        public string Name { get; set; }
        public string Number { get; set; } = "";
        public string Email { get; set; }
        public string Address { get; set; }
        public string WorkInfo { get; set; }
        public string Relationships { get; set; }
        public string Groups { get; set; }
        public string ProfileImage { get; set; } = "";  // Default empty, can be null if needed 
        public bool IsFavorite { get; set; }


        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
