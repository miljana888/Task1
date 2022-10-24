using Microsoft.EntityFrameworkCore;
using NotesApplication.Models;
using NotesApplication.Models.DTO;

namespace NotesApplication.Service
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            this._context = context;
        }

        public User Create (AddUserDto addUserDto)
        {
            var user = new User()
            {
                FirstName = addUserDto.FirstName,
                LastName = addUserDto.LastName,
                Email = addUserDto.Email
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User GetUser(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }

        public User Update(int id, UpdateUserDto updateUserDto)
        {
            var user = GetUser(id);
            if (user != null)
            {
                user.FirstName = updateUserDto.FirstName;
                user.LastName = updateUserDto.LastName;
                user.Email = updateUserDto.Email;
                user.Id = updateUserDto.Id;    
            }
            else
            {
                throw new ArgumentException(message: "There is no user with that id");
            }
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return user;
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
