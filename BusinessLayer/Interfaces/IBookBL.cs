using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IBookBL
    {
        public BookModel AddBook(AddBook addBook);
        public BookModel UpdateBook(BookModel updateBook);
        public string DeleteBook(int bookId);
        public List<BookModel> GetAllBooks();
        public BookModel GetBookById(int bookId);
    }
}
