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
        public async Task<Result<Borrowing>> AddBorrowAsync(CreateBrowingDto borrow)
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
        public async Task<IEnumerable<Result<BorrowingBook>>> GetAll()
        {
            List<Result<BorrowingBook>> results = new List<Result<BorrowingBook>>();
            var value = await userBorrowingRepository.GetBorrowings();
            if (value != null)
            {
                foreach (var item in value)
                {
                    Result<BorrowingBook> data = new Result<BorrowingBook>
                    {
                        IsSuccess = true,
                        Data = new BorrowingBook
                        {
                            BookName = item.Book?.Name,
                            UserName = item.User?.UserName,
                            CaseBook = item.IsReturned ? "Returned" : "Not Returned"
                        }
                    };
                   
                    
                    results.Add(data);

                }



            }
            return results;

        }

        #endregion


        #region return list from Member by bookID
        public async Task<IEnumerable<Result<BorrowingBook>>> GetByBookIdAsync(int bookID)
        {
            List<Result<BorrowingBook>> results = new List<Result<BorrowingBook>>();
            var value = await userBorrowingRepository.GetByIdsync(bookID);
            if (value != null)
            {
                foreach (var item in value)
                {
                    Result<BorrowingBook> data = new Result<BorrowingBook>
                    {
                       IsSuccess = true,
                   
                        Data = new BorrowingBook
                        {
                            BookName = item.Book?.Name,
                            UserName = item.User?.UserName,
                            CaseBook = item.IsReturned ? "Returned" : "Not Returned"
                        }

                    };
                    
                    
                    results.Add(data);

                }



            }
            return results;
        }

        #endregion

        #region books for Member
        public async Task<IEnumerable<Result<BorrowingBook>>> GetByUserIdAsync(string userId)
        {
            List<Result<BorrowingBook>> results = new List<Result<BorrowingBook>>();
            var value = await userBorrowingRepository.GetByUserIdAsync(userId);
            if (value != null)
            {
                foreach (var item in value)
                {
                    Result<BorrowingBook> data = new Result<BorrowingBook>
                    {
                        IsSuccess = true,
                        Data = new BorrowingBook
                        {
                            BookName = item.Book?.Name,
                            UserName = item.User?.UserName,
                            CaseBook = item.IsReturned ? "Returned" : "Not Returned"
                        }
                    };
                   
                    results.Add(data);

                }



            }
            return results;
        }

        #endregion
        #region display not return
        public async Task<IEnumerable<Result<CreateBrowingDto?>>> GetOverdueBorrowingsAsync()
        {
            List<Result<CreateBrowingDto>> results = new List<Result<CreateBrowingDto>>();
            var value = await userBorrowingRepository.GetOverdueBorrowingsAsync();
            if (value != null)
            {
                foreach (var item in value)
                {
                    Result<CreateBrowingDto> data = new Result<CreateBrowingDto>
                    {
                        IsSuccess = true,
                        Data = new CreateBrowingDto
                        {
                            BorrowDate = item.BorrowDate,
                           UserId = item.UserId,
                            BookId = item.BookId,
                           
                        }
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
