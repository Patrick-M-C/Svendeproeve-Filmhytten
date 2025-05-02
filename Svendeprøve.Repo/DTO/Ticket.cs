namespace Svendeprøve.Repo.DTO
{
    public class Ticket
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int UserId { get; set; }
        public int SeatId { get; set; }
        public int ScreeningId { get; set; }
        public bool IsCanceled { get; set; }
    }
}
