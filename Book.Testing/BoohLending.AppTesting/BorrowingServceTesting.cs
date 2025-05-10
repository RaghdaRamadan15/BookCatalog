using BookLending.Application.Dtos;
using BookLending.Application.Interface;
using BookLending.Application.Services;
using BookLending.Domain.Interfaces;
using BookLending.Domain.Models;
using Microsoft.VisualBasic;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Book.Testing.BoohLending.AppTesting
{
    public class BorrowingServceTesting
    {
        private readonly Mock<IUserBorrowingRepository> userBorrowingRepository;
        private readonly BorrowingServce borrowingServce;
        public BorrowingServceTesting()
        {
            userBorrowingRepository = new Mock<IUserBorrowingRepository>();
            borrowingServce=new BorrowingServce(userBorrowingRepository.Object);
        }

        #region Add Borrow 
        [Fact]
        public async Task AddBorrowAsync()
        {


            var brorrowbBook = new  CreatRequstBrorrow
            {
                BookId = 1,
                UserId = "1",

            };
            var brorrow = new Borrowing
            {
                DueDate = DateTime.Today.AddDays(7),
                BorrowDate = DateTime.Today,
                UserId = "1",
                BookId = 1,
                Id = 1,
                IsReturned = false,
                ReturnDate = null,
            };

            userBorrowingRepository.Setup(x => x.AddBorrowingAsync(It.IsAny<Borrowing>())).ReturnsAsync(brorrow);
           //act
           var result = await borrowingServce.AddBorrowAsync(brorrowbBook);
            Assert.NotNull(result);
            Assert.Equal(brorrow, result.Data);
        }
        #endregion

        #region return book
        [Fact]
        public async Task UpdateBorrowAsync()
        {
            var bookId = 1;
            var userId = "1";
            var retuenValue = "Returned successfully";
            userBorrowingRepository.Setup(x=>x.UpdateBorrowingAsync(bookId, userId)).ReturnsAsync(retuenValue);
            var result = await borrowingServce.UpdateBorrowAsync(bookId,userId);
            Assert.Equal(result, retuenValue);
        }

       

        #endregion




    }
}
