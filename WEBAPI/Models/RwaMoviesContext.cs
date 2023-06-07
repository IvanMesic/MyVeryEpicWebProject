using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace WEBAPI.Models;

public partial class RwaMoviesContext : DbContext
{
    public RwaMoviesContext()
    {
    }

    public RwaMoviesContext(DbContextOptions<RwaMoviesContext> options)
        : base(options)
    {
    }
    private readonly List<Video> _videoList = new List<Video>();
    private readonly List<VideoTag> _videoTags = new List<VideoTag>();

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<VideoTag> VideoTags { get; set; }
    public List<Video> GetVideos() => _videoList;
    public List<VideoTag> GetVideoTags => _videoTags;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=Mesketron\\EPSKISERVER;Database=RwaMovies;User Id=sa;Password=SQL;TrustServerCertificate=True;MultipleActiveResultSets=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.Property(e => e.Code).IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasOne(d => d.CountryOfResidence).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Country");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");

            entity.HasOne(d => d.Genre).WithMany(p => p.Videos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Video_Genre");

            entity.HasOne(d => d.Image).WithMany(p => p.Videos).HasConstraintName("FK_Video_Images");
        });

        modelBuilder.Entity<VideoTag>(entity =>
        {
            entity.HasOne(d => d.Tag).WithMany(p => p.VideoTags)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VideoTag_Tag");

            entity.HasOne(d => d.Video).WithMany(p => p.VideoTags)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VideoTag_Video");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
