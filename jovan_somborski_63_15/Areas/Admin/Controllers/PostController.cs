using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biznis;
using biznis.BussinessLayer.Operations;
using biznis.BussinessLayer.DTO;
using jovan_somborski_63_15.Models.Application;
using jovan_somborski_63_15.Areas.Admin.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace jovan_somborski_63_15.Areas.Admin.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private OperationManager _manager = OperationManager.Singleton;
        // GET: Admin/Post
        public ActionResult Index()
        {
            OpPostBase op = new OpPostBase();

            if (!User.IsInRole("admin"))
            {
                op.Criteria.Uuid = User.Identity.GetUserId();
            }
            var posts = _manager.ExecuteOperation(op);
            return View(posts.Items as PostDTO[]);
        }

       

        // GET: Admin/Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Post/Create
        [HttpPost]
        public ActionResult Create(PostAdminModel vm)
        {
            try
            {
                string id_korisnik = User.Identity.GetUserId();
                string FileName = Guid.NewGuid().ToString() + "_" + vm.slika.FileName;
                string putanja = Path.Combine(Server.MapPath("~/Content/Images"), FileName);
                vm.slika.SaveAs(putanja);
                PostDTO post = new PostDTO
                {
                    heading=vm.heading,
                    Uuid= id_korisnik,
                    paragraph=vm.paragraph,
                    ImageAbout=vm.ImageAbout,
                    paragraph2=vm.paragraph2,
                    src= FileName

                };


                OpPostInsert op = new OpPostInsert();
                op.DTO = post;
                OperationResult res = _manager.ExecuteOperation(op);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Admin/Post/Delete/5
        public ActionResult Delete(int id)
        {
            var dto = getInstance(id);
            return View(dto);
        }

        // POST: Admin/Post/Delete/5
        [HttpPost]
        public ActionResult Delete(PostDTO dto)
        {
            OpPostDelete delete = new OpPostDelete();
            delete.idzabriranje = dto.Id;
            var result = _manager.ExecuteOperation(delete);
            return RedirectToAction("Index");
        }
        private PostDTO getInstance(int id)
        {
            OpPostBase op = new OpPostBase();
            op.Criteria.Id = id;
            var result = _manager.ExecuteOperation(op);
            PostDTO dto = result.Items[0] as PostDTO;
            return dto;
        }
    }
}
