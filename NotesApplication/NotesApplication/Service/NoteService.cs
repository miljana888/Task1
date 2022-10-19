using Microsoft.EntityFrameworkCore;
using NotesApplication.Models;
using NotesApplication.Models.DTO;
using NotesApplication.Models.Enums;

namespace NotesApplication.Service
{
    public class NoteService
    {
        private readonly AppDbContext _context;

        public NoteService(AppDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Note>GetAll()
        {
            return _context.Notes;
        }

        public Note GetNote(int id)
        {
            return _context.Notes.Include(n => n.User).FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<Note>FilterBy(string filter)
        {
            return _context.Notes.Where(n => n.Text.ToLower().Contains(filter.ToLower()));
        }

        public void Create(AddNoteDTO addNoteDTO)
        {
            var note = new Note
            {
                Text = addNoteDTO.Text,
                Color = Enum.Parse<Color>(addNoteDTO.Color, true),
                UserId = 1
            };
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public void Update(int id, AddNoteDTO addNoteDTO)
        {
            var note = GetNote(id);
            if (note != null)
            {
                note.Text = addNoteDTO.Text;
                note.Color = Enum.Parse<Color>(addNoteDTO.Color, true);
            }
            throw new ArgumentException(message: "There is no note with that id");

            _context.Entry(note).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteNote(int id)
        {
             var note = GetNote(id);
            if (note == null)
                throw new ArgumentException(message: "There is no note with that id");

            _context.Notes.Remove(note);
            _context.SaveChanges();
        }
    }
}
