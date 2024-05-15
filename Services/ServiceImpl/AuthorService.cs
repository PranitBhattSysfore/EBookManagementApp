using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ExceptionsHandling.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ModelClass;
using ModelClass.Dto;
using Services.Interface;

namespace Services.ServiceImpl
{
    public class AuthorService : IAuthor
    {
        private readonly IConfiguration _configurations;

        public AuthorService(IConfiguration configurations)
        {
            _configurations = configurations;
        }
        public List<Author> GetAllAuthors()
        {
            var storedprod = "GetAllAuthors";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            var authors = connection.Query<Author>(storedprod, commandType: CommandType.StoredProcedure).ToList();
            if (authors.Count == 0)
            {
                throw new NoAuthors("No authors found.");
            }
            return authors;
        }
        public Author AddNewAuthor(AuthorDto authorDto)
        {
            var storedprod = "AddNewAuthor";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            connection.Open();
            var parameters = new
            {
                Id = Guid.NewGuid(),
                authorDto.FirstName,
                authorDto.LastName,
                authorDto.Biography,
                //Birthdate = authorDto.BirthDate,
                authorDto.Country
            };
            connection.Execute(storedprod, parameters, commandType: CommandType.StoredProcedure);
            //return bookDto;
            return null;
        }
        public void HardDelete(Guid bookId)
        {
            DynamicParameters parameters = new DynamicParameters();
            var storedprod = "DeleteAuthorById";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            parameters.Add("@Id", bookId);
            connection.Open();
            var book = connection.Query<Book>(storedprod, parameters, commandType: CommandType.StoredProcedure);
            //return null;
        }

        public Guid UpdateAuthor(AuthorUpdateDto authorUpdateDto, Guid Id)
        {
            var storedprod = "UpdateAuthor";
            using SqlConnection connection = new SqlConnection(_configurations.GetConnectionString("ConnectionString"));
            var parameters = new
            {
                Id,
                authorUpdateDto.FirstName,
                authorUpdateDto.LastName,
                authorUpdateDto.Biography,
                //Birthdate = authorDto.BirthDate,
                authorUpdateDto.Country
            };
            connection.Open();
            var book = connection.Execute(storedprod, parameters, commandType: CommandType.StoredProcedure);
            //return book;
            return Id;
        }
    }
}
