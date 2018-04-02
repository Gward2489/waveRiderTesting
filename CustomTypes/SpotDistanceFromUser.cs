using waveRiderTester.Models;

// this class is designed to hold data for a beach and its distance from a user

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