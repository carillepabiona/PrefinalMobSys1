using PrefinalMobSys1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrefinalMobSys1.Services;
using Microsoft.EntityFrameworkCore;



namespace PrefinalMobSys1.Data
{
    /// <summary>
    /// Centralized Class for handling local SQLite Database things for the App
    /// Loaded in MauiProgram as Singleton (one instance only within the App)
    /// </summary>
    public class DatabaseContext 
    {
        SQLiteAsyncConnection database;
        public static DatabaseContext Instance { set; get; }

        public DatabaseContext()
        {
            // init from constructor
            DatabaseContext.Instance = this;
        }

        // public DbSet<User> Users { get; set; }
        /// <summary>
        /// Initialize Database Availability
        /// </summary>
        /// <returns></returns>
        public async Task Init()
        {
            if (database is not null)
                return;

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            // Create tables

            await database.CreateTableAsync<Group>();
            await database.CreateTableAsync<GroupMember>();
            await database.CreateTableAsync<Persons>();
            await database.CreateTableAsync<User>();
        }

        // Get all users except soft-deleted ones
        public async Task<List<User>> Users()
        {
            await Init();
            return await database.Table<User>()
                                 .Where(u => !u.IsDeleted)
                                 .ToListAsync();
        }

        // Get a user by ID, only if not soft-deleted
        public async Task<User> GetUserById(int id)
        {
            await Init();
            return await database.Table<User>()
                                 .Where(u => u.ID == id && !u.IsDeleted)
                                 .FirstOrDefaultAsync();
        }

        // SaveUser method to add or update the user
        public async Task SaveUser(User user)
        {
            await Init();

            if (user.ID == 0)
            {
                await database.InsertAsync(user);
            }
            else
            {
                await database.UpdateAsync(user);
            }
        }

        public async Task<int> SavePerson(Persons person, int userId)
        {
            await Init();
            person.UserID = userId; // Associate person with user
            if (person.ID != 0)
                return await database.UpdateAsync(person);
            else
                return await database.InsertAsync(person);
        }


        // Hard delete user (remove from DB)
        public async Task<int> DeleteUser(User incoming)
        {
            await Init();
            return await database.DeleteAsync(incoming);
        }

        // Soft delete user (set IsDeleted = true and update)
        public async Task<int> SoftDeleteUser(User incoming)
        {
            await Init();
            incoming.IsDeleted = true;
            return await database.UpdateAsync(incoming);
        }


        // add contacts
        public async Task<int> DeletePerson(int id)
        {
            await Init();
            var person = await database.Table<Persons>().Where(p => p.ID == id).FirstOrDefaultAsync();
            if (person != null)
            {
                person.IsDeleted = true; // Soft delete
                return await database.UpdateAsync(person);
            }
            return 0; // Return 0 if no person found
        }



        public async Task<int> SavePerson(Persons person)
        {
            await Init();
            if (person.ID != 0)
                return await database.UpdateAsync(person);
            else
                return await database.InsertAsync(person);
        }

        // Get all Persons (non deleted)
        public async Task<List<Persons>> GetPersons()
        {
            await Init();
            return await database.Table<Persons>()
                                 .Where(p => !p.IsDeleted)
                                 .ToListAsync();
        }

        public async Task<List<Persons>> GetPersonsByUserId(int userId)
        {
            await Init();
            return await database.Table<Persons>()
                                 .Where(p => p.UserID == userId && !p.IsDeleted)
                                 .ToListAsync();
        }


        // Get all Contacts for a specific PersonID
        public async Task<List<User>> GetContactsByPersonId(int personId)
        {
            await Init();
            return await database.Table<User>()
                                 .Where(u => u.PersonID == personId && !u.IsDeleted)
                                 .ToListAsync();
        }

        public async Task<int> UpdatePerson(Persons person)
        {
            await Init();

            var existingPerson = await database.Table<Persons>()
                                               .Where(p => p.ID == person.ID)
                                               .FirstOrDefaultAsync();

            if (existingPerson != null)
            {
                existingPerson.Name = person.Name;
                existingPerson.Number = person.Number;
                existingPerson.Email = person.Email;
                existingPerson.Address = person.Address;
                existingPerson.WorkInfo = person.WorkInfo;
                existingPerson.Relationships = person.Relationships;
                existingPerson.Groups = person.Groups;
                existingPerson.IsFavorite = person.IsFavorite;

                return await database.UpdateAsync(existingPerson);
            }
            return 0; // No person found to update
        }

        public async Task UpdatePersonFavoriteStatus(int personId, bool isFavorite)
        {
            await Init(); // make sure db is initialized

            var person = await database.Table<Persons>()
                                       .Where(p => p.ID == personId)
                                       .FirstOrDefaultAsync();

            if (person != null)
            {
                person.IsFavorite = isFavorite;
                await database.UpdateAsync(person);
            }
        }


        public async Task<int> SaveGroup(Group group, List<GroupMember> members)
        {
            await Init();

            if (group.ID == 0)
                await database.InsertAsync(group);
            else
                await database.UpdateAsync(group);

            foreach (var member in members)
            {
                member.GroupID = group.ID;

                if (member.ID == 0)
                    await database.InsertAsync(member);
                else
                    await database.UpdateAsync(member);
            }

            return group.ID;
        }

        public async Task<List<Group>> GetGroupsWithMembers()
        {
            await Init();

            var groups = await database.Table<Group>().Where(g => !g.IsDeleted).ToListAsync();

            foreach (var group in groups)
            {
                var members = await database.Table<GroupMember>()
                    .Where(m => m.GroupID == group.ID)
                    .ToListAsync();

                // Optionally, load each member's Person details here
                foreach (var member in members)
                {
                    member.Person = await database.Table<Persons>()
                                                 .Where(p => p.ID == member.PersonID)
                                                 .FirstOrDefaultAsync();
                }

                group.Members = members;
            }

            return groups;
        }


        public async Task DeleteGroup(int groupId)
        {
            await Init();

            // Delete all members for group
            var members = await database.Table<GroupMember>().Where(m => m.GroupID == groupId).ToListAsync();
            foreach (var member in members)
            {
                await database.DeleteAsync(member);
            }

            // Delete group itself
            var group = await database.Table<Group>().Where(g => g.ID == groupId).FirstOrDefaultAsync();
            if (group != null)
            {
                await database.DeleteAsync(group);
            }
        }
        public async Task SaveGroupMember(GroupMember member)
        {
            await database.InsertAsync(member);
        }

        // ✅ Add this method to insert a group member
        public async Task AddGroupMemberAsync(GroupMember member)
        {
            await database.InsertAsync(member);
        }

        // Optionally, get all group members
        public Task<List<GroupMember>> GetGroupMembersAsync() =>
            database.Table<GroupMember>().ToListAsync();

   

        // groups / Singleton instance and SQLite connection setup here...

        public Task<List<Group>> GetGroups()
        {
            return database.Table<Group>().Where(g => !g.IsDeleted).ToListAsync();
        }

        public Task<List<GroupMember>> GetGroupMembersByGroupId(int groupId)
        {
            return database.Table<GroupMember>().Where(m => m.GroupID == groupId).ToListAsync();
        }

        public Task<int> InsertGroup(Group group)
        {
            return database.InsertAsync(group);
        }

        public Task<int> UpdateGroup(Group group)
        {
            return database.UpdateAsync(group);
        }

        public Task<int> InsertGroupMember(GroupMember member)
        {
            return database.InsertAsync(member);
        }

        public Task<int> UpdateGroupMember(GroupMember member)
        {
            return database.UpdateAsync(member);
        }

    }
}
