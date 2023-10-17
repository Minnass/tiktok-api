using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TiktokAPI.Core.Interfaces;
using TiktokAPI.Entities;
using TiktokAPI.Models;
using TiktokAPI.Models.Account;
using TiktokAPI.Models.Post;
using TiktokAPI.Services.Interfaces;

namespace TiktokAPI.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork<TikTerDBContext> uow;
        private readonly IAuthService authService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public PostService(IUnitOfWork<TikTerDBContext> uow, IAuthService authService, IWebHostEnvironment hostEnvironment)
        {
            this.uow = uow;
            this.authService = authService;
            _hostEnvironment = hostEnvironment;
        }
        public async Task CreatePost(PostRequestModel file)
        {
            if (file == null || file.VideoFile.Length == 0)
            {
                throw new Exception("No file Selected");
            }
            if (file.VideoFile.ContentType != "video/mp4")
            {
                throw new Exception("Only .mp4 files are allowed.");
            }
            var uploadPath = Path.Combine(_hostEnvironment.ContentRootPath, "uploads");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.VideoFile.FileName;
            var filePath = Path.Combine(uploadPath, uniqueFileName);
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.VideoFile.CopyToAsync(stream);
            }
            var userInfo = this.authService.getUserInfoFromJwt();
            var newVideo = new Video
            {
                Caption = file.Caption,
                IsDeleted = false,
                UploadDate = DateTime.Now,
                VideoUrl = filePath,
                UserId = userInfo.UserId
            };
            uow.GetRepository<Video>().Insert(newVideo);
            uow.SaveChanges();

            string[] parts = file.HashTag.Split(new string[] { " " }, StringSplitOptions.None);
            foreach (var hasTagText in parts)
            {

                var existingHashTag = uow.GetRepository<HashTag>().Queryable().Where(x => x.HashTagName == hasTagText).FirstOrDefault();
                var hashTag = new HashTag()
                {
                    HashTagName = hasTagText
                };
                if (existingHashTag == null)
                {
                    uow.GetRepository<HashTag>().Insert(hashTag);
                }
                uow.SaveChanges();
                uow.GetRepository<HashtagVideo>().Insert(new HashtagVideo
                {
                    HasTagId = (existingHashTag == null) ? hashTag.HashTagId : existingHashTag.HashTagId,
                    VideoId = newVideo.VideoId,
                });
                uow.SaveChanges();
            }


        }

        public void DeletePost(long videoID)
        {
            Expression<Func<Video, bool>> predicate = x => x.IsDeleted != false && x.VideoId == videoID;
            var video = uow.GetRepository<Video>().Queryable().Where(predicate).FirstOrDefault();
            if (video == null)
            {
                throw new Exception("Video not found");
            }
            video.IsDeleted = true;
            uow.SaveChanges();
        }

        public VideoOverview GetVideo(long videoID)
        {
            Expression<Func<Video, bool>> predicate = x => x.IsDeleted == false && x.VideoId == videoID;
            var result = uow.GetRepository<Video>().Queryable()
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Comments)
                .ThenInclude(y => y.User)
                .Include(x => x.HashtagVideos)
                .ThenInclude(y => y.HasTag)
                .Include(x => x.Likes)
                .Where(predicate).FirstOrDefault();
            if (result == null)
            {
                throw new Exception("Video not found");
            }
            return new VideoOverview
            {
                User = new UserInfomation
                {
                    UserId = result.UserId,
                    Avatar = result.User.Avatar,
                    UserName = result.User.UserName,
                    DisplayedName = result.User.DisplayedName
                },
                Caption = result.Caption,
                Comment = result.Comments.Where(x => x.IsDeleted == false).Count(),
                Like = result.Likes.Where(x => x.IsDislike == false).Count(),
                UploadDate = result.UploadDate,
                VideoId = result.VideoId,
                VideoUrl = result.VideoUrl,
                HasTag = result.HashtagVideos.Select(z => new Models.HasTagModel
                {
                    HasTagId = z.HasTagId,
                    HasTagName = z.HasTag.HashTagName
                }).ToList()
            };
        }

        public IList<VideoOverview> GetVidesByTagName(string name)
        {
            Expression<Func<HashtagVideo, bool>> predicate = x => string.IsNullOrEmpty(name) || name == "null" || x.HasTag.HashTagName.Contains(name);
            var result = uow.GetRepository<HashtagVideo>().Queryable()
               .AsNoTracking()
               .Include(x=>x.Video)
               .ThenInclude(y=>y.User)
               .Include(x => x.HasTag)
               .Where(predicate)
              .Where(predicate).Select(y => new VideoOverview
              {
                  User = new UserInfomation
                  {
                      UserId = y.Video.UserId,
                      Avatar = y.Video.User.Avatar,
                      UserName = y.Video.User.UserName,
                      DisplayedName = y.Video.User.DisplayedName
                  },
                  Caption = y.Video.Caption,
                  Comment = y.Video.Comments.Where(z => z.IsDeleted == false).Count(),
                  Like = y.Video.Likes.Where(z => z.IsDislike == false).Count(),
                  UploadDate = y.Video.UploadDate,
                  VideoId = y.Video.VideoId,
                  VideoUrl = y.Video.VideoUrl,
                  HasTag = y.Video.HashtagVideos.Select(z => new Models.HasTagModel
                  {
                      HasTagId = z.HasTagId,
                      HasTagName = z.HasTag.HashTagName
                  }).ToList()
              }).ToList();
            return result;

        }



        public IList<VideoOverview> getAll(string? search)
        {
            Expression<Func<Video, bool>> predicate = x => (string.IsNullOrEmpty(search) || search == "null" || x.Caption.Contains(search)
            )
            && x.IsDeleted == false;
            var result = uow.GetRepository<Video>().Queryable()
                .AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Likes)
                .Include(x => x.Comments)
                .Include(x => x.HashtagVideos)
                .Where(predicate).Select(y => new VideoOverview
                {
                    User = new UserInfomation
                    {
                        UserId = y.UserId,
                        Avatar = y.User.Avatar,
                        UserName = y.User.UserName,
                        DisplayedName = y.User.DisplayedName
                    },
                    Caption = y.Caption,
                    Comment = y.Comments.Where(z => z.IsDeleted == false).Count(),
                    Like = y.Likes.Where(z => z.IsDislike == false).Count(),
                    UploadDate = y.UploadDate,
                    VideoId = y.VideoId,
                    VideoUrl = y.VideoUrl,
                    HasTag = y.HashtagVideos.Select(z => new Models.HasTagModel
                    {
                        HasTagId = z.HasTagId,
                        HasTagName = z.HasTag.HashTagName
                    }).ToList()
                }).ToList();
            return result;

        }

        public IList<VideoOverview> GetFollowingVideos(IList<long> ids)
        {
            Expression<Func<Video, bool>> predicate = x => x.IsDeleted == false
            && ids.Contains(x.UserId);
            var result = this.uow.GetRepository<Video>().Queryable()
                 .AsNoTracking()
                  .Where(predicate).Select(y => new VideoOverview
                  {
                      User = new UserInfomation
                      {
                          UserId = y.UserId,
                          Avatar = y.User.Avatar,
                          UserName = y.User.UserName,
                          DisplayedName = y.User.DisplayedName
                      },
                      Caption = y.Caption,
                      Comment = y.Comments.Where(z => z.IsDeleted == false).Count(),
                      Like = y.Likes.Where(z => z.IsDislike == false).Count(),
                      UploadDate = y.UploadDate,
                      VideoId = y.VideoId,
                      VideoUrl = y.VideoUrl,
                      HasTag = y.HashtagVideos.Select(z => new Models.HasTagModel
                      {
                          HasTagId = z.HasTagId,
                          HasTagName = z.HasTag.HashTagName
                      }).ToList()
                  }).ToList();
            return result;
        }
    }
}
