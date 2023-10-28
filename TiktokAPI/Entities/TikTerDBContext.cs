using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TiktokAPI.Entities
{
    public partial class TikTerDBContext : DbContext
    {
        public TikTerDBContext()
        {
        }

        public TikTerDBContext(DbContextOptions<TikTerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<FollowRelationship> FollowRelationships { get; set; } = null!;
        public virtual DbSet<HashTag> HashTags { get; set; } = null!;
        public virtual DbSet<HashtagVideo> HashtagVideos { get; set; } = null!;
        public virtual DbSet<Like> Likes { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public virtual DbSet<Search> Searches { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Video> Videos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("server=localhost;Database=TikTerDB;Username=postgres;Password=Trieukute011");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.CommentId)
                    .HasColumnName("commentID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Text)
                    .HasColumnType("character varying")
                    .HasColumnName("text");

                entity.Property(e => e.Time)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("time ");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_comment_user");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.VideoId)
                    .HasConstraintName("fk_comment_video");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Image)
                    .HasColumnType("character varying")
                    .HasColumnName("image");

                entity.Property(e => e.Problem)
                    .HasColumnType("character varying")
                    .HasColumnName("problem");

                entity.Property(e => e.Title)
                    .HasColumnType("character varying")
                    .HasColumnName("title");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("feedback_userId_fkey");
            });

            modelBuilder.Entity<FollowRelationship>(entity =>
            {
                entity.HasKey(e => e.FollowId)
                    .HasName("followRelationship_pkey");

                entity.ToTable("followRelationship");

                entity.Property(e => e.FollowId)
                    .HasColumnName("followID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Followeduser).HasColumnName("followeduser");

                entity.Property(e => e.FollowerUser).HasColumnName("followerUser");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Time)
                    .HasColumnType("time with time zone")
                    .HasColumnName("time");

                entity.HasOne(d => d.FolloweduserNavigation)
                    .WithMany(p => p.FollowRelationshipFolloweduserNavigations)
                    .HasForeignKey(d => d.Followeduser)
                    .HasConstraintName("fk_followRelationship_followed");

                entity.HasOne(d => d.FollowerUserNavigation)
                    .WithMany(p => p.FollowRelationshipFollowerUserNavigations)
                    .HasForeignKey(d => d.FollowerUser)
                    .HasConstraintName("fk_followRelationship_follower");
            });

            modelBuilder.Entity<HashTag>(entity =>
            {
                entity.ToTable("hashTag");

                entity.Property(e => e.HashTagId)
                    .HasColumnName("hashTagID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.HashTagName)
                    .HasColumnType("character varying")
                    .HasColumnName("hashTagName");
            });

            modelBuilder.Entity<HashtagVideo>(entity =>
            {
                entity.ToTable("hashtagVideo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.HasTagId).HasColumnName("hasTagID");

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.HasOne(d => d.HasTag)
                    .WithMany(p => p.HashtagVideos)
                    .HasForeignKey(d => d.HasTagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_hasTagVideo_hashTag");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.HashtagVideos)
                    .HasForeignKey(d => d.VideoId)
                    .HasConstraintName("fk_hashTagVideo_Video");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("like");

                entity.Property(e => e.LikeId)
                    .HasColumnName("likeID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.IsDislike).HasColumnName("isDislike");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.VideoId).HasColumnName("videoID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_like_user");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.VideoId)
                    .HasConstraintName("fk_like_video");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("notification");

                entity.Property(e => e.NotificationId)
                    .HasColumnName("notificationID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Content)
                    .HasColumnType("character varying")
                    .HasColumnName("content");

                entity.Property(e => e.IsRead).HasColumnName("isRead");

                entity.Property(e => e.Time)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("time");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_notification_user");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("refreshToken");

                entity.Property(e => e.Id).UseIdentityAlwaysColumn();

                entity.Property(e => e.ExpiredDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("expiredDate");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.IsUsed).HasColumnName("isUsed");

                entity.Property(e => e.IssuedDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("issuedDate");

                entity.Property(e => e.Token).HasColumnType("character varying");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_refreshToken_User");
            });

            modelBuilder.Entity<Search>(entity =>
            {
                entity.ToTable("search");

                entity.Property(e => e.SearchId)
                    .HasColumnName("searchId")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.KeyWord)
                    .HasColumnType("character varying")
                    .HasColumnName("keyWord");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Searches)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("search_userId_fkey");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId)
                    .HasColumnName("userID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Avatar)
                    .HasColumnType("character varying")
                    .HasColumnName("avatar");

                entity.Property(e => e.Bio)
                    .HasColumnType("character varying")
                    .HasColumnName("bio");

                entity.Property(e => e.DisplayedName)
                    .HasColumnType("character varying")
                    .HasColumnName("displayedName");

                entity.Property(e => e.Email).HasColumnType("character varying");

                entity.Property(e => e.HashedPassword)
                    .HasColumnType("character varying")
                    .HasColumnName("hashedPassword");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.Salt)
                    .HasColumnType("character varying")
                    .HasColumnName("salt");

                entity.Property(e => e.UserName)
                    .HasColumnType("character varying")
                    .HasColumnName("userName");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.ToTable("video");

                entity.Property(e => e.VideoId)
                    .HasColumnName("videoID")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Caption)
                    .HasColumnType("character varying")
                    .HasColumnName("caption");

                entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");

                entity.Property(e => e.UploadDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("uploadDate");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.VideoUrl)
                    .HasColumnType("character varying")
                    .HasColumnName("videoURL");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_video_user");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
