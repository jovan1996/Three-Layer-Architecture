using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace biznis.BussinessLayer.DTO
{
    public class UserDTO : BaseDTO
    {      
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

    }
}
