using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Mentor")]
    public class Mentor
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(20)]
        [Required]
        public string Ime { get; set; }

        [Required]
        [MaxLength(20)]
        public string Prezime { get; set; }

        public List<Task> Taskovi { get; set; }

    }
}