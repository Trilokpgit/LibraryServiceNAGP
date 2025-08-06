using LibraryService.Database;
using LibraryService.DTOs;
using LibraryService.Helpers;
using LibraryService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryService.Controllers
{
    [ApiController]
    [Route("api/library")]
    public class LibraryController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public LibraryController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            CommonResponse response;
            try
            {
                var books = await _context.Books.ToListAsync();
                response = CommonResponseBuilder.BuildResponse(true, 200, books);
            }
            catch (Exception ex)
            {
                response = CommonResponseBuilder.BuildResponse(false, 500, null, new ErrorDetail { ErrorId = 500, Message = ex.Message, Description = ex.ToString() });
                return StatusCode(500, response);
            }
            return Ok(response);
        }

        [HttpPost(Name = "AddBook")]
        public async Task<IActionResult> AddBook(Book book)
        {
            CommonResponse response;
            try
            {
                var addedBook = await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
                response = CommonResponseBuilder.BuildResponse(true, 200, new { NewBookId = addedBook.Entity.id });

            }
            catch (Exception ex)
            {
                response = CommonResponseBuilder.BuildResponse(false, 500, null, new ErrorDetail { ErrorId = 500, Message = ex.Message, Description = ex.ToString() });
                return StatusCode(500, response);
            }
            return Ok(response);
        }


    }
}
