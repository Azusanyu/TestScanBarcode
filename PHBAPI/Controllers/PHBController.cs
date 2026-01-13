using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PHBAPI.Connection;
using PHBAPI.Model;
using System;

namespace PHBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PHBController : ControllerBase
    {
        private readonly PBHDbContext _context;

        public PHBController(PBHDbContext context)
        {
            _context = context;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _context.DocCaptureImages.ToListAsync();
            return Ok(data);
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _context.DocCaptureImages.FindAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DocCaptureImages model)
        {
            model.UploadDate = DateTime.Now;
            _context.DocCaptureImages.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }
        //--------------------------------------------------------------------------------
        //[HttpPost("upload")]
        //public async Task<IActionResult> Upload([FromForm] string soPhieu, [FromForm] IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("No file uploaded");

        //    using var ms = new MemoryStream();
        //    await file.CopyToAsync(ms);

        //    var item = new DocCaptureImages
        //    {
        //        SoPhieu = soPhieu,
        //        ImageData = ms.ToArray(),
        //        FileName = file.FileName,
        //        UploadDate = DateTime.Now
        //    };

        //    _context.DocCaptureImages.Add(item);
        //    await _context.SaveChangesAsync();

        //    return Ok(new { message = "Uploaded Successfully", id = item.ID });
        //}
        //----------------------------------------------------------------------
        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DocCaptureImages model)
        {
            var item = await _context.DocCaptureImages.FindAsync(id);
            if (item == null) return NotFound();

            item.SoPhieu = model.SoPhieu;
            item.ImageData = model.ImageData;
            item.FileName = model.FileName;
            item.UploadDate = model.UploadDate;

            await _context.SaveChangesAsync();
            return Ok(item);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.DocCaptureImages.FindAsync(id);
            if (item == null) return NotFound();
            _context.DocCaptureImages.Remove(item);
            await _context.SaveChangesAsync();
            return Ok(new { message = "Deleted" });
        }
    }
}
