using BL.DTO;
using BookStore.DTO;
using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Interface
{
    public interface IBookService
    {
        Task<List<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task<string> AddBook(CreateBookDTO book);
        Task<string> UpdateBook(int id, UpdateBookDTO model);
        Task<string> DeleteBook(int id);
    }
}
