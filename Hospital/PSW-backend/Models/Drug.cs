using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Models
{
    [Table("Drug")]
    public class Drug
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Amount { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
