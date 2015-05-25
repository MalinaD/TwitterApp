namespace Twitter.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
         private const int DEFAULT_MESSAGE_STATUS = 0;

        private MessageStatus messageStatus;
        private DateTime dateModified;

        public Message()
        {
            this.MessageStatus = DEFAULT_MESSAGE_STATUS;
            this.DateModified = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(2000, ErrorMessage = "Message should be between 1 and 2000 charachters.")]
        public string Content { get; set; }

        [Required]
        [DefaultValue(DEFAULT_MESSAGE_STATUS)]
        public MessageStatus MessageStatus
        {
            get { return this.messageStatus; }
            set { this.messageStatus = value; }
        }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateModified
        {
            get { return this.dateModified; }
            set { this.dateModified = value; } 
        }

        public string SenderId { get; set; }

        public virtual User Sender { get; set; }

        public string ReceiverId { get; set; }

        public virtual User Receiver { get; set; }
    }
}
