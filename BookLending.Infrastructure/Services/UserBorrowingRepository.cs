using BookLending.Domain.Interfaces;
using BookLending.Domain.Models;
using BookLending.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace BookLending.Infrastructure.Services
{
    public class UserBorrowingRepository : IUserBorrowingRepository
    {
        private BookContext context;
        private readonly IConfiguration config;
        //private readonly UserManager<IdentityUser> userManager;
        public UserBorrowingRepository(BookContext _context,IConfiguration _config ) 
        {
            context= _context;
            config= _config;
            //userManager = _userManager;


        }
        #region Borrowing book
        public async Task<Borrowing> AddBorrowingAsync(Borrowing borrowing)
        {
            var userId = borrowing.UserId;
            Borrowing borrow = new Borrowing();
            //var userExist= await userManager.FindByIdAsync(userId);
            //var bookExist = await context.books.FirstOrDefaultAsync(x=>x.Id==borrowing.BookId);
            //if (userExist==null || bookExist==null)
            //{
            //    return null;
            //}
            var isCanBrorrow = await context.borrows.Where(x => x.UserId == userId && x.IsReturned == false)
                   .FirstOrDefaultAsync();
            if (isCanBrorrow == null)
            {


                borrow.UserId = userId;
                borrow.BookId = borrowing.BookId;
                borrow.BorrowDate = DateTime.Today;
                borrow.DueDate = DateTime.Today.AddDays(int.Parse(config["NumberDayBorrow:Number"]));
                borrow.IsReturned = false;
                var book = await context.books.FirstOrDefaultAsync(x => x.Id == borrowing.BookId);
                if (book != null)
                {
                    book.Quantity -= 1;
                    context.books.Update(book);
                }
                await context.borrows.AddAsync(borrow);

                await context.SaveChangesAsync();

                return borrow;

            }
            return null;
        }

        #endregion


        #region getAll return and borrow
        public async Task<IEnumerable<Borrowing?>> GetBorrowings()
        {
            return await context.borrows.AsNoTracking().Include(x => x.User).Include(x => x.Book).ToListAsync();



        }

        #endregion

        #region get one
        public async Task<IEnumerable<Borrowing?>> GetByIdsync(int id)
        {
            return await context.borrows.Where(x => x.BookId == id).AsNoTracking().Include(x => x.User).Include(x => x.Book).ToListAsync();

        }
        #endregion

        #region  get all process for the Member
        public async Task<IEnumerable<Borrowing?>> GetByUserIdAsync(string userId)
        {
            return await context.borrows.Where(x => x.UserId == userId).AsNoTracking().Include(x => x.User).Include(x => x.Book).ToListAsync();

        }
        #endregion


        #region getAll late
        public async Task<IEnumerable<Borrowing?>> GetOverdueBorrowingsAsync()
        {
            return await context.borrows.Where(x => x.IsReturned == false && x.DueDate < DateTime.Today).AsNoTracking().ToListAsync();

        }
        #endregion






        #region Return Book
        public async Task<string> UpdateBorrowingAsync(int bookId, string userId)
        {
            var borrowing = await context.borrows
                .FirstOrDefaultAsync(x => x.UserId == userId && x.BookId == bookId);

            if (borrowing == null)
                return "No borrowing record found for this book and user.";

            if (borrowing.IsReturned)
                return "This book was already returned.";

            
            borrowing.IsReturned = true;
            borrowing.ReturnDate = DateTime.Today;

            var book = await context.books.FirstOrDefaultAsync(x => x.Id == bookId);
            if (book != null)
            {
                book.Quantity += 1;
                context.books.Update(book);
            }

            context.borrows.Update(borrowing);
            await context.SaveChangesAsync();

            return "Returned successfully.";
        }
        #endregion

    }
}
