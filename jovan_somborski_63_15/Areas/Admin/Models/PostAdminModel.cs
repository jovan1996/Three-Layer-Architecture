using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using biznis.BussinessLayer.DTO;
using biznis.DataLayer;
namespace jovan_somborski_63_15.Areas.Admin.Models
{
    public class PostAdminModel
    {


       
        public string heading { get; set; }
        public string paragraph { get; set; }    
        public Nullable<int> idImg { get; set; }
        public CategoryDTO Category { get; set; }
        public string ImageAbout { get; set; }
        public string paragraph2 { get; set; }
        public HttpPostedFileBase slika { get; set; }
        public HttpPostedFileBase putanja { get; set; }
        public Image slika1 { get; set; }
    }
}