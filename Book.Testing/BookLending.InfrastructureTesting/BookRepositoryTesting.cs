using BookLending.Application.Services;
using BookLending.Infrastructure.Models;
using BookLending.Infrastructure.Services;
using BookLending.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using BookLending.Domain.Interfaces;

namespace BookLending.UnitTests
{
    public class BookRepositoryTesting
    {
        private readonly BookRepository bookRepository;
        private readonly BookContext context;
        
        public BookRepositoryTesting()
        {
            //option database in mommery
           var  options = new DbContextOptionsBuilder<BookContext>()
            .UseInMemoryDatabase(databaseName: "BookTestDb")
            .Options;

            context = new BookContext(options);

            bookRepository = new BookRepository(context);  

           
        }
        #region Add Book
        [Fact]
        public async Task AddBookAsyncSuccessfully()
        {
            
            var bookToAdd = new BookLending.Domain.Models.Book
            {
                Name = "Test Book",
                Quantity = 5,
                IsDeleted = false
            };



            var addedBook = await bookRepository.AddAsync(bookToAdd);

            
            Assert.NotNull(addedBook);
            Assert.Equal("Test Book", addedBook.Name);
            Assert.Equal(5, addedBook.Quantity);

            
            var bookInDb = await context.books.FirstOrDefaultAsync(b => b.Name == "Test Book");
            Assert.NotNull(bookInDb);
            Assert.Equal(addedBook.Name, bookInDb.Name);
        }
        #endregion


        #region Update  book
        [Fact]
        public async Task Update()
        {
            var book = new BookLending.Domain.Models.Book
            {
                Name = "Test Book",
                Quantity = 5,
                IsDeleted = false,
                Id = 1

            };
            await context.books.AddAsync(book);
            await context.SaveChangesAsync();
           
            var bookUpdate = new BookLending.Domain.Models.Book
            {
                Name = "Test Book",
                Quantity = 5,
                IsDeleted = true,
                Id = book.Id,

            };
            
            var updateBook = await bookRepository.UpdateAsync(bookUpdate, book.Id);

            Assert.NotNull(updateBook);
            Assert.Equal("Test Book", updateBook.Name);
            Assert.Equal(5, updateBook.Quantity);
        }

        [Fact]
        public async Task UpdateAsyncBookDoesNotExist()
        {

            var bookUpdate = new BookLending.Domain.Models.Book
            {
                Id = 999,
                Name = "Updated Book Name",
                Quantity = 10,
                IsDeleted = true
            };

            var result = await bookRepository.UpdateAsync(bookUpdate, 999);


            Assert.Null(result);
        }
        #endregion




        #region delete book
        [Fact]
        public async Task DeleteAsync()
        {

            var book = new BookLending.Domain.Models.Book
            {
                Name = "Test Book",
                Quantity = 5,
                IsDeleted = false,
                Id = 4

            };
            //var delbook = new BookLending.Domain.Models.Book
            //{
            //    Name = "Test Book",
            //    Quantity = 5,
            //    IsDeleted = true,
            //    Id = 1

            //};

            await context.books.AddAsync(book);
            await context.SaveChangesAsync();
            var result = await bookRepository.DeleteAsync(4);
            Assert.NotNull(result);
            Assert.Equal("Test Book", result.Name);


        }
        #endregion

    }
}
