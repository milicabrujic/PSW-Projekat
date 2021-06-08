using PSW_backend.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Models
{
    [Table("User")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(30)]
        public string Surname { get; set; }

        [MaxLength(30)]
        public string Username { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [MaxLength(30)]
        public string Password { get; set; }

        [MaxLength(50)]
        public string Address { get; set; }
            
        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        public Roles Role { get; set; }

        public bool IsBlocked { get; set; }

        public bool IsMalicious { get; set; }

        public String AuthenticationToken { get; set; }
    }
}
