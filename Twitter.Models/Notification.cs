namespace Twitter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        private DateTime dateModified;

        public Notification()
        {
            this.DateModified = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public DateTime DateModified
        {
            get { return this.dateModified; }
            set { this.dateModified = value; }
        }

        [Required]
        public string UserId { get; set; }

        [Required]
        public virtual User User { get; set; }
    }
}
