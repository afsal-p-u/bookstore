using BL.DTO;
using BL.Interface;
using BookStore.DTO;
using BookStore.Model;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Book>> GetBooks()
        {
            return await _repository.GetBooks();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _repository.GetBookById(id);
        }

        public async Task<string> AddBook(CreateBookDTO _book)
        {
            Book model = new Book();
            model.Title = _book.Title;
            model.Description = _book.Description;
            model.Author = _book.Author;
            model.Price = _book.Price;
            model.Category = _book.Category;
            model.Stock = _book.Stock;
            model.ImageURL = _book.ImageURL;
            model.CreatedDate = DateTime.Now;

            await _repository.AddBook(model);

            return "Book Added Successfully";
        }

        public async Task<string> UpdateBook(int id, UpdateBookDTO _model)
        {
            var book = await _repository.GetBookById(id);

            if (book == null)
            {
                return "Book Not Found";
            }

            Book model = new Book();

            if (!string.IsNullOrEmpty(_model.Title))
            {
                model.Title = _model.Title;
            }
            if (!string.IsNullOrEmpty(_model.Description))
            {
                model.Description = _model.Description;
            }
            if (!string.IsNullOrEmpty(_model.Author))
            {
                model.Author = _model.Author;
            }
            if (model.Price > 0)
            {
                model.Price = _model.Price;
            }
            if (!string.IsNullOrEmpty(_model.Category))
            {
                model.Category = _model.Category;
            }
            if (!string.IsNullOrEmpty(_model.ImageURL))
            {
                model.ImageURL = _model.ImageURL;
            }

            await _repository.UpdateBook(model);

            return "Book Updated Successfully";
        }

        public async Task<string> DeleteBook(int id)
        {
            var book = await _repository.GetBookById(id);

            if (book == null)
            {
                return "Book Not Found";
            }

            await _repository.DeleteBook(book);

            return "Book Deleted Successfully";
        }
    }
}
