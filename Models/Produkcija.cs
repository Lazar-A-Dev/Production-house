using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Produkcija{
        [Key]
        public int ID{get; set;}
        public required string? NazivPro{get; set;}
        public List<Kategorija> Kategorije{get; set;}

    }
}