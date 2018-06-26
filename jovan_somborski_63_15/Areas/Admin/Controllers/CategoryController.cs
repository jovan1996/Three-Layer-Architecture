using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biznis;
using biznis.BussinessLayer.Operations;
using biznis.BussinessLayer.DTO;
using jovan_somborski_63_15.Models.Application;

namespace jovan_somborski_63_15.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private OperationManager _manager = OperationManager.Singleton;

        // GET: Admin/Category
        public ActionResult Index()
        {
            OpCategoriesBase op = new OpCategoriesBase();

            var items = _manager.ExecuteOperation(op);

            return View(items.Items as CategoryDTO[]);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int id)
        {
            var dto = getInstance(id);
            return View(dto);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        [HttpPost]
        public ActionResult Create(CategoryDTO dto)
        {
            OpCategoryInsert op = new OpCategoryInsert();
            op.Cat = dto;
            var result = _manager.ExecuteOperation(op);

            if (result.Status)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Create");
            }

        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            var dto = getInstance(id);
            return View(dto);
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(CategoryDTO dto)
        {

            OpCategoryUpdate op = new OpCategoryUpdate();
            op.Cat = dto;
            var result = _manager.ExecuteOperation(op);
            return RedirectToAction("Index");
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int id)
        {
            var dto = getInstance(id);
            return View(dto);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost]
        public ActionResult Delete(CategoryDTO dto)
        {
            OpCategoryDelete delete = new OpCategoryDelete();
            delete.IdzaBrisanje = dto.Id;
            var result = _manager.ExecuteOperation(delete);
            return RedirectToAction("Index");
        }

        private CategoryDTO getInstance(int id)
        {
            OpCategoriesBase op = new OpCategoriesBase();
            op.Criteria.Id = id;
            var result = _manager.ExecuteOperation(op);
            CategoryDTO dto = result.Items[0] as CategoryDTO;
            return dto;
        }
    }
}
