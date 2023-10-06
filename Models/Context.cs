using Microsoft.EntityFrameworkCore;

namespace Models{
    public class Context:DbContext{
        public DbSet<Film> Filmovi{get; set;}
        public DbSet<Kategorija> Kategorije{get; set;}
        public DbSet<Produkcija> Produkcije{get; set;}
        public Context(DbContextOptions options) : base(options){

        }
    }
}