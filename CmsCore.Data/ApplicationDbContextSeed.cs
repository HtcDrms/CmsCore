using CmsCore.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Data
{
    public static class ApplicationDbContextSeed
    {
        public static void Seed(this ApplicationDbContext context, UserManager<ApplicationUser> _userManager, RoleManager<Role> _roleManager)
        {

            context.Database.Migrate();

            // Perform seed operations
            AddLanguages(context);
            AddPages(context);
            AddMenus(context);
            AddMenuItems(context);
            AddMenuLocations(context);
            AddUsers(context, _userManager);
            AddRoles(_roleManager);
            AddRoleToUser(_userManager);
            // Save changes and release resources
            context.SaveChangesAsync();
            context.Dispose();
        }
        static Menu menu;
        static ApplicationUser user;
        
        private static void AddLanguages(ApplicationDbContext context)
        {
            context.AddRange(
                new Language { Name = "Türkçe", NativeName = "Türkçe", Culture = "tr-TR", IsActive = true }
                );
        }
        private static void AddPages(ApplicationDbContext context)
        {
            context.AddRange(
                new Page { Title = "Merhaba", Slug = "merhaba", Body = "Hoşgeldiniz" }
                );
        }
        private static void AddMenus(ApplicationDbContext context)
        {
            menu = new Menu { Name = "name" };
            context.AddRange(menu

                );
        }
        private static void AddMenuItems(ApplicationDbContext context)
        {
            context.AddRange(
                new MenuItem { Name = "MenuLocname", Url = "url", Menu = menu }
                );
        }
        private static void AddMenuLocations(ApplicationDbContext context)
        {
            context.AddRange(
                new MenuLocation { Name = "MenuLocname", Menu = menu }
                );
        }

        private static void AddUsers(ApplicationDbContext context, UserManager<ApplicationUser> _userManager)
        {
            user = new ApplicationUser { Id = Guid.NewGuid(), UserName = "cmscore@gmail.com", Email = "cmscore@gmail.com", EmailConfirmed = true, NormalizedEmail = "CMSCORE@GMAIL.COM", NormalizedUserName = "CMSCORE@GMAIL.COM" };
            var task1 = Task.Run(() => _userManager.CreateAsync(user, "Cms123+"));
            task1.Wait();
        }

        private static void AddRoles(RoleManager<Role> _roleManager)
        {
           
            var role = new Role { Id = Guid.NewGuid(), Name = "ADMIN", NormalizedName = "ADMIN", ConcurrencyStamp = "Yönetici" };
            var task1 =Task.Run(()=>_roleManager.CreateAsync(role));
            task1.Wait();

            role = new Role { Id = Guid.NewGuid(), Name = "SLIDER", NormalizedName = "SLIDER", ConcurrencyStamp = "Slayt" };
            task1 = Task.Run(() => _roleManager.CreateAsync(role));
            task1.Wait();

            role = new Role { Id = Guid.NewGuid(), Name = "MENU", NormalizedName = "MENU", ConcurrencyStamp = "Menü" };
            task1 = Task.Run(() => _roleManager.CreateAsync(role));
            task1.Wait();

            role = new Role { Id = Guid.NewGuid(), Name = "HOME", NormalizedName = "HOME", ConcurrencyStamp = "Anasayfa" };
            task1 = Task.Run(() => _roleManager.CreateAsync(role));
            task1.Wait();


            role = new Role { Id = Guid.NewGuid(), Name = "FORM", NormalizedName = "FORM", ConcurrencyStamp = "Form" };
            task1 = Task.Run(() => _roleManager.CreateAsync(role));
            task1.Wait();

            role = new Role { Id = Guid.NewGuid(), Name = "GALLERY", NormalizedName = "GALLERY", ConcurrencyStamp = "Galeri" };
            task1 = Task.Run(() => _roleManager.CreateAsync(role));
            task1.Wait();

            role = new Role { Id = Guid.NewGuid(), Name = "MEDIA", NormalizedName = "MEDIA", ConcurrencyStamp = "Medya" };
            task1 = Task.Run(() => _roleManager.CreateAsync(role));
            task1.Wait();

        }
        private static void AddRoleToUser(UserManager<ApplicationUser> _userManager)
        {
            var task1 = Task.Run(() => _userManager.AddToRoleAsync(user, "ADMIN"));
            task1.Wait();
        }
    }
}
