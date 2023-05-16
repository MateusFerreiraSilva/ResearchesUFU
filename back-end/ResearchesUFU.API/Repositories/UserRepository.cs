using Microsoft.EntityFrameworkCore;
using ResearchesUFU.API.Context;
using ResearchesUFU.API.Models;
using ResearchesUFU.API.Repositories.Interfaces;

namespace ResearchesUFU.API.Repositories;

public class UserRepository : IBaseRepository<User>
{

    private readonly ResearchesUFUContext _dbContext;
    private readonly DbSet<User> _userRepository;

    public UserRepository(ResearchesUFUContext dbContext)
    {
        _dbContext = dbContext;
        _userRepository = _dbContext.Users;
    }

    public async Task<User> GetOneAsync(int id)
    {
        return await _userRepository.FindAsync(id);
    }

    public async Task<IQueryable<User>> GetAllAsync()
    {
        return (await _userRepository.ToListAsync()).AsQueryable();
    }

    public void Insert(User user)
    {
        _userRepository.Add(user);
    }

    public void Update(User oldUser, User newUser)
    {
        _dbContext.Entry(oldUser).CurrentValues.SetValues(newUser);
    }

    public void Delete(User user)
    {
        _userRepository.Remove(user);
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}