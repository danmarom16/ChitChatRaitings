using ChitChatRaitings.Models;

namespace ChitChatRaitings.Services
{
    public class FeedbackService : IFeedbackService
    {
        private static List<Feedback> Feedbacks = new List<Feedback>();

        public void Create(int Rate, string? Description, string? Name)
        {
            int nextId = 0;

            if (Feedbacks.Count > 0)
            {
                nextId = Feedbacks.Max(x => x.Id) + 1;
            }
            Feedbacks.Add(new Feedback() { Id = nextId, Rate = Rate, Description = Description, Name = Name});
        }

        public void DeleteFeedback(int Id)
        {
            Feedback feedback = GetFeedback(Id);
            Feedbacks.Remove(feedback); 
        }

        public void EditFeedback(int Id, int Rate , string? Description)
        {
            Feedback feedback = GetFeedback(Id);
            feedback.Description = Description;
            feedback.Rate = Rate;

        }

        public List<Feedback> GetAllFeedbacks()
        {
            return Feedbacks;
        }

        public Feedback GetFeedback(int Id)
        {
            return Feedbacks.Find(x => x.Id == Id);
        }
    }
}
