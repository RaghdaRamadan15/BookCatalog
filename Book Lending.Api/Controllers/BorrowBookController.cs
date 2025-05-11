using BookLending.Application.Dtos;
using BookLending.Application.Services;
using BookLending.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Lending.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowBookController : ControllerBase
    {
        private readonly BorrowingServce borrowingServce;
        public BorrowBookController(BorrowingServce _borrowingServce) 
        {

            borrowingServce=_borrowingServce;
        }
        [HttpPost("Borrow")]
        [Authorize]
        public async Task<IActionResult> Addborrow(CreatRequstBrorrow borrow)
        {
            if (ModelState.IsValid)
            {
                var result= await borrowingServce.AddBorrowAsync(borrow);
                return Ok(result);
            }

            return BadRequest(ModelState.IsValid);

        }

        [HttpGet("DisplayAll")]
        //[Authorize(Roles = "Admin")]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> GetAllborrow()
        {

            var result = await borrowingServce.GetAll();
                return Ok(result);
            

        }


        [HttpGet("DisplayBororrwingMember")]
        [Authorize]
        public async Task<IActionResult> GetMemberForBOOK(int bookID)
        {

            var result = await borrowingServce.GetByBookIdAsync(bookID);
            return Ok(result);


        }




        [HttpGet("DisplaybooksforOneMember")]
        [Authorize]
        public async Task<IActionResult> GetMemberForBOOK(string userId)
        {

            var result = await borrowingServce.GetByUserIdAsync(userId);
            return Ok(result);


        }

        [HttpPut("ReturnBook")]
        [Authorize]
        public async Task<IActionResult> Update(int BookId,string userId)
        {

            var result = await borrowingServce.UpdateBorrowAsync(BookId, userId); ;
            return Ok(result);


        }


        [HttpGet("DisplaybooksOverDue")]
        //[Authorize(Roles = "Admin")]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> GetLateBOOK()
        {

            var result = await borrowingServce.GetOverdueBorrowingsAsync();
            return Ok(result);


        }











    }
}
