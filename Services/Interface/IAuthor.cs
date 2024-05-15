using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelClass;
using ModelClass.Dto;

namespace Services.Interface
{
    public interface IAuthor
    {
        public List<Author> GetAllAuthors();

        public Author AddNewAuthor(AuthorDto authorDto);

        public void HardDelete(Guid id);

        public Guid UpdateAuthor(AuthorUpdateDto authorUpdateDto, Guid Id);    
    }
}
