using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examplemvc.Models;

public class Post
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [ForeignKey("Pengguna"), Column("user_id")]
    public int UserId { get; set; }
    
    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    
    [Column("CreatedAt")]
    public DateTime CreatedAt { get; set; }

    [InverseProperty(nameof(PostTag.Post))]
    public ICollection<PostTag>? PostTags { get; set; }

    public User? Pengguna {get; set;}
}

public class PostTag
{
    [Key]
    public int PostId { get; set; }

    [Key]
    public int TagId { get; set; }

    [ForeignKey("PostId")]
    public Post? Post { get; set; }

    [ForeignKey("TagId")]
    public Tags? Tag { get; set; }
}

public class Tags
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
}

public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}

public class Login
{

    [Key]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; }
}



