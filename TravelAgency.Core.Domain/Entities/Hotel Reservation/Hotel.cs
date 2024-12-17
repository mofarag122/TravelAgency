using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities._Common;

namespace TravelAgency.Core.Domain.Entities.Hotel_Reservation
{
    public class Hotel : Entity
    {
        public string Name { get; set; } = null!;

        public string ImagePath { get; set; } = null!;

        public Location Location { get; set; } = null!;


        public ICollection<Room> Rooms = new List<Room>();   
    }
}
