using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelClass.Dto
{
    public class AuthorDto
    {
        [StringLength(30, ErrorMessage = "Length should be <= 30")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string FirstName { get; set; }

        [StringLength(30, ErrorMessage = "Length should be <= 30")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public string LastName { get; set; }
        public string Biography { get; set; }
        //public DateOnly BirthDate { get; set; }

        [StringLength(20, ErrorMessage = "Length should be <= 20")]
        public string Country { get; set; }
    }
}
