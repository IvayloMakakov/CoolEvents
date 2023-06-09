﻿

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolEvents.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EventId { get; set; }

        public virtual Event Event { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser  ApplicationUser { get; set; }

    }
}
