using System;
namespace YarnStash.Models
{
    public class YarnModel
    {
        public int id { get; set; } //required by database for primary key
        public string yarnManufacturer { get; set; }
        public string yarnName { get; set; }
        public int yarnAmount { get; set; }
        public string yarnColor { get; set; }
        public int yarnSize { get; set; }
    }
}
