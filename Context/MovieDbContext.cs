using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Book2Screen.Models;

public partial class MovieDbContext : DbContext
{
    public MovieDbContext()
    {
    }

    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=azure.com;Port=port_number;Database=database_name;Username=user_name;Password=password;Ssl Mode=Require;Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.ActorsId).HasName("actors_pkey");

            entity.ToTable("actors");

            entity.Property(e => e.ActorsId)
                .ValueGeneratedNever()
                .HasColumnName("actors_id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.Movie).WithMany(p => p.Actors)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("actors_movie_id_fkey");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("author_pkey");

            entity.ToTable("author");

            entity.Property(e => e.AuthorId)
                .ValueGeneratedNever()
                .HasColumnName("author_id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .HasColumnName("country");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Works)
                .HasMaxLength(255)
                .HasColumnName("works");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("books_pkey");

            entity.ToTable("books");

            entity.Property(e => e.BookId)
                .ValueGeneratedNever()
                .HasColumnName("book_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Country)
                .HasMaxLength(255)
                .HasColumnName("country");
            entity.Property(e => e.Genre)
                .HasMaxLength(255)
                .HasColumnName("genre");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Orjlanguage)
                .HasMaxLength(255)
                .HasColumnName("orjlanguage");
            entity.Property(e => e.Summary).HasColumnName("summary");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("books_author_id_fkey");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("comments_pkey");
         
            entity.ToTable("comments");

            entity.Property(e => e.CommentId)
                .ValueGeneratedNever()
                .HasColumnName("comment_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.CommentText).HasColumnName("comment_text");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");

            entity.HasOne(d => d.Book).WithMany(p => p.Comments)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("comments_book_id_fkey");

            entity.HasOne(d => d.IdNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Id)
                .HasConstraintName("fk_user");

            entity.HasOne(d => d.Movie).WithMany(p => p.Comments)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("comments_movie_id_fkey");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.DirectorId).HasName("directors_pkey");

            entity.ToTable("directors");

            entity.Property(e => e.DirectorId)
                .ValueGeneratedNever()
                .HasColumnName("director_id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Works)
                .HasMaxLength(255)
                .HasColumnName("works");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("movies_pkey");

            entity.ToTable("movies");

            entity.Property(e => e.MovieId)
                .ValueGeneratedNever()
                .HasColumnName("movie_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.Boxofficerevenue).HasColumnName("boxofficerevenue");
            entity.Property(e => e.Budget).HasColumnName("budget");
            entity.Property(e => e.DirectorId).HasColumnName("director_id");
            entity.Property(e => e.Movieduration).HasColumnName("movieduration");
            entity.Property(e => e.Movietype)
                .HasMaxLength(255)
                .HasColumnName("movietype");
            entity.Property(e => e.Releasedate).HasColumnName("releasedate");
            entity.Property(e => e.Score).HasColumnName("score");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.Property(e => e.Photopath)
            .HasColumnName("photopath")
            .HasMaxLength(255);

            entity.HasOne(d => d.Book).WithMany(p => p.Movies)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("movies_book_id_fkey");

            entity.HasOne(d => d.Director).WithMany(p => p.Movies)
                .HasForeignKey(d => d.DirectorId)
                .HasConstraintName("movies_director_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pk");

            entity.ToTable("users");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Mail).HasColumnName("mail");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
