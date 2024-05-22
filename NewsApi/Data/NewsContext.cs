using Microsoft.EntityFrameworkCore;
using NewsApi.Models;

namespace NewsApi.Data
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions<NewsContext> options)
            : base(options)
        {

        }
        public DbSet<FavoriteNews> FavoriteNews { get; set; }
    }
}
