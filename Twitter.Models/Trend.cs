namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Trend
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Details { get; set; }

        public Location Location { get; set; }

        public DateTime TakenDate { get; set; }

        [Required]
        public virtual User Author { get; set; }
    }
}
