using TiktokAPI.Models;

namespace TiktokAPI.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task CreateFeedback(FeedbackModel model);
    }
}
