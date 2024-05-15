using System.Data;
using Microsoft.Data.SqlClient;
using ModelClass;
using ModelClass.Dto;
using Services.Interface;
using Dapper;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Services.ServiceImpl
{
    public class BookService : IBooks
    {

        private readonly IConfiguration _configurations;

        public BookService(IConfiguration configurations)
        {
            _configurations = configurations;
        }
        public BookDto CreateNewBook(BookDto bookDto, List<Guid> authorId)
        {
            var storedprod = "AddBookWithAuthors";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();

            var authorIdsTable = new DataTable();
            authorIdsTable.Columns.Add("AuthorID", typeof(Guid));
            foreach (var authorIds in authorId)
            {
                authorIdsTable.Rows.Add(authorIds);
            }

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", Guid.NewGuid());
            parameters.Add("@ISBN", "asjkdjasg");
            parameters.Add("@Title", bookDto.Title);
            parameters.Add("@Description", bookDto.Description);
            parameters.Add("@PublicationDate", bookDto.PublicationDate);
            parameters.Add("@Price", bookDto.Price);
            parameters.Add("@Language", bookDto.Language);
            parameters.Add("@Publisher", bookDto.Publisher);
            parameters.Add("@PageCount", bookDto.PageCount);
            parameters.Add("@AverageRating", bookDto.AverageRating);
            parameters.Add("@GenreId", bookDto.GenreId);
            parameters.Add("@AuthorIDs", authorIdsTable.AsTableValuedParameter("AuthorIDListType"));

            connection.QuerySingle<Book>("AddBookWithAuthors", parameters, commandType: CommandType.StoredProcedure);
            return bookDto;

        }

        public List<Book> GetAllBooks()
        {
            var storredProd = "GetAllBooks";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));

            connection.Open();
            var books = connection.Query<Book>(storredProd, commandType: CommandType.StoredProcedure).ToList();
            return books;
        }

        public Book GetBooksById(Guid bookId)
        {
            DynamicParameters parameters = new DynamicParameters();
            var storedprod = "GetBookById";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            parameters.Add("@value1", bookId);
            connection.Open();
            var book = connection.QuerySingle<Book>(storedprod, parameters, commandType: CommandType.StoredProcedure);
            return book;
        }

        public Book HardDelete(Guid bookId)
        {
            DynamicParameters parameters = new DynamicParameters();
            var storedprod = "DeleteBookById";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            parameters.Add("@Id", bookId);
            connection.Open();
            var book = connection.Query<Book>(storedprod, parameters, commandType: CommandType.StoredProcedure);
            return null;
        }

        //public Book SoftDelete(Guid bookId)
        //{
        //    throw new NotImplementedException();
        //}

        public Guid UpdateBook(UpdateBookDto updatebook, Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();
            var storedprod = "UpdateBook";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            parameters.Add("@AverageRating", updatebook.AverageRating);
            parameters.Add("@Price", updatebook.Price);
            parameters.Add("@Description", updatebook.Description);
            parameters.Add("@Id", id);
            connection.Open();
            var book = connection.Execute(storedprod, parameters, commandType: CommandType.StoredProcedure);
            return id;

        }
    }
}
