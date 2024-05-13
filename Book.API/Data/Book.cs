namespace Book.API.Data;

public partial class Book
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public int? Year { get; set; }

    public string Isbn { get; set; } = null!;

    public string? Summary { get; set; }

    public string? Image { get; set; }

    public string? Price { get; set; }

    public int? AuthorId { get; set; }

    public virtual Author? Author { get; set; }
}
