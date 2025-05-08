using BookLending.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLending.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(int id);
        Task<IEnumerable<Book?>> GetAllBooksAsync();
        Task<Book> AddAsync(Book book);
        Task<Book> UpdateAsync(Book book,int id);
        Task<Book> DeleteAsync(int id);
    }
}
