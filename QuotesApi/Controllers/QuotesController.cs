using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesApi.Data;
using QuotesApi.Models;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private QuotesDbContext _quoteDbContext;  // = new QuotesDbContext();

        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quoteDbContext = quotesDbContext;
        }


        // GET: api/Quotes
        [HttpGet]
        //public IEnumerable<Quote> Get()
        //{
        //    return _quoteDbContext.Quotes;
        //}
        public IActionResult Get(string sort)
        {
            //return BadRequest();
            //return NotFound();
            //return StatusCode(401);
            //return StatusCode(StatusCodes.Status200OK);

            //Adding sorting
            IQueryable<Quote> quotes;
            switch(sort)
            {
                case "desc":
                    quotes = _quoteDbContext.Quotes.OrderByDescending(q => q.CreatedAt);
                    break;
                case "asc":
                    quotes = _quoteDbContext.Quotes.OrderBy(q => q.CreatedAt);
                    break;
                default:
                    quotes = _quoteDbContext.Quotes;
                    break;
            }


            //return Ok(_quoteDbContext.Quotes);
            return Ok(quotes);
        }

        [HttpGet("[action]")]
        //[Route("[action]")]
        public IActionResult PagingQuote(int? pageNumber, int? pageSize)
        {
            var quotes = _quoteDbContext.Quotes;
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;

            return Ok(quotes.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }



        // GET: api/Quotes/5
        [HttpGet("{id}", Name = "Get")]
        public Quote Get(int id)
        {
            var quote = _quoteDbContext.Quotes.Find(id);
            return quote;
        }

        // api/Quotes/Test/1
        [HttpGet("[action]/{id}")]
        public int Test(int id)
        {
            return id;
        }

        // POST: api/Quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            _quoteDbContext.Quotes.Add(quote);
            _quoteDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Quotes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            var entity = _quoteDbContext.Quotes.Find(id);
            if (entity == null)
            {
                return NotFound("No record found for this id");
            }
            else
            {
                entity.Title = quote.Title;
                entity.Author = quote.Author;
                entity.Description = quote.Description;
                entity.Type = quote.Type;
                entity.CreatedAt = quote.CreatedAt;    
                _quoteDbContext.SaveChanges();
                return Ok("Record updated successfully");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var quote = _quoteDbContext.Quotes.Find(id);
            if (quote == null)
            {
                return NotFound("Quote not found");
            }
            else
            {
                _quoteDbContext.Quotes.Remove(quote);
                _quoteDbContext.SaveChanges();
                return Ok("Quote deleted");
            }
        }
    }
}
