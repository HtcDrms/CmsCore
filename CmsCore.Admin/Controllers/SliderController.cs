using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CmsCore.Service;
using CmsCore.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using CmsCore.Model.Entities;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CmsCore.Admin.Controllers
{
    public class SliderController : BaseController
    {
        private readonly ISlideService slideService;
        private readonly ISliderService sliderService;
        public SliderController(ISlideService slideService, ISliderService sliderService)
        {
            this.slideService = slideService;
            this.sliderService = sliderService;
        }
        public IActionResult Index()
        {
            var slider = sliderService.GetSliders();
            return View(slider);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SliderViewModel slider)
        {
            if (ModelState.IsValid)
            {
                Slider svm = new Slider();
                svm.Name = slider.Name;
                svm.IsPublished = slider.IsPublished;
                svm.Id = slider.Id;
                svm.ModifiedBy = User.Identity.Name ?? "username";
                svm.ModifiedDate = DateTime.Now;
                svm.AddedBy = User.Identity.Name ?? "username";
                svm.AddedDate = DateTime.Now;
                sliderService.CreateSlider(svm);
                sliderService.SaveSlider();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Edit(long id)
        {
            ViewBag.Slider = new SelectList(sliderService.GetSliders(), "Id", "Name");
            var slider = sliderService.GetSlider(id);
            SliderViewModel svm = new SliderViewModel();
            svm.Name = slider.Name;
            svm.IsPublished = slider.IsPublished;
            svm.Id = slider.Id;
            svm.ModifiedBy = slider.ModifiedBy;
            svm.ModifiedDate = slider.ModifiedDate;
            svm.AddedBy = slider.AddedBy;
            svm.AddedDate = slider.AddedDate;
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
                svm.ModifiedBy = User.Identity.Name ?? "username";
                svm.ModifiedDate = DateTime.Now;
                svm.AddedBy = slider.AddedBy;
                svm.AddedDate = slider.AddedDate;
                sliderService.UpdateSlider(svm);
                sliderService.SaveSlider();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(long id)
        {
            var sld = sliderService.GetSlider(id);
            if (sld != null)
            {
                var frmfield = sliderService.GetSlideBySliderId(sld.Id);
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
