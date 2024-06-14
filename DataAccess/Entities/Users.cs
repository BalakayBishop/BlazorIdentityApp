using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required, NotNull]
        public string FirstName { get; set; } = null!;

        [Required, NotNull] 
        public string LastName { get; set; } = null!;

        [Required, NotNull]
        public string Email { get; set; } = null!;

        [Required, NotNull]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<PushNotifications> PushNotifications { get; set; } = new List<PushNotifications>();
        public virtual ICollection<UserNotifications> UserNotifications { get; set; } = new List<UserNotifications>();
        public virtual ICollection<UserRecords> UserRecords { get; set; } = new List<UserRecords>();
    }
}
