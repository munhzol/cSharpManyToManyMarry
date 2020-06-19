using System.ComponentModel.DataAnnotations;

namespace LoginRegTest.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID {get; set;}

        public int UserID {get; set;}

        public int PlanID {get; set;}

        public User Guest {get; set;}

        public Plan MyPlan {get; set;}

    }
}