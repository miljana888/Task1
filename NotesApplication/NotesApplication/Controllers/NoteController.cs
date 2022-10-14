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
          /*[HttpGet]
          public IActionResult GetNotes()
          {
              return Ok(_noteService.GetAll().ToList());
          }*/

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

        /*[HttpPost]
        public IActionResult CreateNote(Note note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _noteService.Create(note);
            return CreatedAtAction("GetNote", new { id = note.Id }, note);
            
        }*/
        [HttpPost]
        public IActionResult CreateNote(AddNoteDTO addNoteDTO)
        {
            var note = new Note
            {
                Text = addNoteDTO.Text,
                color = Enum.Parse<Color>(addNoteDTO.Color, true),
                UserId = 1
            };
            _noteService.Create(note);
            return CreatedAtAction("GetNote", new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(int id, AddNoteDTO addNoteDTO)
        {
            var note = _noteService.GetNote(id);
            if(note != null)
            {
                note.Text = addNoteDTO.Text;
                note.color = Enum.Parse<Color>(addNoteDTO.Color, true);
            }
            try
            {
                _noteService.Update(note);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(note);

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteNote(int id)
        {
            var note = _noteService.GetNote(id);
            if(note == null)
            {
                return NotFound();
            }
            _noteService.DeleteNote(note);
            return NoContent();
        }

    }
}
