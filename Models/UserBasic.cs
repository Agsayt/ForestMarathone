using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestMarathone.Models
{
    class UserBasic
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string email { get; set; }
        public Role RoleId { get; set; }

        public string RoleName { get; set; }

    }
}
