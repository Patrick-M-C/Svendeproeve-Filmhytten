﻿namespace Svendeprøve.Repo.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
