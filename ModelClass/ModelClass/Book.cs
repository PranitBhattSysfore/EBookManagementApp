using ModelClass.Dto;

namespace ModelClass
{
    public class Book
    {
        //private BookDto bookDto;

        //public Book(BookDto bookDto)
        //{
        //    this.bookDto = bookDto;
        //}

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime PublicationDate { get; set; }
        public float Price { get; set; }
        public string Language { get; set;}
        public string Publisher {  get; set; }
        public int PageCount { get; set;}
        public float AverageRating { get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int GenreId { get; set; }

    }
}
