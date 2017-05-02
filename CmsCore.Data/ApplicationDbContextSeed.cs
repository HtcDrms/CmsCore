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
            // Look for any pages.
            if (context.Languages.Any())
            {
                return;   // DB has been seeded
            }
            // Perform seed operations
            AddLanguages(context);
            AddSetting(context);
            context.SaveChanges();
            AddTemplates(context);
            context.SaveChanges();
            AddPages(context);
            AddPostCategories(context);
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
                new Page { Title = "Anasayfa", Slug = "anasayfa", TemplateId=3,LanguageId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Page { Title = "Haberler", Slug = "haberler", TemplateId = 6, LanguageId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Page { Title = "Blog", Slug = "blog", TemplateId = 7, LanguageId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Page { Title = "Ön Kayıt Formu", Slug = "on-kayit-formu", TemplateId = 10, LanguageId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Page { Title = "İş Başvuru Formu", Slug = "is-basvuru-formu", TemplateId = 11, LanguageId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Page { Title = "Arama", Slug = "arama", TemplateId = 12, LanguageId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Page { Title = "Anket", Slug = "anket", TemplateId = 13, LanguageId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Page { Title = "Galeri", Slug = "galeri", TemplateId = 14, LanguageId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Page { Title = "Site Haritası", Slug = "site-haritasi", TemplateId = 15, LanguageId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }

                );
        }
        private static void AddPostCategories(ApplicationDbContext context)
        {
            context.AddRange(
                new PostCategory { Name = "Haberler", Slug="haberler", LanguageId=1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new PostCategory { Name = "Kadromuz", Slug = "kadromuz", LanguageId = 1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }
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
                new MenuItem { Name = "Hakkımızda", Url = "/hakkimizda", Position = 1, IsPublished = true, MenuId = 1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Farkımız", Url = "/farkimiz", Position = 2, MenuId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Eğitim Modeli", Url = "/egitim-modeli", Position = 3, IsPublished = true, MenuId = 1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Kampüs", Url = "/kampus", Position = 4, MenuId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "İletişim", Url = "/iletisim", Position = 5, MenuId = 1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now });
            context.SaveChanges();
            context.AddRange (
                new MenuItem { Name = "Kurumsal", Url = "/kurumsal", Position=1, MenuId = 1, IsPublished = true, ParentMenuItemId =1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Vizyon Misyon", Url = "/vizyon-misyon", Position = 2, IsPublished = true, MenuId = 1, ParentMenuItemId=1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Kadromuz", Url = "/kadromuz", MenuId = 1, Position = 3, IsPublished = true, ParentMenuItemId =1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Yönetim Kurulumuz", Url = "/yonetim-kurulumuz", MenuId = 1, IsPublished = true, Position = 4, ParentMenuItemId =1, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "İngilizce Eğitimleri", Url = "/ingilizce-egitimleri", MenuId = 1, IsPublished = true, Position = 5, ParentMenuItemId =2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Bilişim Eğitimleri", Url = "/bilisim-egitimleri", MenuId = 1, IsPublished = true, Position = 6, ParentMenuItemId =2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Kişisel Gelişim", Url = "/kisisel-gelisim", MenuId = 1, IsPublished = true, Position = 7, ParentMenuItemId = 2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Sanat Eğitimleri", Url = "/sanat-egitimleri", MenuId = 1, IsPublished = true, Position = 8, ParentMenuItemId = 2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Spor Eğitimi", Url = "/spor-egitimi", MenuId = 1, IsPublished = true, Position = 9, ParentMenuItemId = 2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Kurullar", Url = "/kurullar", MenuId = 1, IsPublished = true, Position = 10, ParentMenuItemId = 2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Okul Öğrenci Konseyi", Url = "/okul-ogrenci-konseyi", Position = 11, IsPublished = true, MenuId = 1, ParentMenuItemId = 2, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "A+5B Eğitim Modeli", Url = "/a-5b-egitim-modeli", Position = 12, IsPublished = true, MenuId = 1, ParentMenuItemId = 3, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Anaokulu", Url = "/anaokulu", MenuId = 1, Position = 13, IsPublished = true, ParentMenuItemId = 3, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "İlkokul", Url = "/ilkokul", MenuId = 1, Position = 14, IsPublished = true, ParentMenuItemId = 3, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Ortaokul", Url = "/ortaokul", MenuId = 1, Position = 15, IsPublished = true, ParentMenuItemId = 3, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Lise", Url = "/lise", MenuId = 1, Position = 16, IsPublished = true, ParentMenuItemId = 3, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Derslikler", Url = "/derslikler", Position = 17, MenuId = 1, IsPublished = true, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "İngilizce Laboratuvarı", Url = "/ingilizce-laboratuvari", Position = 18, IsPublished = true, MenuId = 1, ParentMenuItemId=4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Bilişim Laboratuvarı", Url = "/bilisim-laboratuvarı", MenuId = 1, Position = 19, IsPublished = true, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Fen Bilimleri Laboratuvarı", Url = "/fen-bilimleri-laboratuvari", Position = 20, IsPublished = true, MenuId = 1, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Müzik Atölyesi", Url = "/muzik-atolyesi", MenuId = 1, Position = 21, IsPublished = true, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Güzel Sanatlar Atölyesi", Url = "/guzel-sanatlar-atolyesi", MenuId = 1, IsPublished = true, Position = 22, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Spor Salonu", Url = "/spor-salonu", MenuId = 1, Position = 23, IsPublished = true, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Kütüphane", Url = "/kutuphane", MenuId = 1, Position = 24, IsPublished = true, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Yemekhane", Url = "/yemekhane", MenuId = 1, Position = 25, IsPublished = true, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Bahçe", Url = "/bahce", MenuId = 1, Position = 26, IsPublished = true, ParentMenuItemId = 4, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Bize Ulaşın", Url = "/bize-ulasin", MenuId = 1, Position = 27, IsPublished = true, ParentMenuItemId = 5, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Ön Kayıt", Url = "/on-kayit", MenuId = 1, Position = 28, IsPublished = true, ParentMenuItemId = 5, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Online Veli Görüşmesi", Url = "/online-veli-gorusmesi", MenuId = 1, Position = 29, IsPublished = true, ParentMenuItemId = 5, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Veli-Öğrenci El Kitabı", Url = "/veli-ogrenci-el-kitabi", MenuId = 1, Position = 30, IsPublished = true, ParentMenuItemId = 5, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new MenuItem { Name = "Anket", Url = "/anket", MenuId = 1, Position = 31, IsPublished = true, ParentMenuItemId = 5, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }
                );
        }
        private static void AddTemplates(ApplicationDbContext context)
        {
            context.AddRange(
                new Template { Name = "Default", ViewName="Default", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "Secondary", ViewName = "Secondary", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "Anasayfa", ViewName = "Index", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "Sayfa", ViewName = "Page", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "Yazı", ViewName = "Post", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "Yazılar", ViewName = "Posts", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "Blog", ViewName = "Blog", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "İletişim", ViewName = "Contact", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "İletişim Formu", ViewName = "ContactForm", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "Ön Kayıt Formu", ViewName = "PreRegistration", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "İş Başvuru Formu", ViewName = "JobRecourseForm", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "Arama", ViewName = "Search", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "Anket Formu", ViewName = "Survey", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "Galeri", ViewName = "Gallery", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Template { Name = "Site Haritası", ViewName = "SiteMap", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }






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
                new Slide { Title="Video", SliderId=1, Video= "http://assets.bilgikoleji.com/uploads/media/slide/13036596003383465.mov", CallToActionUrl="", IsPublished=true, DisplayTexts=false, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Slide { Title = "Image", SliderId=1, Photo = "http://assets.bilgikoleji.com/uploads/media/slide/946468297image-slider-2.jpg", CallToActionUrl = "#", IsPublished = true, DisplayTexts = false, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }
                );
        }
        private static void AddSetting(ApplicationDbContext context)
        {
            context.AddRange(
                new Setting { Name="Email",Value="ertyeni@gmail.com",AddedBy="username",AddedDate=DateTime.Now,ModifiedBy="username",ModifiedDate=DateTime.Now},
                new Setting { Name = "EmailPassword", Value = "###", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Setting { Name = "SmtpUserName", Value = "###", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Setting { Name = "SmtpPassword", Value = "###", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Setting { Name = "SmtpHost", Value = "###", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Setting { Name = "SmtpPort", Value = "###", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Setting { Name = "SmtpUseSSL", Value = "###", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Setting { Name = "GoogleAnalytics", Value = "#", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Setting { Name = "FooterScript", Value = "#", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Setting { Name = "HeaderScript", Value = "#", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Setting { Name = "MapLat", Value = "40.9891303", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new Setting { Name = "MapLon", Value = "29.0288929", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }
                );
        }

        private static void AddForms(ApplicationDbContext context)
        {
            context.AddRange(
                new Form { FormName = "Sizi Arayalım", EmailTo="ertyeni@gmail.com", LanguageId=1, IsPublished = true, AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now }
                );
        }

        private static void AddFormFields(ApplicationDbContext context)
        {
            context.AddRange(
                new FormField { Name = "Ad Soyad", FormId=1, FieldType=FieldType.fullName, Position=1, Required=true, Value="", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new FormField { Name = "E-posta", FormId = 1, FieldType = FieldType.email, Position = 2, Required = true, Value = "", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new FormField { Name = "Telefon", FormId = 1, FieldType = FieldType.telephone, Position = 3, Required = true, Value = "", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new FormField { Name = "Çocuğunuzu kaydettirmeyi düşündüğünüz okul aşağıdakilerden hangisidir?", FormId = 1, FieldType = FieldType.radioButtons, Position = 4, Required = true, Value = "Anaokulu,İlkokul,Ortaokul,Lise", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
                new FormField { Name = "Çocuğunuzu kaydettirmeyi düşündüğünüz sınıf hangisidir?", FormId = 1, FieldType = FieldType.dropdownMenu, Position = 5, Required = true, Value = "Seçiniz,1,2,3,4", AddedBy = "username", AddedDate = DateTime.Now, ModifiedBy = "username", ModifiedDate = DateTime.Now },
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
