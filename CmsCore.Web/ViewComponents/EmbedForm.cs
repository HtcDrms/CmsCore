using CmsCore.Model.Entities;
using CmsCore.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Web.ViewComponents
{
    public class EmbedForm:ViewComponent
    {
        private readonly IFormService formService;
        
        public EmbedForm(IFormService formService)
        {
            this.formService = formService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string name,string template)
        {
            var form = await GetForm(name,template);
            if (form == null)
            {
                form = new Form();
            }
            return View(template,form);
          
        }
        private Task<CmsCore.Model.Entities.Form> GetForm(string formName,string template)
        {           
           return Task.FromResult(formService.GetForm(formName));
        }
    }
}
 