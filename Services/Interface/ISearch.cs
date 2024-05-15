using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelClass;

namespace Services.Interface
{
    public interface ISearch
    {
        public List<dynamic> SearchByTitle(string title);
        public List<dynamic> SearchByGenre(int genreId);

        public List<dynamic> SearchByAuthor(Guid id);


    }
}
