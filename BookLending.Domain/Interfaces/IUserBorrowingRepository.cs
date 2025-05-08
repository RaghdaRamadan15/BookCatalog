using BookLending.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Domain.Interfaces
{
    public interface IUserBorrowingRepository
    {
        Task<IEnumerable<Borrowing?>> GetByIdsync(int id);
        Task<IEnumerable<Borrowing?>> GetByUserIdAsync(string userId);
        Task<Borrowing?> AddBorrowingAsync(Borrowing borrowing);
        Task<string> UpdateBorrowingAsync(int BookId, string UserId);
        Task<IEnumerable<Borrowing?>> GetBorrowings();
        Task<IEnumerable<Borrowing?>> GetOverdueBorrowingsAsync();
    }
}
