﻿namespace LMS.Entities
{
    public class Examinee
    {
        public int Id { get; set; }

        public User User { get; set; }

        public string UserId { get; set; }

        public int BirthYear { get; set; }

        public string College { get; set; }

        public string Faculty { get; set; }

        public string Specialty { get; set; }

        public int Course { get; set; }

        public string EnglishLevel { get; set; }

        public string Comment { get; set; }
    }
}
