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

        Task<Result<Borrowing?>> AddBorrowAsync(CreatRequstBrorrow borrow);
        Task<string> UpdateBorrowAsync(int BookId, string UserId);
        Task<IEnumerable<BorrowingBook?>> GetByUserIdAsync(string userId);
        Task<IEnumerable<BorrowingBook?>> GetByBookIdAsync(int bookID);
        Task<IEnumerable<BorrowingBook?>> GetAll();
        Task<IEnumerable<CreateBrowingDto?>> GetOverdueBorrowingsAsync();


    }
}
