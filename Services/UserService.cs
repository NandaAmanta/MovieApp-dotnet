using MovieApp.Data;
using MovieApp.Dtos.ModelInterfaces;
using MovieApp.Models;

namespace MovieApp.Services;

public class UserService(MovieAppDataContext context)
{
    private readonly MovieAppDataContext _context = context;

    public async Task<IUser> detail(long id)
    {
        User? user = await _context.User.FindAsync(id) ?? throw new KeyNotFoundException();
        return user;
    }

}