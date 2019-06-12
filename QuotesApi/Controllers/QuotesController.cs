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
        public IActionResult Get()
        {
            //return BadRequest();
            //return NotFound();
            return Ok(_quoteDbContext.Quotes);
            //return StatusCode(401);
            //return StatusCode(StatusCodes.Status200OK);
   
        }


        // GET: api/Quotes/5
        [HttpGet("{id}", Name = "Get")]
        public Quote Get(int id)
        {
            var quote = _quoteDbContext.Quotes.Find(id);
            return quote;
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
