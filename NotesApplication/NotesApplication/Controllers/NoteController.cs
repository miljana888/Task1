using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesApplication.Models;
using NotesApplication.Models.DTO;
using NotesApplication.Models.Enums;
using NotesApplication.Service;

namespace NotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NoteController(AppDbContext context)
        {
            _noteService = new NoteService(context);
        }

        [HttpGet("{id}")]
        public IActionResult GetNote(int id)
        {
            var note = _noteService.GetNote(id);
            if(note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpGet]
        public IActionResult GetNotes([FromQuery] string filter)
        {
            if(filter == null)
            {
                return BadRequest();
            }
            return Ok(_noteService.FilterBy(filter).ToList());
        }

        [HttpPost]
        public IActionResult CreateNote(AddNoteDTO addNoteDTO)
        { 
            _noteService.Create(addNoteDTO);
            return CreatedAtAction("GetNote", new { id = addNoteDTO }, addNoteDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, AddNoteDTO addNoteDTO)
        {
            try
            {
                _noteService.Update(id, addNoteDTO);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(addNoteDTO);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            try
            {
                _noteService.DeleteNote(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            return NoContent();
        }
    }
}
