using Microsoft.EntityFrameworkCore;

namespace Book.API.Data;

public partial class BookContext : DbContext
{
    public BookContext()
    {
    }

    public BookContext(DbContextOptions<BookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Book;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC0712697649");

            entity.Property(e => e.Bio)
                .HasMaxLength(1000)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsFixedLength();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07D35FF18F");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EAFC9158FA").IsUnique();

            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Isbn)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("ISBN");
            entity.Property(e => e.Price)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Summary)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsFixedLength();

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToTable");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
