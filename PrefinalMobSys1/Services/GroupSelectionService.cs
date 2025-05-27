using PrefinalMobSys1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Services
{
    public class GroupSelectionService
    {
        public string GroupName { get; set; } = string.Empty;
        public List<SelectableContact> SelectedContacts { get; set; } = new();

        public List<Group> Groups { get; set; } = new();  // <-- add this
        public int CurrentGroupID { get; set; } = 0; // ✅ Add this here

        public class Group
        {
            public string Name { get; set; }
            public List<SelectableContact> Members { get; set; } = new();
            public bool IsExpanded { get; set; } = false;
        }
    }

}
