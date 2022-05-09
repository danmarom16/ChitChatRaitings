using ChitChatRaitings.Models;

namespace ChitChatRaitings.Services
{
    public interface IFeedbackService
    {
        public Feedback GetFeedback(int Id);

        public List<Feedback> GetAllFeedbacks();

        public void EditFeedback(int Id, int Rate, string? Description);

        public void DeleteFeedback(int Id);

        public void Create(int Rate, string? Description, string? Name);
    }
}
