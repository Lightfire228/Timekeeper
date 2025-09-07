
using Microsoft.EntityFrameworkCore;
using Tk.Database.Models;

namespace Tk.Database;

public class TkDbContext(DbContextOptions<TkDbContext> opts)
    : DbContext(opts)
{

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
    //     base.OnConfiguring(optionsBuilder);
    //     optionsBuilder.
    // }

    public DbSet<TaskModel>       Tasks            { get; set; }
    public DbSet<CompletionEvent> CompletionEvents { get; set; }
}
