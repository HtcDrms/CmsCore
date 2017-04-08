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
            context.SaveChanges();
            AddPages(context);
            AddMenuLocations(context);
            context.SaveChanges();
            AddMenus(context);
            context.SaveChanges();
            AddMenuItems(context);
            context.SaveChanges();
            AddSliders(context);
            context.SaveChanges();
            AddSlides(context);
            context.SaveChanges();
            AddForms(context);
            context.SaveChanges();
            AddFormFields(context);
            context.SaveChanges();
            AddUsers(context, _userManager);
            AddRoles(_roleManager);
            AddRoleToUser(_userManager);
            // Save changes and release resources
            context.SaveChangesAsync();
            context.Dispose();
        }
        
        static ApplicationUser user;
        
        private static void AddLanguages(ApplicationDbContext context)
        {
            context.AddRange(
                new Language { Name = "Türkçe", NativeName = "Türkçe", Culture = "tr-TR", IsActive = true, AddedBy="username", AddedDate=DateTime.Now, ModifiedBy="username", ModifiedDate=DateTime.Now }
                );
        }
        private static void AddPages(ApplicationDbContext context)
        {
            context.AddRange(
                new Page { Title = "Anasayfa", Slug = "anasayfa", LanguageId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }
                );
        }
        private static void AddMenuLocations(ApplicationDbContext context)
        {
            context.AddRange(
                new MenuLocation { Name = "Primary", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }
                );
        }
        private static void AddMenus(ApplicationDbContext context)
        {
            var menu = new Menu { Name = "Ana Menü", MenuLocationId = 1, LanguageId = 1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now };
            context.AddRange(menu);
            context.MenuLocations.FirstOrDefault(f => f.Id == 1).Menu = menu;
        }
        private static void AddMenuItems(ApplicationDbContext context)
        {
            context.AddRange(
                new MenuItem { Name = "Hakkımızda", Url = "/hakkimizda", MenuId = 1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Farkımız", Url = "/farkimiz", MenuId = 1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Eğitim Modeli", Url = "/egitim-modeli", MenuId = 1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Kampüs", Url = "/kampus", MenuId = 1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "İletişim", Url = "/iletisim", MenuId = 1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now });
            context.SaveChanges();
            context.AddRange (
                new MenuItem { Name = "Kurumsal", Url = "/kurumsal", MenuId = 1, ParentMenuItemId=1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Vizyon Misyon", Url = "/vizyon-misyon", MenuId = 1, ParentMenuItemId=1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Kadromuz", Url = "/kadromuz", MenuId = 1, ParentMenuItemId=1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Yönetim Kurulumuz", Url = "/yonetim-kurulumuz", MenuId = 1, ParentMenuItemId=1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "İngilizce Eğitimleri", Url = "/ingilizce-egitimleri", MenuId = 1, ParentMenuItemId=2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Bilişim Eğitimleri", Url = "/bilisim-egitimleri", MenuId = 1, ParentMenuItemId=2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Kişisel Gelişim", Url = "/kisisel-gelisim", MenuId = 1, ParentMenuItemId = 2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Sanat Eğitimleri", Url = "/sanat-egitimleri", MenuId = 1, ParentMenuItemId = 2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Spor Eğitimi", Url = "/spor-egitimi", MenuId = 1, ParentMenuItemId = 2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Kurullar", Url = "/kurullar", MenuId = 1, ParentMenuItemId = 2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Okul Öğrenci Konseyi", Url = "/okul-ogrenci-konseyi", MenuId = 1, ParentMenuItemId = 2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "A+5B Eğitim Modeli", Url = "/a-5b-egitim-modeli", MenuId = 1, ParentMenuItemId = 3, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Anaokulu", Url = "/anaokulu", MenuId = 1, ParentMenuItemId = 3, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "İlkokul", Url = "/ilkokul", MenuId = 1, ParentMenuItemId = 3, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Ortaokul", Url = "/ortaokul", MenuId = 1, ParentMenuItemId = 3, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Lise", Url = "/lise", MenuId = 1, ParentMenuItemId = 3, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Derslikler", Url = "/derslikler", MenuId = 1, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "İngilizce Laboratuvarı", Url = "/ingilizce-laboratuvari", MenuId = 1, ParentMenuItemId=4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Bilişim Laboratuvarı", Url = "/bilisim-laboratuvarı", MenuId = 1, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Fen Bilimleri Laboratuvarı", Url = "/fen-bilimleri-laboratuvari", MenuId = 1, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Müzik Atölyesi", Url = "/muzik-atolyesi", MenuId = 1, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Güzel Sanatlar Atölyesi", Url = "/guzel-sanatlar-atolyesi", MenuId = 1, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Spor Salonu", Url = "/spor-salonu", MenuId = 1, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Kütüphane", Url = "/kutuphane", MenuId = 1, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Yemekhane", Url = "/yemekhane", MenuId = 1, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Bahçe", Url = "/bahce", MenuId = 1, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Bize Ulaşın", Url = "/bize-ulasin", MenuId = 1, ParentMenuItemId = 5, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Ön Kayıt", Url = "/on-kayit", MenuId = 1, ParentMenuItemId = 5, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Online Veli Görüşmesi", Url = "/online-veli-gorusmesi", MenuId = 1, ParentMenuItemId = 5, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Veli-Öğrenci El Kitabı", Url = "/veli-ogrenci-el-kitabi", MenuId = 1, ParentMenuItemId = 5, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Anket", Url = "/anket", MenuId = 1, ParentMenuItemId = 5, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }
                );
        }

        private static void AddSliders(ApplicationDbContext context)
        {
            context.AddRange(
                new Slider { Name = "Anasayfa Slider", IsPublished=true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }
                );
        }

        private static void AddSlides(ApplicationDbContext context)
        {
            context.AddRange(
                new Slide { Title="Video", SliderId=1, Video= "13036596003383465.mov", CallToActionUrl="", IsPublished=true, DisplayTexts=false, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Slide { Title = "Image", SliderId=1, Photo = "946468297image-slider-2.jpg", CallToActionUrl = "#", IsPublished = true, DisplayTexts = false, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }
                );
        }

        private static void AddForms(ApplicationDbContext context)
        {
            context.AddRange(
                new Form { FormName = "Sizi Arayalım", EmailTo="mdemirci@outlook.com", LanguageId=1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }
                );
        }

        private static void AddFormFields(ApplicationDbContext context)
        {
            context.AddRange(
                new FormField { Name = "Ad Soyad", FormId=1, FieldType=FieldType.fullName, Position=1, Required=true, Value="", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new FormField { Name = "E-posta", FormId = 1, FieldType = FieldType.email, Position = 2, Required = true, Value = "", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new FormField { Name = "Telefon", FormId = 1, FieldType = FieldType.telephone, Position = 3, Required = true, Value = "", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new FormField { Name = "Çocuğunuzu kaydettirmeyi düşündüğünüz okul aşağıdakilerden hangisidir?", FormId = 1, FieldType = FieldType.radioButtons, Position = 4, Required = true, Value = "Anaokulu,İlkokul,Ortaokul,Lise", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new FormField { Name = "Çocuğunuzu kaydettirmeyi düşündüğünüz sınıf hangisidir?", FormId = 1, FieldType = FieldType.dropdownMenu, Position = 5, Required = true, Value = "1,2,3,4", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new FormField { Name = "Abonelik", FormId = 1, FieldType = FieldType.checkbox, Position = 6, Required = true, Value = "Bilgi Koleji Okullarından gönderilen her türlü haber&#44; bilgi ve tanıtım içeriklerinden e-posta adresim ve telefonum aracılığıyla haberdar olmak istiyorum.", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }
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
            string[] roles = { "ADMIN", "SLIDER", "MENU", "HOME", "FORM", "GALLERY", "MEDIA", "PAGE", "POST", "LINK", "SEO", "SETTING","PRODUCT" };
            string[] stamp = { "Yönetici", "Slayt", "Menü", "Anasayfa", "Formlar", "Galeri", "Medya", "Sayfalar", "Gönderiler", "Bağlantılar", "SEO", "Ayarlar","Ürün" };

            for (int i = 0; i < roles.Count(); i++)
            {
                var role = new Role { Id = Guid.NewGuid(), Name = roles[i], NormalizedName = roles[i], ConcurrencyStamp = stamp[i]};
                var task1 = Task.Run(() => _roleManager.CreateAsync(role));
                task1.Wait();
            }            
        }
        private static void AddRoleToUser(UserManager<ApplicationUser> _userManager)
        {
            var task1 = Task.Run(() => _userManager.AddToRoleAsync(user, "ADMIN"));
            task1.Wait();
        }
    }
}
