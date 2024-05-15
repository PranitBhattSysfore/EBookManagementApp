using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelClass;
using ModelClass.Dto;

namespace Services.Interface
{
    public interface IBooks
    {
        public BookDto CreateNewBook(BookDto bookdto, List<Guid> authorId);

        public Guid UpdateBook(UpdateBookDto UpdatebBook , Guid id);

        public List<Book> GetAllBooks();

        public Book GetBooksById(Guid bookId);

        //public Book SoftDelete(Guid bookId);

        public Book HardDelete(Guid bookId);
    }
}
