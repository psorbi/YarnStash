using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace YarnStash.Models
{
    public enum TypeEnum
    {
        Hat,
        Sweater,
        Scarf,
        [Display(Name = "Shawl/Wrap")]
        Shawl,
        Socks,
        [Display(Name = "Gloves/Mittens")]
        Gloves,
        Toy,
        Blanket,
        Other
    }

    public class PatternModel
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Designer { get; set; }
        [DisplayName("Weight")]
        public SizeEnum Size { get; set; }
        [Required]
        public int Amount { get; set; }
        [DisplayName("Needle Size")]
        public int NeedleSize { get; set; }
        public TypeEnum Type { get; set; }

    }
}
