namespace Svendeprøve.Repo.DTO
{
    public class Seat
    {
        // En siddeplads i en sal med række- og sædenummer samt reservationstilstand.
        // Knytter sig til en bestemt Hall via HallId (fremmednøgle).
        public int Id { get; set; }
        public int Row { get; set; }
        public bool IsReserved { get; set; }
        public int SeatNumber { get; set; }
        public int HallId { get; set; }
    }
}
