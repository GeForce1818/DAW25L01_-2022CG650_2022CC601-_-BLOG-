using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace L01_2022CG650_2022CC601.Models
{
    public class BlogContext : DbContext
    {

        public BlogContext(DbContextOptions<BlogContext> option) : base(option)

        {

        }
    }
}
