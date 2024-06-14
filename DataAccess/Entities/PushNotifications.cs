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
    public class PushNotifications
    {
        [Key]
        public int Id { get; set; }

        [Required, NotNull]
        public string Endpoint { get; set; } = null!;

        [Required, NotNull]
        public string P256DH { get; set; } = null!;

        [Required, NotNull]
        public string Auth { get; set;} = null!;

        [Required, NotNull]
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; } = new DateTime();

        [Required, NotNull]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public Users User { get; set; }

        public virtual ICollection<UserNotifications> UserNotifications { get; set; } = new List<UserNotifications>();
    }
}
