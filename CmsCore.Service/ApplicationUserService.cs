using CmsCore.Data;
using CmsCore.Data.Infrastructure;
using CmsCore.Data.Repositories;
using CmsCore.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CmsCore.Service
{

    public interface IApplicationUserService
    {
        IEnumerable<ApplicationUser> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords);
        List<Role> GetAllRole();
        void DeleteRolesAsync(IEnumerable<string> deleteList, string userId);
    }
    public class ApplicationUserService:IApplicationUserService
    {
        private readonly IApplicationUserRepository applicationUserRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<Role> roleManager;

        public ApplicationUserService(IApplicationUserRepository applicationUserRepository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.applicationUserRepository = applicationUserRepository;
            this.unitOfWork = unitOfWork;
            this.roleManager = roleManager;
        }
        #region
        public IEnumerable<ApplicationUser> Search(string search, int sortColumnIndex, string sortDirection, int displayStart, int displayLength, out int totalRecords, out int totalDisplayRecords)
        {
            var application = applicationUserRepository.Search(search, sortColumnIndex, sortDirection, displayStart, displayLength, out totalRecords, out totalDisplayRecords);
            return application;
        }
        public List<Role> GetAllRole()
        {
            var allrole = roleManager.Roles.ToList<Role>();
            //IEnumerable<string> allRole = (IEnumerable<string>)roleManager.Roles.ToList();
            return allrole;
        }
        public async void DeleteRolesAsync(IEnumerable<string> deleteList, string email)
        {
            ApplicationUser user = await userManager.FindByEmailAsync(email);

            if (email != null)
            {
                foreach (var roleName in deleteList)
                {
                    //var result=await userManager.RemoveFromRolesAsync(user, deleteList);
                    var result = await userManager.RemoveFromRoleAsync(user, "ADMIN");
                    if(result.Succeeded)
                    {
                        await userManager.UpdateAsync(user);
                    }
                }
            }
        }
        #endregion
    }
}
