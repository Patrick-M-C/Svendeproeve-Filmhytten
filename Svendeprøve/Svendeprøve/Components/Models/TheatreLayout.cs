namespace Svendeprøve.Components.Models
{
    // Frontend-datamodel til repræsentation af en biografopsætning i en Blazor-applikation.
    // Indeholder information om biografens navn, billetpris og tilknyttede sale.
    // Bruges til at vise og håndtere biografrelaterede data i UI eller til deserialisering af API-data.

    // Funktionalitet: (i rækkefølge):
    // TheaterName -> Navnet på biografen, f.eks. "Filmhytten".
    // TicketPrice -> Prisen for en billet i biografen.
    // Hall -> Liste af sale (Hall-objekter) i biografen.

    public class TheaterLayout
    {
        public string TheaterName { get; set; }
        public decimal TicketPrice { get; set; }
        public List<Hall> Hall { get; set; }
    }

}