using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LoginRegTest.Models;

namespace LoginRegTest.Models
{
    public class Plan
    {
        [Key]
        public int PlanID {get; set;}

        [Required]
        public string Wedder1 {get; set;}

        [Required]
        public string Wedder2 {get; set;}

        [Required]
        [FutureDate]
        public DateTime Date {get; set;}

        [Required]
        public string Address {get; set;}

        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime UpdatedAt {get; set;} = DateTime.Now;

        public int UserID {get; set;}
        public User Organizer {get; set;}

        public List<Reservation> Guests { get; set; }


    }
}