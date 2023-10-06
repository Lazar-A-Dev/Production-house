using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Kategorija{
        [Key]
        public int ID{get; set;}
        public required string? NazivKat{get; set;}
        public List<Film> Filmovi{get; set;}
    }
}