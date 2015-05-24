namespace Twitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Tweet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MaxLength(140)]
        public string Description { get; set; }

        public Location Location { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TakenDate { get; set; }

        public string AuthorId { get; set; }

        
        public virtual User Author { get; set; }


    }
}
