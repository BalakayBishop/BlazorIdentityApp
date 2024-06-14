
namespace Domain.ViewModels
{
    public class UserNotificationsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Body { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; } = new DateTime();

        public int UserId { get; set; }

        public int PushId { get; set; }
    }
}
