﻿using System.Linq;
using System.Threading.Tasks;
using CompeTournament.Backend.Data;
using CompeTournament.Backend.Data.Entities;
using CompeTournament.Backend.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CompeTournament.Backend.Persistence.Implementations
{
    public class LeagueRepository : Repository<League>, ILeagueRepository
    {
        private readonly ApplicationDbContext _context;
        //private readonly IUserHelper _userHelper;

        //public GroupRepository(ApplicationDbContext context, IUserHelper userHelper) : base(context)
        //{
        //    _context = context;
        //    _userHelper = userHelper;
        //}
        public LeagueRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<League> GetWithGroup()
        {
            return _context.Leagues
                .Include(p=>p.Group)
                .AsNoTracking();
        }
        public async Task<League> GetByIdWithChildrens(int key)
        {
            var entity = await Context.Leagues.Where(p => p.Id == key)
                .Include(p => p.Teams)
                .FirstOrDefaultAsync();
            return entity;
        }
    }
}