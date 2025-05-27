using PrefinalMobSys1.Models;
using PrefinalMobSys1.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrefinalMobSys1.Services
{
    public class GroupService
    {
        private readonly DatabaseContext _db;

        public GroupService()
        {
            _db = DatabaseContext.Instance;
        }

        public async Task<List<Persons>> GetAllContactsAsync()
        {
            return await _db.GetPersons();
        }

        public async Task<List<Group>> GetGroupsAsync()
        {
            return await _db.GetGroups(); // You need to implement GetGroups in DatabaseContext
        }

        public async Task<List<Group>> GetGroupsWithMembersAsync()
        {
            var groups = await GetGroupsAsync();

            foreach (var group in groups)
            {
                group.Members = await _db.GetGroupMembersByGroupId(group.ID);
            }

            return groups;
        }

        public async Task SaveGroupAsync(Group group)
        {
            if (group.ID == 0)
            {
                await _db.InsertGroup(group);
            }
            else
            {
                await _db.UpdateGroup(group);
            }

            foreach (var member in group.Members)
            {
                member.GroupID = group.ID;

                if (member.ID == 0)
                {
                    await _db.InsertGroupMember(member);
                }
                else
                {
                    await _db.UpdateGroupMember(member);
                }
            }
        }
    }
}
