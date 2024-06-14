using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class UserNotifications
    {
        [Key]
        public int Id { get; set; }

        [Required, NotNull]
        public string Title { get; set; } = null!;

        public string? Body { get; set; }

        [Required, NotNull]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; } = new DateTime();

        [Required, NotNull]
        public int UserId { get; set; }

        [Required, NotNull]
        public int PushId { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }

        [ForeignKey("PushId")]
        public PushNotifications PushNotification { get; set; }
    }
}
