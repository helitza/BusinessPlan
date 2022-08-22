using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Task")]
    public class Task
    {
        [Key]
        public int ID { get; set; }
        
        [Required]
        [RegularExpression(@"\w+")]
        [MaxLength(50)]
        public string Pozicija { get; set; }

        public Mentor Mentor { get; set; } 

        public Kompanija Kompanija { get; set; }

    }
}