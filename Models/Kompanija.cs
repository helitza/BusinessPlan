using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Kompanija")]
    public class Kompanija
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(200)]
        [RegularExpression(@"\w+")]
        [Required]
        public string Naziv { get; set; }

        [MaxLength(100)]
        [RegularExpression(@"\w+")]
        public string HR { get; set; }

        [MaxLength(20)]
        [RegularExpression(@"\w+")]
        public string Oblast { get; set; }
      
        [Range(1990,2022)]
        public int GodinaOsnivanja { get; set; }
        
        public Sajam Sajam { get; set; }
        
        public List<Task> Taskovi { get; set; }
        
        public List<Prezentovanje> Prezentovanja { get; set; }
    }
}