using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TEST.Data;
using TEST.Models;

namespace TEST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly TestDbContext dbContext;
        public TestController(TestDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetTable()
        {
            return Ok(await dbContext.Test.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRecord([FromRoute] Guid id)
        {
            var record = await dbContext.Test.FindAsync(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecord(AddRecord addRecordRequest)
        {
            var record = new Records()
            {
                Id = Guid.NewGuid(),
                Product = addRecordRequest.Product,
                Category = addRecordRequest.Category
            };
            await dbContext.Test.AddAsync(record);
            await dbContext.SaveChangesAsync();

            return Ok(record);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRecord([FromRoute] Guid id, UpdateRecord updateRecordRequest)
        {
            var record = await dbContext.Test.FindAsync(id);
            if (record != null) 
            {
                record.Product = updateRecordRequest.Product;
                record.Category = updateRecordRequest.Category;

                await dbContext.SaveChangesAsync();
                return Ok(record);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRecord([FromRoute] Guid id)
        {
            var record = await dbContext.Test.FindAsync(id);
            if (record != null)
            {
                dbContext.Remove(record);
                await dbContext.SaveChangesAsync();
                return Ok(record);
            }
            return NotFound();
        }

    }
}
