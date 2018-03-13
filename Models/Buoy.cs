using System.ComponentModel.DataAnnotations;

namespace waveRiderTester.Models
{
    public class Buoy
    {
        [Key]
        public int BuoyId { get; set;}
        [Required]
        public string NbdcId { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longtitude { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Owner { get; set; }

        public Buoy(string id, string lat, string lon, string name, string owner)
        {
            NbdcId = id;
            Latitude = lat;
            Longtitude = lon;
            Name = name;
            Owner = owner;
        }

        public Buoy()
        {
            
        }
    }
}