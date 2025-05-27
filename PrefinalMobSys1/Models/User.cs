using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class User
    {
        [PrimaryKey]
        [NotNull]
        [AutoIncrement]
        public int ID { get; set; }

        [NotNull]
        public string Username { get; set; }

        [NotNull]
        public string Password { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Address { get; set; }
        
        public string MobileNumber { get; set; }

        public string Photo { get; set; }

        public string WorkInfo { get; set; }
        public string Email { get; set; }
        public string Relationships { get; set; }
        public string Groups { get; set; }

        // existing properties
        public bool IsFavorite { get; set; }

        public int PersonID { get; set; }  // FK to Person table

        [NotNull]
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
