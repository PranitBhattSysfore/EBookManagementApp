using ModelClass.Dto;

namespace EbookManagementApp.Controllers
{
    public class BookWithAuthorsModel
    {
        public BookDto Book { get; set; }
        public List<Guid> AuthorIds { get; set; }
    }
}
