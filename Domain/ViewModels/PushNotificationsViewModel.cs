
using System.ComponentModel.DataAnnotations;
namespace Domain.ViewModels
{
    public class PushNotificationsViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Endpoint { get; set; } = null!;

        public string P256DH { get; set; } = null!;

        public string Auth { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
