using BookLending.Application.Dtos;
using BookLending.Application.Interface;
using BookLending.Domain.Interfaces;
using BookLending.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace BookLending.Application.Services
{
    public class BorrowingServce : IBorrowingServce
    {
        private readonly IUserBorrowingRepository userBorrowingRepository;
        public BorrowingServce(IUserBorrowingRepository _userBorrowing) 
        {
            userBorrowingRepository = _userBorrowing;
        }
        #region AddBorrow
        public async Task<Result<Borrowing>> AddBorrowAsync(CreatRequstBrorrow borrow)
        {
            var borrowing = borrow.Adapt<Borrowing>();

            var value = await userBorrowingRepository.AddBorrowingAsync(borrowing);
            if (value != null)
            {
                return new Result<Borrowing>
                {
                    Data = value,
                    Message = "Congratulations takeing this book",
                    IsSuccess = true,
                };


            }
            return new Result<Borrowing>
            {
                Data = value,
                Message = "User already borrowed a book.",
                IsSuccess = false,
            };


        }
        #endregion

        #region return All case of books
        public async Task<IEnumerable<BorrowingBook>> GetAll()
        {
            List<BorrowingBook> results = new List<BorrowingBook>();
            var value = await userBorrowingRepository.GetBorrowings();
            if (value != null)
            {
                foreach (var item in value)
                {
                    BorrowingBook data = new BorrowingBook 
                    { 
                    
                            BookName = item.Book?.Name,
                            UserName = item.User?.UserName,
                            CaseBook = item.IsReturned ? "Returned" : "Not Returned"
                        
                    };
                   
                    
                    results.Add(data);

                }



            }
            return results;

        }

        #endregion


        #region return list from Member by bookID
        public async Task<IEnumerable<BorrowingBook>> GetByBookIdAsync(int bookID)
        {
            List<BorrowingBook> results = new List<BorrowingBook>();
            var value = await userBorrowingRepository.GetByIdsync(bookID);
            if (value != null)
            {
                foreach (var item in value)
                {
                    BorrowingBook data = new BorrowingBook
                    {
                       
                            BookName = item.Book.Name,
                            UserName = item.User.UserName,
                            CaseBook = item.IsReturned ? "Returned" : "Not Returned"
                        

                    };
                    
                    
                    results.Add(data);

                }



            }
            return results;
        }

        #endregion

        #region books for Member
        public async Task<IEnumerable<BorrowingBook>> GetByUserIdAsync(string userId)
        {
            List<BorrowingBook> results = new List<BorrowingBook>();
            var value = await userBorrowingRepository.GetByUserIdAsync(userId);
            if (value != null)
            {
                foreach (var item in value)
                {
                    BorrowingBook data = new BorrowingBook
                    {
                       
                            BookName = item.Book.Name,
                            UserName = item.User.UserName,
                            CaseBook = item.IsReturned ? "Returned" : "Not Returned"
                       
                    };
                   
                    results.Add(data);

                }



            }
            return results;
        }

        #endregion
        #region display not return
        public async Task<IEnumerable<CreateBrowingDto?>> GetOverdueBorrowingsAsync()
        {
            List<CreateBrowingDto> results = new List<CreateBrowingDto>();
            var value = await userBorrowingRepository.GetOverdueBorrowingsAsync();
            if (value != null)
            {
                foreach (var item in value)
                {
                   CreateBrowingDto data = new CreateBrowingDto
                    {
                           BorrowDate = item.BorrowDate,
                           UserId = item.UserId,
                           BookId = item.BookId,
                           
                    };
                   
                    results.Add(data);

                }



            }
            return results;


        }

        #endregion


        #region return book
        public async Task<string> UpdateBorrowAsync(int BookId,string UserId)
        {
            var  result=await userBorrowingRepository.UpdateBorrowingAsync(BookId, UserId);
            return result;
        }

       

        #endregion






    }
}
