using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("Sala")]
    public class Sala
    {
        [Key]
        public int ID { get; set; }

        public Sajam Sajam { get; set; }

        public string Naziv { get; set; }
        
        public List<Prezentovanje> Prezentovanja { get; set; }
    }
}