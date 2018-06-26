using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using biznis.BussinessLayer.DTO;
namespace jovan_somborski_63_15.Models.Application
{
    public class HomeViewModel
    {
        public List<ContactDTO> Contacts { get; set; }
        public List<PostDTO> Posts { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public HeaderDTO Header { get; set; }
        public int Broj { get; set; }
        public PostDTO HotPost { get; set; }
    }
}