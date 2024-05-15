using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ExceptionsHandling.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ModelClass;
using Services.Interface;

namespace Services.ServiceImpl
{
    public class SearchService : ISearch
    {
        private readonly IConfiguration _configurations;

        public SearchService(IConfiguration configurations)
        {
            _configurations = configurations;
        }
        public List<dynamic> SearchByAuthor(Guid id)
        {
            DynamicParameters parameters = new DynamicParameters();
            var storedprod = "FindBookByAuthor";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            parameters.Add("@AuthorId", id);
            connection.Open();
            List<dynamic> book = (List<dynamic>)connection.Query<dynamic>(storedprod, parameters, commandType: CommandType.StoredProcedure);
            if (book.Count == 0)
            {
                throw new BookNotFoundException("No books found for the given author.");
            }
            return (List<dynamic>)book;
        }

        public List<dynamic> SearchByGenre(int genreId)
        {
            DynamicParameters parameters = new DynamicParameters();
            var storedprod = "FindBookByGenreWithAuthor";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            parameters.Add("@GenreId", genreId);
            connection.Open();
            List<dynamic> book = (List<dynamic>)connection.Query<dynamic>(storedprod, parameters, commandType: CommandType.StoredProcedure);
            if (book.Count == 0)
            {
                throw new NoBooksWithThisGenreException("No books found for the Genre.");
            }
            return book;
        }

        public List<dynamic> SearchByTitle(string title)
        {
            DynamicParameters parameters = new DynamicParameters();
            var storedprod = "FindBookByTitleWithAuthor";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            parameters.Add("@Title", title);
            connection.Open();
            List<dynamic> book = (List<dynamic>)connection.Query<dynamic>(storedprod, parameters, commandType: CommandType.StoredProcedure);
            if (book.Count == 0)
            {
                throw new NoBooksWithThisTitleException("No books found for the title.");
            }
            return book;
        }
    }
}
