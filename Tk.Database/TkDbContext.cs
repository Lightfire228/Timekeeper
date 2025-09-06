
using Microsoft.EntityFrameworkCore;

namespace Tk.Database;

public class TkDbContext(DbContextOptions<TkDbContext> opts)
    : DbContext(opts)
{

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    //     base.OnConfiguring(optionsBuilder);
    // }

    public DbSet<Blog> Blog { get; set; }
    public DbSet<Post> Post { get; set; }
}

// test


public class Blog {
    public int        BlogId { get; set; }
    public string     Url    { get; set; } = default!;
    public int        Rating { get; set; }
    public List<Post> Posts  { get; set; } = [];
}

public class Post {
    public int    PostId  { get; set; }
    public string Title   { get; set; } = default!;
    public string Content { get; set; } = default!;

    public int    BlogId  { get; set; }
    public Blog   Blog    { get; set; } = default!;
}