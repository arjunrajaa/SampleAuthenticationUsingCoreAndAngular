using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessors
{
    public partial class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Uid { get; set; }
        public string UserName { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        public int RoleId { get; set; }
        public DateTime DateOfBirth { get; set; }

        [ForeignKey("RoleId")]
        public virtual Roles Roles { get; set; }
    }
}
