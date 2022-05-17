using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }

        public BookModel AddBook(AddBook addBook)
        {
            try
            {
                return bookRL.AddBook(addBook);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BookModel UpdateBook(BookModel updateBook)
        {
            try
            {
                return bookRL.UpdateBook(updateBook);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteBook(int bookId)
        {
            try
            {
                return bookRL.DeleteBook(bookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BookModel> GetAllBooks()
        {
            try
            {
                return bookRL.GetAllBooks();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BookModel GetBookById(int bookId)
        {
            try
            {
                return bookRL.GetBookById(bookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
