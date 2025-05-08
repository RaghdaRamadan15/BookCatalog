using BookLending.Application.Dtos;
using BookLending.Domain.Interfaces;
using BookLending.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLending.Application.MapSter;
using Mapster;
using System.Net;

namespace BookLending.Application.Services
{
    public class BookServices
    {
        private readonly IBookRepository bookRepository;
        public BookServices(IBookRepository _bookRepository) 
        {
            bookRepository= _bookRepository;
        }
        #region Create book 
        public async Task<GetBook> CreateAsync(CreatingBook bookDto)
        {
            var book= bookDto.Adapt<Book>();
            var result=await bookRepository.AddAsync(book);
            var returnBook= result.Adapt<GetBook>();
            return returnBook;
        }
        #endregion


        #region GetBookById

       
        public async Task<GetBook> GetBookByIdAsync(int bookId)
        {
            
            var result = await bookRepository.GetByIdAsync(bookId);
            var returnBook = result.Adapt<GetBook>();
            return returnBook;
        }
        #endregion

        #region update book 
        public async Task<Result<Book>> EditAsync(CreatingBook bookDto,int bookId)
        {
            Result<Book> result = new Result<Book>();

            var book = bookDto.Adapt<Book>();
            var value = await bookRepository.UpdateAsync(book, bookId);
            if (value != null)
            {
                result.Message = "updated ";
                result.Data = value;
            }
            result.Message = "please enter correct id";
            return result;
        }
        #endregion

        #region delete book
        public async Task<Result<Book>> DeleteAsync(int bookId)
        {
            Result<Book> result = new Result<Book>();
            var value = await bookRepository.DeleteAsync(bookId);
            if (value != null) 
            {
               
                result.Message = "the book is delete";
                result.Data = value;

            }
            result.Message = "please enter write id for book";


            return result;
        }
        #endregion





        #region get all
        public async Task<List<Result<GetBook>>> GetBooks()
        {
            List<Result<GetBook>> books = new List<Result<GetBook>>();
            var result = await bookRepository.GetAllBooksAsync();

            if (result != null)
            {
                foreach (var book in result)
                {
                    Result<GetBook> returnBook = new Result<GetBook>();
                     returnBook.Data = book.Adapt<GetBook>();
                   

                    books.Add(returnBook);
                }
            }

            return books;
        }
        #endregion

    }
}
