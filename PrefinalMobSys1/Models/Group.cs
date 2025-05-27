using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrefinalMobSys1.Services;
using PrefinalMobSys1.Models;

namespace PrefinalMobSys1.Models
{
    public class Group
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string GroupName { get; set; } = "";

        public bool IsDeleted { get; set; }

        [Ignore]
        public List<GroupMember> Members { get; set; } = new List<GroupMember>();



    }
}
