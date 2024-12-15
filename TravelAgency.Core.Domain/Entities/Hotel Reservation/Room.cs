using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TravelAgency.Core.Domain.Entities._Common;

namespace TravelAgency.Core.Domain.Entities.Hotel_Reservation
{
    public class Room : Entity
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RoomTypes RoomType { get; set; }

        public List<string>? ImagePaths { get; set; }

        public decimal PricePerNight { get; set; }
    }
}
