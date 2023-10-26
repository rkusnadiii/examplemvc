using examplemvc.Models;
using examplemvc.Models.Request;
using Microsoft.EntityFrameworkCore;

namespace examplemvc.Data;
public class ApplicationDbContext : DbContext
{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

    public DbSet<Post> Post { get; set; }
    public DbSet<Tags> Tags { get; set; }
    public DbSet<PostTag> PostTags { get; set; }
    public DbSet<User> Users { get; internal set; }
    public DbSet<Login> Logins { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<PostTag>().HasKey(k => new {
            k.PostId, k.TagId
        });
    }
}