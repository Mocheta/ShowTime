using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Repositories.Implementations
{
    public class FestivalImplement : GenericImplement<Festival>, IFestivalRepo
    {
        private readonly DbSet<Festival> _festivals;

        public FestivalImplement(ShowTimeDBContext context) : base(context)
        {
            _festivals = context.Set<Festival>();
        }
        public override async Task<IEnumerable<Festival>> GetAllAsync()
        {
            return await _festivals
                .Include(f => f.Artists)
                .Include(f => f.Lineups)
                .Include(f => f.Tickets)
                .ToListAsync();
        }
        public override async Task<Festival?> GetByIdAsync(int id)
        {
            return await _festivals
                .Include(f => f.Artists)
                .Include(f => f.Lineups)
                .Include(f => f.Tickets)
                .FirstOrDefaultAsync(f => f.Id == id);
        }
        public async Task<ICollection<Artist>> GetArtistsByFestivalIdAsync(int festivalId)
        {
            return await _festivals
                .Where(f => f.Id == festivalId)
                .SelectMany(f => f.Artists)
                .ToListAsync();
        }
        public async Task<ICollection<Lineup>> GetLineupsByFestivalIdAsync(int festivalId)
        {
            return await _festivals
                .Where(f => f.Id == festivalId)
                .SelectMany(f => f.Lineups)
                .ToListAsync();
        }
        public async Task<ICollection<Ticket>> GetTicketsByFestivalIdAsync(int festivalId)
        {
            return await _festivals
                .Where(f => f.Id == festivalId)
                .SelectMany(f => f.Tickets)
                .ToListAsync();
        }
        public async Task UpdateFestivalArtistsAsync(int festivalId, ICollection<Artist> artists)
        {
            var festival = await GetByIdAsync(festivalId);
            if (festival != null)
            {
                festival.Artists = artists;
                await UpdateAsync(festival);
            }
        }
        public async Task UpdateFestivalLineupsAsync(int festivalId, ICollection<Lineup> lineups)
        {
            var festival = await GetByIdAsync(festivalId);
            if (festival != null)
            {
                festival.Lineups = lineups;
                await UpdateAsync(festival);
            }


        }
        public async Task AddTicketAsync(int festivalId, Ticket ticket)
        {
            var festival = await GetByIdAsync(festivalId);
            if (festival != null)
            {
                festival.Tickets.Add(ticket);
                await UpdateAsync(festival);
            }
        }
        public async Task<List<Ticket>> GetFestivalTicketsAsync(int festivalId)
        {
            var festival = await _festivals
                .Include(f => f.Tickets)
                .FirstOrDefaultAsync(f => f.Id == festivalId);
            return festival?.Tickets.ToList() ?? new List<Ticket>();
        }
        
    }

}
