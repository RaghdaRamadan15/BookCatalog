using BookLending.Application.Dtos;
using BookLending.Application.Services;
using BookLending.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Lending.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
       
        public BookServices bookServices;
        public BookController(BookServices _bookServices)
        {
           bookServices = _bookServices;
        }

        #region Add
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>AddBook(CreatingBook book)
        {
            if (ModelState.IsValid) 
            {
                var result = await bookServices.CreateAsync(book);
                return Ok(result);
            }
           return BadRequest(ModelState);
        }

        #endregion

        [HttpPut]
        [Authorize(Roles = "Admin")]
        #region updateBook
        public async Task<IActionResult> updateBook(CreatingBook book,int bookId)
        {
           
            if (ModelState.IsValid)
            {
                var result = await bookServices.EditAsync(book, bookId);
                return Ok(result);
            }
            return BadRequest(ModelState);
            
        }

        #endregion

        #region delete
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int bookId)
        {
            var result = await bookServices.DeleteAsync(bookId);
            return Ok(result);
        }

        #endregion
        #region Get one book
        [HttpGet("GetBook")]
        [Authorize]
        public async Task<IActionResult> GetOne(int bookId)
        {
            var result = await bookServices.GetBookByIdAsync(bookId);
            return Ok(result);
        }

        #endregion
        #region Get All
        [HttpGet("GetBooks")]
        [Authorize]
        public async Task<IActionResult> GetAllBook()
        {
            var result = await bookServices.GetBooks();
            return Ok(result);
        }


        #endregion

    }
}
