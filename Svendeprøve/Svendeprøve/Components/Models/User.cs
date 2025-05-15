namespace Svendeprøve.Components.Models
{
    public class User
    {
        // Bruger profil med kontaktoplysninger, adgangskode og administratorstatus.
        // Indeholder en liste af billetter (Tickets), som brugeren har købt eller reserveret.
        // Bruges til at håndtere brugerdata i frontend og til deserialisering af API-data.

        // Funktionalitet: (i rækkefølge):
        // Id -> Unikt ID for brugeren, typisk fra en database.
        // Name -> Brugerens navn.
        // Email -> Brugerens e-mailadresse.
        // PhoneNumber -> Brugerens telefonnummer.
        // Password -> Brugerens adgangskode
        // IsAdmin -> Angiver, om brugeren har administratorrettigheder.
        // Tickets -> Liste af billetter købt eller reserveret af brugeren, kan være null.

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public List<Ticket>? Tickets { get; set; }
    }
}