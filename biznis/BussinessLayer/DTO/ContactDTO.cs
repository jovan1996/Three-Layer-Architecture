using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biznis.BussinessLayer.DTO
{
    public class ContactDTO : BaseDTO
    {
    

        public string message { get; set; }
        public System.DateTime? date { get; set; }
        public  string User { get; set; }
    }
}
