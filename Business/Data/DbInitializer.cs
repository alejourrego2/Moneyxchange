using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Data
{
    public class DbInitializer
    {
        public static void Initialize(ExchangeContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var users = new User[]
            {
                new User{userName="murrego", password="123456"},
                new User{userName="belatrix", password="belatrix"},
                new User{userName="admin", password="123456"},
                new User{userName="chief", password="123456"},
            };

            foreach (User u in users)
            {
                context.Users.Add(u);
            }

            context.SaveChanges();
        }
    }
}
