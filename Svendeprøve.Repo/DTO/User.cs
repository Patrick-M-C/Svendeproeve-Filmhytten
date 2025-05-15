namespace Svendeprøve.Repo.DTO
{
    public class User
    {
        // Bruger profil med kontaktoplysninger, adgangskode og administratorstatus.
        // Indeholder en liste af billetter (Tickets), som brugeren har købt eller reserveret.
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public List<Ticket>? Tickets { get; set; }
    }
}
