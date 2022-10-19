using Microsoft.EntityFrameworkCore;
using NotesApplication.Models;

namespace NotesApplication.Service
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            this._context = context;
        }

        public void Create (User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public void Update(int id, User user)
        {

            if (id != user.Id)
            {
                throw new ArgumentException(message: "There is no user with that id");
            }
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            var user = GetUser(id);
            if (user == null)
                throw new ArgumentException(message: "There is no user with that id");

            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
