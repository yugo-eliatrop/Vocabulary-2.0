using System;
using System.ComponentModel.DataAnnotations;

namespace Vocabulary
{
    public class Word
    {
        public int Id { get; set; }

        // [Required]
        // [StringLength(50, MinimumLength = 1, ErrorMessage = "Length must be between 1 and 50 characters")]
        // public string Rus { get; set; }

        // [Required]
        // [StringLength(50, MinimumLength = 1, ErrorMessage = "Length must be between 1 and 50 characters")]
        // public string Eng { get; set; }

        // [Required]
        // [Range(0, 5)]
        // public int Points { get; set; }

        // public DateTime UpdatedAt { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Length must be between 1 and 50 characters")]
        private string rus;

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Length must be between 1 and 50 characters")]
        private string eng;

        [Required]
        [Range(0, 5)]
        private int points;

        private DateTime updatedAt;

        public string Rus
        {
            get => rus;
            set
            {
                if (value.Length > 0)
                {
                    rus = value;
                    updatedAt = DateTime.Now;
                }
            }
        }

        public string Eng
        {
            get => eng;
            set
            {
                if (value.Length > 0)
                {
                    eng = value;
                    updatedAt = DateTime.Now;
                }
            }
        }

        public int Points
        {
            get => points;
            set
            {
                if (value >= 0)
                {
                    points = value;
                    updatedAt = DateTime.Now;
                }
            }
        }

        public DateTime UpdatedAt
        {
            get => updatedAt;
            set { updatedAt = value; }
        }

        public Word(string rus, string eng, int points = 0)
        {
            Rus = rus;
            Eng = eng;
            Points = points;
        }

        public Word() { }

        public void UpPoints()
        {
            if (Points < 5)
                ++Points;
        }

        public void DownPoints()
        {
            if (Points > 0)
                --Points;
        }
    }
}
