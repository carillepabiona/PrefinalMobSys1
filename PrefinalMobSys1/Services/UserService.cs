using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrefinalMobSys1.Data;
using PrefinalMobSys1.Models;

namespace PrefinalMobSys1.Services
{
    public class UserService
    {
        private readonly DatabaseContext _dbContext;

        public UserService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveUserAsync(User user)
        {
            await _dbContext.Init();
            await _dbContext.SaveUser(user);
            return user.ID; // after InsertAsync, user.ID is updated with new PK
        }

    }

}
