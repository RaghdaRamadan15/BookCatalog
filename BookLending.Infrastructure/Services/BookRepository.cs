using BookLending.Domain.Interfaces;
using BookLending.Domain.Models;
using BookLending.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Infrastructure.Services
{
    public class BookRepository: IBookRepository
    {
        public BookContext context;
        public BookRepository(BookContext _context) 
        {
            context= _context;


        }
        #region Add
        public async Task<Book> AddAsync(Book book)
        {
            await context.books.AddAsync(book);
            await context.SaveChangesAsync();
            return book;
        }

        #endregion

        #region delete
        public async Task<Book> DeleteAsync(int id)
        {
            //var book= await context.books.FirstOrDefaultAsync(x => x.Id == id);
            var book = GetBookById(context, id);

            if (book != null)
            {
                book.IsDeleted = true;
                context.books.Update(book);
                await context.SaveChangesAsync();
                return book;
            }
            return null;
        }

        #endregion

        public async Task<IEnumerable<Book?>> GetAllBooksAsync()
        {
            var books= await context.books.AsNoTracking().Where(x=>!x.IsDeleted&&x.Quantity>=1).ToListAsync();
            return books;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            var book = GetBookById(context, id);
            return book;
        }

        public async Task<Book> UpdateAsync(Book book, int id)
        {
            //var oldBook = await context.books.FirstOrDefaultAsync(x => x.Id == id);
            var oldBook =  GetBookById(context, id);
            if (oldBook != null) 
            {
                oldBook.Name = book.Name;
                oldBook.IsDeleted = book.IsDeleted;
                oldBook.Quantity = book.Quantity;
                 context.books.Update(oldBook);
                 await context.SaveChangesAsync();
                return oldBook;
            }
            return oldBook;


        }


        #region reapet query
        private static readonly Func<BookContext, int, Book> GetBookById =
            EF.CompileQuery((BookContext context, int id) =>
                context.books.AsNoTracking().FirstOrDefault(x => x.Id == id));

        #endregion
    }
}
