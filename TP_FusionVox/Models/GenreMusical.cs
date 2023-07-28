namespace TP2.Models
{
    public class GenreMusical
    {
        public int Id { get; set; }
        public List<Artiste> Artistes { get; set; }
        public string Nom { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public GenreMusical() { }
    }
}
