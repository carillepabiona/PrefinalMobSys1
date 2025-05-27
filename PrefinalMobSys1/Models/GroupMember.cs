using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Models
{
    public class GroupMember
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int GroupID { get; set; }

        public int PersonID { get; set; }

        [Ignore]
        public Persons Person { get; set; }  // ✅ Just one Person, not a list
    }
}

