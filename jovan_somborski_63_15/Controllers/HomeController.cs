using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biznis;
using biznis.BussinessLayer.Operations;
using biznis.BussinessLayer.DTO;
using jovan_somborski_63_15.Models.Application;
using Microsoft.AspNet.Identity;

namespace jovan_somborski_63_15.Controllers
{
    public class HomeController : Controller
    {
        private OperationManager _manager = OperationManager.Singleton;

        public ActionResult Index()
        {
            OpPostBase op = new OpPostBase();
            var postovi = _manager.ExecuteOperation(op);

            OpCategoriesBase op3 = new OpCategoriesBase();
            var categories = _manager.ExecuteOperation(op3);

            OpHeaderBase op2 = new OpHeaderBase();
            var header = _manager.ExecuteOperation(op2);

            HomeViewModel home = new HomeViewModel
            {
                Posts = (postovi.Items as PostDTO[]).ToList(),
                Categories = (categories.Items as CategoryDTO[]).ToList(),
                Broj= postovi.Broj,
                Header = header.Items[0] as HeaderDTO,
                HotPost = postovi.Items[0] as PostDTO
            };
                return View(home);
           
        }

        public ActionResult Show(int id)
        {
            OpPostBase op = new OpPostBase();
            op.Criteria.Id = id;
            
            var postovi = _manager.ExecuteOperation(op);

            OpCategoriesBase op3 = new OpCategoriesBase();
            var categories = _manager.ExecuteOperation(op3);

            OpHeaderBase op2 = new OpHeaderBase();
            var header = _manager.ExecuteOperation(op2);

            HomeViewModel home = new HomeViewModel
                {
                Posts = (postovi.Items as PostDTO[]).ToList(),
                Categories = (categories.Items as CategoryDTO[]).ToList(),
                Broj = postovi.Broj,
                Header = header.Items[0] as HeaderDTO
               
            };
                return View(home);            
        }

        [HttpGet]
        public JsonResult Categories(int id)
        {

            OpPostCategoryFilter op = new OpPostCategoryFilter();

            op.Criteria.Id = id;
            var post = _manager.ExecuteOperation(op);

            return Json(post, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Pages(int id)
        {
            OpPostBasePaginationFilter op = new OpPostBasePaginationFilter();

            op.Criteria.SkipFilter = id;

            var posts = _manager.ExecuteOperation(op);

            return Json(posts, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult Categoriesfilter(int page, int category)
        {

            OpPostCategoryPaginateFilter op = new OpPostCategoryPaginateFilter();

            op.Criteria.Id = category;
            op.Criteria.SkipFilter = page;

            var posts = _manager.ExecuteOperation(op);

            return Json(posts, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Contact()
        {
         
            return View();
        }


        public ActionResult Autor()
        {

            return View();
        }
    }
}
