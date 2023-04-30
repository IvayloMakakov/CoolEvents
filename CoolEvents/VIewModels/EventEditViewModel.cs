using CoolEvents.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoolEvents.VIewModels
{
    public class EventEditViewModel
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(64, ErrorMessage = "Event name must be max 64 symbols")]
        public string Name { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Event name must be max 255 symbols")]
        public string Description { get; set; }

        public string PictureURL { get; set; }

        public DateTime DateTimeEvent { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
