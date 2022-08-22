using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Prezentovanje")]
    public class Prezentovanje
    {
        [Key]
        public int ID { get; set; }
        
        public Kompanija Kompanija { get; set; }
        
        public DateTime Datum { get; set; }

        public Sala Sala { get; set; }
        
        public Sajam Sajam { get; set; }
    }
}