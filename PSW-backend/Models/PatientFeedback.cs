using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PSW_backend.Models
{
    [Table("PatientFeedback")]
    public class PatientFeedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(200)]
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public bool IsPosted { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }  
        public virtual Patient Patient { get; set; }
    }
}
