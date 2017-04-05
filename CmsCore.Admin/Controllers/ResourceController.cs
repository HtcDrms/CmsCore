using CmsCore.Admin.Models;
using CmsCore.Model.Entities;
using CmsCore.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CmsCore.Admin.Controllers
{
    public class ResourceController : BaseController
    {

        // GET: Resource
        private readonly IResourceService resourceService;
        private readonly ILanguageService languageService;
        public ResourceController(IResourceService resourceService, ILanguageService languageService)
        {
            this.resourceService = resourceService;
            this.languageService = languageService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            ViewBag.Languages = new SelectList(languageService.GetLanguages(), "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ResourceViewModel resource)
        {
            if (resource != null)
            {
                var rs = new Resource();
                rs.AddedBy = User.Identity.Name ?? "username";
                rs.AddedDate = DateTime.Now;
                rs.ModifiedBy = User.Identity.Name ?? "username";
                rs.ModifiedDate = DateTime.Now;
                rs.Name = resource.Name;
                rs.LanguageId = resource.LanguageId;
                rs.Value = resource.Value;
                resourceService.CreateResource(rs);
                resourceService.SaveResource();
                return RedirectToAction("Index");
            }
            ViewBag.Languages = new SelectList(languageService.GetLanguages(), "Id", "Name");
            return View(resource);
        }
        public ActionResult Delete(int? id)
        {
            if (id.HasValue)
            {
                var resource = resourceService.GetResource(id.Value);
                if (resource != null)
                {
                    resourceService.DeleteResource(resource);
                    resourceService.SaveResource();
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var resource = resourceService.GetResource(id);
            ViewBag.Languages = new SelectList(languageService.GetLanguages(), "Id", "Name", resource.LanguageId);
            if (resource != null)
            {
                var rs = new ResourceViewModel();
                rs.AddedBy = User.Identity.Name ?? "username";
                rs.AddedDate = DateTime.Now;
                rs.ModifiedBy = User.Identity.Name ?? "username";
                rs.ModifiedDate = DateTime.Now;
                rs.Name = resource.Name;
                rs.LanguageId = resource.LanguageId;
                rs.Value = resource.Value;

                return View(rs);
            }
            return View(resource);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ResourceViewModel rsv)
        {
            if (ModelState.IsValid)
            {
                var rs = resourceService.GetResource(rsv.Id);
                rs.AddedBy = User.Identity.Name ?? "username";
                rs.AddedDate = DateTime.Now;
                rs.ModifiedBy = User.Identity.Name ?? "username";
                rs.ModifiedDate = DateTime.Now;
                rs.Name = rsv.Name;
                rs.LanguageId = rsv.LanguageId;
                rs.Value = rsv.Value;
                resourceService.UpdateResource(rs);
                resourceService.SaveResource();
                return RedirectToAction("Index");
            }

            return View(rsv);
        }

        public IActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            string sSearch = "";
            if (param.sSearch != null) sSearch = param.sSearch;
            var sortColumnIndex = Convert.ToInt32(Request.Query["iSortCol_0"]);
            var sortDirection = Request.Query["sSortDir_0"]; // asc or desc
            int iTotalRecords;
            int iTotalDisplayRecords;
            var displayedPages = resourceService.Search(sSearch, sortColumnIndex, sortDirection, param.iDisplayStart, param.iDisplayLength, out iTotalRecords, out iTotalDisplayRecords);


            var result = from p in displayedPages
                         select new[] {
                             p.Id.ToString(),
                             p.Name.ToString(),
                             p.AddedBy.ToString(),
                             p.AddedDate.ToString(),
                             string.Empty };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = iTotalRecords,
                iTotalDisplayRecords = iTotalDisplayRecords,
                aaData = result.ToList()
            });
        }

    }
}
