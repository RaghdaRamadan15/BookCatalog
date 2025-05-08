using BookLending.Application.Dtos;
using BookLending.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Application.Interface
{
    public interface IBorrowingServce
    {
        //Task<Result<BorrowingBook>> GetBorrowAsync(int Id);

        Task<Result<Borrowing?>> AddBorrowAsync(CreateBrowingDto borrow);
        Task<string> UpdateBorrowAsync(int BookId, string UserId);
        Task<IEnumerable<Result<BorrowingBook?>>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Result<BorrowingBook?>>> GetByBookIdAsync(int bookID);
        Task<IEnumerable<Result<BorrowingBook?>>> GetAll();
        Task<IEnumerable<Result<CreateBrowingDto?>>> GetOverdueBorrowingsAsync();


    }
}
