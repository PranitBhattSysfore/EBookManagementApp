using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass.Dto
{
    public class UpdateBookDto
    {
        public float AverageRating { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
    }
}
