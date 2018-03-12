using System.ComponentModel.DataAnnotations;

namespace waveRiderTester.Models
{
    public class Beach
    {
        [Key]
        public int BeachId { get; set; }
        [Required]
        public string BreakType { get; set; }
        [Required]
        public string Latitude { get; set; }
        [Required]
        public string Longtitude { get; set; }
        [Required] 
        public string BeachName { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Region { get; set; }

        public Beach()
        {

        }
        public Beach(string breaktype, string lat, string lon, string name, string state,
        string region)
        {
            BreakType = breaktype;
            Latitude = lat;
            Longtitude = lon;
            BeachName = name;
            State = state;
            Region = region;
        }    
    }
}