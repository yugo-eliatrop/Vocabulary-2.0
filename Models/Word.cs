using System;
using System.ComponentModel.DataAnnotations;

namespace Vocabulary
{
    public class Word
    {
        // [Required]
        // [StringLength(50, MinimumLength = 1, ErrorMessage = "Length must be between 1 and 50 characters")]
        // public string Rus { get; set; }

        // [Required]
        // [StringLength(50, MinimumLength = 1, ErrorMessage = "Length must be between 1 and 50 characters")]
        // public string Eng { get; set; }

        // [Required]
        // [Range(0, 5)]
        // public int Points { get; set; }

        // public bool IsLearned { get; set; }

        // public DateTime UpdatedAt { get; set; }
        public int Id { get; set; }

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
                    rus = HandleString(value);
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
                    eng = HandleString(value);
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

        public bool IsLearned { get; set; }

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
            IsLearned = false;
        }

        public Word() { }

        private string HandleString(string str)
        {
            if (char.IsLower(str[0]))
                return char.ToUpper(str[0]) + str.Substring(1, str.Length - 1);
            return str;
        }

        public void UpPoints()
        {
            if (Points < 5)
                ++Points;
        }

        public void DownPoints()
        {
            if (Points == 5)
                Points = 3;
            else if (Points > 0)
                --Points;
        }
    }
}
