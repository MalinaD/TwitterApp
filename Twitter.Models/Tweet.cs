namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Tweet
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(140)]
        public string Description { get; set; }

        public Location Location { get; set; }

        public DateTime TakenDate { get; set; }

        [Required]
        public virtual User Author { get; set; }
    }
}
