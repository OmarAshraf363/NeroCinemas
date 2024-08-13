namespace Nero.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price {  get; set; }
        public string ImgUrl {  get; set; }
        public string TrailerUrl { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieStatus MovieStatus { get; set; }
        public int? NumOfVisit { get; set; }

        //Relations
        public int CategoryId {  get; set; }=0;
        public int CinemaId {  get; set; }
        public Cinema Cinema {  get; set; }
        public Category Category {  get; set; }

        public List<ActorMovie> ActorMovies { get; set; }
    }
}
