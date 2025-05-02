namespace Svendeprøve.Repo.DTO
{
    public class Seat
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public bool IsReserved { get; set; }
        public int SeatNumber { get; set; }
        public int HallId { get; set; }
    }
}
