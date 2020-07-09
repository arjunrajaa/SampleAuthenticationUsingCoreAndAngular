using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Accessors
{
    public class Roles
    {
        [Key]
        public int RoleId { get; set; }
        public string Role { get; set; }
    }
}
