using System.ComponentModel.DataAnnotations;

namespace Models{
    public class Film{
        [Key]
        public int ID{get; set;}
        public required string? NazivFilma{get; set;}
        public required float Ocena{get; set;}


    }
}