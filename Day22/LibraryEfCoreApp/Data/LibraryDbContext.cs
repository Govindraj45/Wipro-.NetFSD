using LibraryEfCoreApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryEfCoreApp.Data;

public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .HasMany(author => author.Books)
            .WithOne(book => book.Author)
            .HasForeignKey(book => book.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Book>()
            .HasMany(book => book.Genres)
            .WithMany(genre => genre.Books)
            .UsingEntity<Dictionary<string, object>>(
                "BookGenre",
                right => right.HasOne<Genre>().WithMany().HasForeignKey("GenreId"),
                left => left.HasOne<Book>().WithMany().HasForeignKey("BookId"));

        modelBuilder.Entity<Genre>()
            .HasIndex(genre => genre.Name)
            .IsUnique();
    }
}
