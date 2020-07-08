using System;
using System.ComponentModel;

namespace YarnStash.Models
{
    public class YarnModel
    {
        public int id { get; set; } //required by database for primary key
        [DisplayName("Brand")]
        public string Manufacturer { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Color { get; set; }
        public int Size { get; set; }
    }
}
