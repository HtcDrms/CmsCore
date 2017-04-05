using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CmsCore.Service;
using CmsCore.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CmsCore.Model.Entities;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CmsCore.Admin.Controllers
{
    [Authorize(Roles ="SLIDE,ADMIN")]
    public class SliderController : BaseController
    {
        private readonly ISlideService slideService;
        private readonly ISliderService sliderService;
        private readonly ITemplateService templateService;
        public SliderController(ISlideService slideService, ISliderService sliderService, ITemplateService templateService)
        {
            this.slideService = slideService;
            this.sliderService = sliderService;
            this.templateService = templateService;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Create()
        {
            SliderViewModel svm = new SliderViewModel();
            ViewBag.Templates = new SelectList(templateService.GetTemplates(), "Id", "Name", svm.TemplateId);
            return View(svm);
        }
        [HttpPost]
        public IActionResult Create(SliderViewModel slider)
        {
            if (ModelState.IsValid)
            {
                Slider svm = new Slider();
                svm.Name = slider.Name;
                svm.IsPublished = slider.IsPublished;
                svm.TemplateId = slider.TemplateId;
                
                sliderService.CreateSlider(svm);
                sliderService.SaveSlider();
                return RedirectToAction("Index");
            }
            ViewBag.Templates = new SelectList(templateService.GetTemplates(), "Id", "Name", slider.TemplateId);
            return View(slider);
        }

        public IActionResult Edit(long id)
        {
            
            var slider = sliderService.GetSlider(id);
            SliderViewModel svm = new SliderViewModel();
            svm.Name = slider.Name;
            svm.IsPublished = slider.IsPublished;
            svm.Id = slider.Id;
            svm.TemplateId = slider.TemplateId;
            ViewBag.Templates = new SelectList(templateService.GetTemplates(), "Id", "Name", slider.TemplateId);
            return View(svm);
        }

        [HttpPost]
        public IActionResult Edit(SliderViewModel slider)
        {
            if (ModelState.IsValid)
            {
                Slider svm = sliderService.GetSlider(slider.Id);
                svm.Name = slider.Name;
                svm.IsPublished = slider.IsPublished;
                svm.TemplateId = slider.TemplateId;
                sliderService.UpdateSlider(svm);
                sliderService.SaveSlider();
                return RedirectToAction("Index");
            }
            ViewBag.Templates = new SelectList(templateService.GetTemplates(), "Id", "Name", slider.TemplateId);
            return View(slider);
        }

        public ActionResult Delete(long id)
        {
            var sld = sliderService.GetSlider(id);
            if (sld != null)
            {
                var frmfield = sliderService.GetSlidesBySliderId(sld.Id);
                foreach (var item in frmfield)
                {
                    slideService.DeleteSlide(item.Id);
                }
                sliderService.SaveSlider();
                sliderService.DeleteSlider(id);
                sliderService.SaveSlider();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public IActionResult AjaxHandler(jQueryDataTableParamModel param)
        {
            string sSearch = "";
            if (param.sSearch != null) sSearch = param.sSearch;
            var sortColumnIndex = Convert.ToInt32(Request.Query["iSortCol_0"]);
            var sortDirection = Request.Query["sSortDir_0"]; // asc or desc
            int iTotalRecords;
            int iTotalDisplayRecords;
            var displayedPages = sliderService.Search(sSearch, sortColumnIndex, sortDirection, param.iDisplayStart, param.iDisplayLength, out iTotalRecords, out iTotalDisplayRecords);


            var result = from p in displayedPages
                         select new[] {
                             p.Id.ToString(),
                             p.Name.ToString(),
                             p.IsPublished.ToString(),
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
