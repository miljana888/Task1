using Microsoft.EntityFrameworkCore;
using NotesApplication.Models;
using System.Drawing;

namespace NotesApplication.Service
{
    public class NoteService
    {
        private readonly AppDbContext _context;

        public NoteService(AppDbContext context)
        {
            this._context = context;
        }

        public IQueryable<Note> GetAll()
        {
            return _context.Notes.Include(n => n.User);
        }

        public Note GetNote(int id)
        {
            return _context.Notes.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<Note> FilterBy (string filter)
        {
            return _context.Notes.Include(n => n.User).Where(n => n.Text.Contains(filter.ToLower()));
        }

        /*public IQueryable<Note> GetByName(string name)
        {
            return _context.Notes.Include(n => n.User)
                .Where(n => n.User.FirstName == name);
        }*/

        public void Create(Note note)
        {
            _context.Notes.Add(note);
            _context.SaveChanges();
        }

        public void Update(Note note)
        {
            _context.Entry(note).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
        public string ConvertEnumToString(Color color)
        {
            return color.ToString();
        }

        public Color ConvertStringToEnum(string color)
        {
            return (Color)Enum.Parse(typeof(Color), color);
        }

        public void DeleteNote(Note note)
        {
            _context.Notes.Remove(note);
            _context.SaveChanges();
        }

    }
}
