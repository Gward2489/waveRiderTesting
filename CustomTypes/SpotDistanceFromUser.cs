using waveRiderTester.Models;

namespace waveRiderTester.CustomTypes
{
    public class SpotDistanceFromUser
    {
        public double DistanceToUser { get; set; }
        public Beach Beach {get; set;}

        public SpotDistanceFromUser(double dis, Beach beach)
        {
            DistanceToUser = dis;
            Beach = beach;
        }

        public SpotDistanceFromUser()
        {

        }

    }
}