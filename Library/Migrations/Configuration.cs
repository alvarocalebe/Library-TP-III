namespace Library.Migrations
{
    using Library.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Library.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Library.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // Cria a role de administrador, se ainda não existir
            if (!roleManager.RoleExists("Admin"))
            {
                roleManager.Create(new IdentityRole("Admin"));
            }

            // Cria um usuário administrador, se necessário
            var adminUser = userManager.FindByEmail("admin@exemplo.com");
            if (adminUser == null)
            {
                adminUser = new ApplicationUser { UserName = "admin@exemplo.com", Email = "admin@exemplo.com" };
                userManager.Create(adminUser, "SenhaForte123"); // Defina uma senha forte
                userManager.AddToRole(adminUser.Id, "Admin");
            }

            // Salva mudanças
            context.SaveChanges();
        }
    }
}
