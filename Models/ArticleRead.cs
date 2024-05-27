
namespace Models
{
    public class ArticleRead
    {
        public required string Id { get; set; }
        public required string UserId { get; set; }
        public User? User { get; set; }
        public required int ArticleId { get; set; }
        public Article? Article { get; set; }
        public required DateTime Date { get; set; }
    }
}
