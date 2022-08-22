using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Sajam")]
    public class Sajam
    {
        
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(200)]
        [RegularExpression(@"\w+")]
        public string Naziv { get; set; }

        [Range(1,10)]
        public int BrojSala { get; set; }
       
        public List<Kompanija> Kompanije { get; set; }
        
        public List<Prezentovanje> Raspored { get; set; }
        
        public List<Sala> Sale { get; set; }

    }
}