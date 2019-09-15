using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Api.Data.Repository
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        private readonly DbSet<UserEntity> _dataSet;

        public UserRepository(EstudoDDDWebApiContext context) : base(context)
        {
            _dataSet = context.Set<UserEntity>();
        }

        public async Task<UserEntity> FindByLogin(string email)
        {
            return await _dataSet.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}
