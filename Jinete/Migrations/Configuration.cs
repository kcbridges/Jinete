namespace Jinete.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Jinete.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Jinete.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        bool AddUserAndRole(ApplicationDbContext context)
        {
            IdentityResult ir;
            //uncomment the following lines to create new role types for users
            var rm = new RoleManager<IdentityRole>
               (new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("Administrator"));
            ir = rm.Create(new IdentityRole("Manager"));
            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "hakcer",
                Email = "kcbridges@casa-latina.org",
                FirstName = "KC",
                LastName = "Bridges",
                Phone = "360-421-1827",
                Address = "where the wind takes me",
                City = "Seattle",
                State = "WA",
                Zip = "98144"
            };
            ir = um.Create(user, "");
            if (ir.Succeeded == false)
                return ir.Succeeded;
            ir = um.AddToRole(user.Id, "Administrator");
            return ir.Succeeded;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var result = AddUserAndRole(context);
        }
    }
}
