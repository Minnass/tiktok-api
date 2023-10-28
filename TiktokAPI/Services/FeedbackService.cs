using Microsoft.Extensions.Hosting;
using TiktokAPI.Core.Interfaces;
using TiktokAPI.Entities;
using TiktokAPI.Models;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork<TikTerDBContext> uow;
        private readonly IWebHostEnvironment _hostEnvironment;
        public FeedbackService(IUnitOfWork<TikTerDBContext> uow, IWebHostEnvironment webHostEnvironment)
        {
            this.uow = uow;
            this._hostEnvironment = webHostEnvironment;
        }

        public async Task CreateFeedback(FeedbackModel model)
        {
            string path = "";
            if (model.Image != null)
            {
                var uploadPath = Path.Combine(_hostEnvironment.ContentRootPath, "feedbacks");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                var filePath = Path.Combine(uploadPath, uniqueFileName);
                path = Path.Combine("feedbacks", uniqueFileName);
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
            }

            this.uow.GetRepository<Feedback>().Insert(
                new Feedback
                {
                    Title = model.Title,
                    Problem = model.Problem,
                    UserId = int.Parse(model.UserId),
                    Image = path
                });

            this.uow.SaveChanges();

        }
    }
}
