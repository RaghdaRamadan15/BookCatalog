using BookLending.Application.Dtos;
using BookLending.Application.Services;
using BookLending.Domain.Interfaces;
using BookLending.Domain.Models;
using Mapster;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Testing.BoohLending.AppTesting
{
    public class BookServicesTesting
    {
        private readonly Mock<IBookRepository> bookRepository;
        private readonly BookServices bookServices;
        public BookServicesTesting()
        {
            bookRepository = new Mock<IBookRepository>();
            bookServices =new BookServices(bookRepository.Object);

        }
        #region create book Successfully
        [Fact]
        public async Task CreateBookAsyncSuccessfully()
        {
            var createBookDto = new CreatingBook
            {
                Name = "Happy",
                Quantity=2
            };
            var book = new BookLending.Domain.Models.Book
            {
                Name = "Happy",
                Quantity = 2

            };
            var returnCreateBook = new BookLending.Domain.Models.Book
            {
                IsDeleted = false,
                Quantity = 2,
                Name = "Happy",
                Id = 1,
            };
            bookRepository.Setup(x => x.AddAsync(It.IsAny<BookLending.Domain.Models.Book>()))
                .ReturnsAsync(returnCreateBook);
            //action
            var result = await bookServices.CreateAsync(createBookDto);

            Assert.NotNull(result);  
            Assert.IsType<GetBook>(result);  
            Assert.Equal(returnCreateBook.Name, result.Name);  
            Assert.Equal(returnCreateBook.Quantity, result.Quantity);  
            Assert.Equal(returnCreateBook.Id, result.Id);

        }
        #endregion

        #region Get Book ById Successfully

        [Fact]
        public async Task GetBookByIdAsync()
        {
            var returnBook = new BookLending.Domain.Models.Book
            {
                IsDeleted = false,
                Quantity = 2,
                Name = "Happy",
                Id = 1,
            };

             bookRepository.Setup(x=>x.GetByIdAsync(1)).ReturnsAsync(returnBook);
            //action
            var result = await bookServices.GetBookByIdAsync(1);
            Assert.NotNull(result);
            Assert.IsType<GetBook>(result);
            Assert.Equal(returnBook.Name, result.Name);
            Assert.Equal(returnBook.Quantity, result.Quantity);
            Assert.Equal(returnBook.Id, result.Id);


        }
        #endregion


        #region update book Successfully

        [Fact]
        public async Task EditAsync()
        {
            var idBook = 1;
            var updateBookDto = new UpdateBook
            {
                IsDeleted = false,
                Quantity = 2,
                Name = "Happy",
            };

            var book = new BookLending.Domain.Models.Book
            {
                IsDeleted = false,
                Quantity = 2,
                Name = "Happy",
                Id = idBook,
            };

           
            bookRepository.Setup(x => x.UpdateAsync(It.IsAny<BookLending.Domain.Models.Book>(), idBook))
                          .ReturnsAsync(book);

           
            var result = await bookServices.EditAsync(updateBookDto, idBook);

            
            Assert.NotNull(result);

            
            Assert.NotNull(result.Data);

            
            Assert.Equal(book.Name, result.Data.Name);
            Assert.Equal(book.Quantity, result.Data.Quantity);
            Assert.Equal(book.Id, result.Data.Id);
        }

        #endregion



    }
}
