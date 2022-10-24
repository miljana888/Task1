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
            var notes = _noteService.FilterBy(filter).ToList();
            return Ok(notes);
        }

        [HttpPost]
        public IActionResult CreateNote(AddNoteDTO addNoteDTO)
        { 
            var note = _noteService.Create(addNoteDTO);
            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, UpdateNoteDto updateNoteDto)
        {
            try
            {
                _noteService.Update(id, updateNoteDto);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(updateNoteDto);
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
