using Business.Data;
using Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Managers
{
    public class UserManager : BaseManager
    {
        public UserManager(ExchangeContext context) : base(context) {

        }
        public async Task<bool> IsUserValidAsync(string userName, string password) {
            return await context.Users.Where(x=> x.userName == userName && x.password == password).AnyAsync();
        }
    }
}
