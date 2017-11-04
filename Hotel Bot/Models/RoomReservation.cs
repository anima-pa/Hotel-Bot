using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hotel_Bot.Models
{
    public enum BedSizeOptions
    {
        King,
        Queen,
        Double,
        Single
    }
    public enum AmentiesOptions
    {
        WiFi,
        ExtraTowels,
        GymAccess,
        Kitchen
    }
    [Serializable]
    public class RoomReservation
    {
        public BedSizeOptions? Bedsize;
        public int? NoOfOccupants;
        public DateTime? CheckInDate;
        public int? NoOfDays;
        public List<AmentiesOptions> Amenities;
        public static IForm<RoomReservation> BuildForm()
        {
            return new FormBuilder<RoomReservation>()
                .Message("Welcome to Hotel Room Reservation System Bot!!!")
                .Build();

        }
    }
}