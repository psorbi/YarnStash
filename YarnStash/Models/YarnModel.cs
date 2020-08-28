using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YarnStash.Models
{
    public class YarnModel
    {
        public int id { get; set; } //required by database for primary key
        [DisplayName("Brand")]
        public string Manufacturer { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public string Color { get; set; }
        public int? Size { get; set; }
    }
}
