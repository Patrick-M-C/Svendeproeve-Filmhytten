namespace Svendeprøve.Components.Models
{
    public class TheaterLayout
    {
        public string TheaterName { get; set; }      // Teaterets navn
        public decimal TicketPrice { get; set; }     // Billetprisen
        public List<Hall> Hall { get; set; }          // Liste over rækker med sæder
    }

}