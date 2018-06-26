using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biznis.BussinessLayer.DTO;
using biznis.BussinessLayer.Operations;
using biznis.BussinessLayer;
using biznis;
using jovan_somborski_63_15.Models.Application;
using Microsoft.AspNet.Identity;

namespace jovan_somborski_63_15.Controllers
{
    public class ContactController : Controller
    {
        private OperationManager _manager = OperationManager.Singleton;

        // GET: Contact

        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            opContactBase op = new opContactBase();

            var result = _manager.ExecuteOperation(op);
                   
            return View((result.Items as ContactDTO[]).ToList());
        }

        // POST: Contact/Create
        [HttpPost]
        public ActionResult Create(ContactDTO dto)
        {
            try
            {
                opContactCreate op = new opContactCreate();
                ContactDTO c = new ContactDTO
                {
                    Uuid = User.Identity.GetUserId(),
                    message = dto.message
                };
                op.DTO = c;
                OperationResult res = _manager.ExecuteOperation(op);
            }
            catch
            {
                return View();
            }
          
            return RedirectToAction("Index","Home");
        }

        private ContactDTO getInstance(int id)
        {
            opContactBase op = new opContactBase();
            op.Criteria.Id = id;
            var result = _manager.ExecuteOperation(op);
            ContactDTO dto = result.Items[0] as ContactDTO;
            return dto;
        }



        // GET: Contact/Delete/5
        public ActionResult Delete(int id)
        {
            var result = getInstance(id);
            return View(result as ContactDTO);
        }

        // POST: Contact/Delete/5
        [HttpPost]
        public ActionResult Delete(ContactDTO dto)
        {
            try
            {
                OpContactDelete delete = new OpContactDelete();
                delete.IdzaBrisanje = dto.Id;
                var result = _manager.ExecuteOperation(delete);
                return RedirectToAction("Index");

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
