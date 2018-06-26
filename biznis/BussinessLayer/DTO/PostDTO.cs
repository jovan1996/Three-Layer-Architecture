using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biznis.BussinessLayer.DTO
{
    public class PostDTO : BaseDTO
    {

        public string heading { get; set; }
        public string paragraph { get; set; }
        public System.DateTime? publish { get; set; }
        public string ImageAbout { get; set; }
        public string paragraph2 { get; set; }

        public byte[] time { get; set; }
        public string username { get; set; }
        public int? postCount { get; set; }
        public string CategoryName { get; set; }
        public string src { get; set; }

    }
}
