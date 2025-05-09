namespace Svendeprøve.Components.Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int SeatsPerRow { get; set; }
        public bool IsReserved { get; set; }
        public bool IsSelected { get; set; }
        public int SeatNumber { get; set; }
        public int HallId { get; set; }
    }

}