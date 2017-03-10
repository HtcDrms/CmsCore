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
    public class FormFieldController : BaseController
    {
        private readonly IFormService formService;
        private readonly IFormFieldService formFieldService;
        public FormFieldController(IFormService formService, IFormFieldService formFieldService)
        {
            this.formService = formService;
            this.formFieldService = formFieldService;
        }
        public IActionResult Create(long id)
        {
            FormFieldViewModel formFieldVM = new FormFieldViewModel();
            formFieldVM.FormId = id;
            ViewBag.Forms = new SelectList(formService.GetForms(), "Id", "FormName", id);
            ViewBag.FormFieldId = new SelectList(formFieldService.GetFormFields(), "Id", "Description");

            return View(formFieldVM);
        }
        [HttpPost]
        public IActionResult Create(FormFieldViewModel formField)
        {
            if (ModelState.IsValid)
            {
                var frm = new FormField();

                frm.FieldType = formField.FieldType;
                frm.FormId = (long)formField.FormId;
                frm.Value = formField.Value;
                frm.Required = formField.Required;
                frm.Name = formField.Name;

                var formfields = formService.GetFormFieldsByFormId((long)formField.FormId);
                frm.Position = formfields.Count + 1;

                frm.AddedBy = User.Identity.Name ?? "User";
                frm.AddedDate = DateTime.Now;
                frm.ModifiedBy = User.Identity.Name ?? "User";
                frm.ModifiedDate = DateTime.Now;

                formFieldService.CreateFormField(frm);
                formFieldService.SaveFormField();
                return RedirectToAction("Index");

            }
            return View();

        }
        public IActionResult Details(long id)
        {
            var frmField = formFieldService.GetFormField(id);
            if (frmField != null)
            {
                FormFieldViewModel formFieldVM = new FormFieldViewModel();
                formFieldVM.Id = frmField.Id;
                formFieldVM.Name = frmField.Name;
                formFieldVM.Position = frmField.Position;
                formFieldVM.Required = frmField.Required;
                formFieldVM.Value = frmField.Value;
                formFieldVM.FormId = frmField.FormId;
                formFieldVM.FieldType = frmField.FieldType;
                formFieldVM.ModifiedDate = frmField.ModifiedDate;
                formFieldVM.ModifiedBy = frmField.ModifiedBy;
                formFieldVM.AddedBy = frmField.AddedBy;
                formFieldVM.AddedDate = frmField.AddedDate;

                var form = formFieldService.GetForms((long)frmField.FormId);
                ViewBag.Form = form.FormName;

                return View(formFieldVM);
            }
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit(long id)
        {
            FormField frmField = formFieldService.GetFormField(id);
            if (frmField != null)
            {
                FormFieldViewModel formFieldVM = new FormFieldViewModel();
                formFieldVM.Id = frmField.Id;
                formFieldVM.Name = frmField.Name;
                formFieldVM.Position = frmField.Position;
                formFieldVM.Required = frmField.Required;
                formFieldVM.Value = frmField.Value;
                formFieldVM.FormId = frmField.FormId;
                formFieldVM.FieldType = frmField.FieldType;
                formFieldVM.ModifiedDate = frmField.ModifiedDate;
                formFieldVM.ModifiedBy = frmField.ModifiedBy;
                formFieldVM.AddedBy = frmField.AddedBy;
                formFieldVM.AddedDate = frmField.AddedDate;

                ViewBag.FormField = new SelectList(formFieldService.GetFormFields(), "Id", "Name", frmField.Id);
                ViewBag.Forms = new SelectList(formService.GetForms(), "Id", "FormName", frmField.FormId);
                return View(formFieldVM);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FormFieldViewModel frmField)
        {
            ViewBag.Forms = new SelectList(formService.GetForms(), "Id", "FormName", frmField.FormId);
            ViewBag.FormFieldId = new SelectList(formFieldService.GetFormFields(), "Id", "Name", frmField.Id);
            if (ModelState.IsValid)
            {
                FormField formFieldVM = new FormField();
                formFieldVM.Id = frmField.Id;
                formFieldVM.Name = frmField.Name;
                formFieldVM.Position = frmField.Position;
                formFieldVM.Required = frmField.Required;
                formFieldVM.Value = frmField.Value;
                formFieldVM.FormId = frmField.FormId;
                formFieldVM.FieldType = frmField.FieldType;
                formFieldVM.ModifiedDate = DateTime.Now;
                formFieldVM.ModifiedBy = User.Identity.Name ?? "username";
                formFieldVM.AddedBy = User.Identity.Name ?? "username";
                formFieldVM.AddedDate = DateTime.Now;

                formFieldService.UpdateFormField(formFieldVM);
                formFieldService.SaveFormField();
                return RedirectToAction("Index");
            }
            return View(frmField);
        }

        public IActionResult Delete(long id)
        {
            var frmField = formFieldService.GetFormField(id);
            if (frmField != null)
            {
                var formFields = formService.GetFormFieldsByFormId((long)frmField.FormId);
                foreach (var formField in formFields)
                {
                    if (formField.Position > frmField.Position)
                    {
                        formField.Position = formField.Position - 1;
                    }
                    formFieldService.UpdateFormField(formField);
                }
                formFieldService.DeleteFormField(id);
                formFieldService.SaveFormField();
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
            long iTotalRecords;
            long iTotalDisplayRecords;
            var displayedForms = formFieldService.Search(sSearch, sortColumnIndex, sortDirection, param.iDisplayStart, param.iDisplayLength, out iTotalRecords, out iTotalDisplayRecords);
            var result = from c in displayedForms
                         select new[] {
                             c.Id.ToString(),
                             c.Name.ToString(),
                             c.FieldType.ToString(),
                             c.Required.ToString(),
                             c.Form.FormName.ToString(),
                             string.Empty };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = iTotalRecords,
                iTotalDisplayRecords = iTotalDisplayRecords,
                aaData = result.ToList()
            });
        }

        public void UpdateFormFieldPosition(string position1, string text1, string position2, string text2, long Id)
        {
            var fields = formService.GetFormFieldsByFormId(Id);
            foreach (var item in fields)
            {
                if (item.Name == text2)
                {
                    item.Position = Convert.ToInt32(position1);
                    formFieldService.UpdateFormField(item);
                    formFieldService.SaveFormField();
                }
                if (item.Name == text1)
                {
                    item.Position = Convert.ToInt32(position2);
                    formFieldService.UpdateFormField(item);
                    formFieldService.SaveFormField();
                }

            }
        }
    }
}

