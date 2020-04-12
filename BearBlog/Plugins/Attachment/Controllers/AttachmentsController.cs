using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BearBlog.Models;
using Microsoft.Extensions.Configuration;

namespace BearBlog.Plugins.Attachment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly BloggingContext _context;
        private readonly IConfiguration _config;

        public AttachmentsController(BloggingContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Attachments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Attachment>>> GetAttachments()
        {
            return await _context.Attachments.ToListAsync();
        }

        // GET: api/Attachments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Attachment>> GetAttachment(int id)
        {
            var attachment = await _context.Attachments.FindAsync(id);

            if (attachment == null)
            {
                return NotFound();
            }

            return attachment;
        }

        [HttpGet("{filename}")]
        public async Task<IActionResult> GetAttachment(string filename)
        {
            var attachment = await _context.Attachments.SingleAsync(a => a.Filename == filename);

            if (attachment == null)
            {
                return NotFound();
            }

            return File(System.IO.File.OpenRead(Path.Combine(_config["UploadFolder"], filename)), "application/octet-stream", attachment.OriginalFilename);
        }

        // PUT: api/Attachments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttachment(int id, Models.Attachment attachment)
        {
            if (id != attachment.Id)
            {
                return BadRequest();
            }

            _context.Entry(attachment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttachmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Attachments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Models.Attachment>> PostAttachment(IFormFile file)
        {
            var extenstion = Path.GetExtension(file.FileName);
            var randomFilename = $"{Guid.NewGuid().ToString()}{extenstion}";
            var randomFilePath = Path.Combine(_config["UploadFolder"], randomFilename);
            Directory.CreateDirectory(Path.GetDirectoryName(randomFilePath));
            await using var stream = System.IO.File.Create(randomFilePath);
            await file.CopyToAsync(stream);
            var attachment = new Models.Attachment
            {
                OriginalFilename = file.FileName,
                Filename = randomFilename,
                FilePath = randomFilePath,
                FileExtension = extenstion,
                FileSize = file.Length,
                Mime = file.ContentType,
                Md5 = BitConverter.ToString(MD5.Create().ComputeHash(stream)).Replace("-", "").ToLowerInvariant()
            };

            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAttachment", new {id = attachment.Id}, attachment);
        }

        // DELETE: api/Attachments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Models.Attachment>> DeleteAttachment(int id)
        {
            var attachment = await _context.Attachments.FindAsync(id);
            if (attachment == null)
            {
                return NotFound();
            }

            _context.Attachments.Remove(attachment);
            await _context.SaveChangesAsync();

            return attachment;
        }

        private bool AttachmentExists(int id)
        {
            return _context.Attachments.Any(e => e.Id == id);
        }
    }
}