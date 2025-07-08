using ShowTime.DataAccess;
using ShowTime.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Abstractions;

namespace ShowTime.BusinessLogic.Services
{
    
    public class LineupService : ILineupService
    {
        private readonly ShowTimeDBContext _context;

        public LineupService(ShowTimeDBContext context)
        {
            _context = context;
        }

        public async Task AddArtistToFestivalAsync(int festivalId, int artistId, string stage, DateTime startTime)
        {
            // Check if the relationship already exists
            var existingLineup = await _context.Lineups
                .FirstOrDefaultAsync(l => l.FestivalId == festivalId && l.ArtistId == artistId);

            if (existingLineup != null)
            {
                throw new InvalidOperationException("Artist is already added to this festival.");
            }

            // Verify festival and artist exist
            var festival = await _context.Festivals.FindAsync(festivalId);
            var artist = await _context.Artists.FindAsync(artistId);

            if (festival == null)
                throw new ArgumentException("Festival not found.");
            if (artist == null)
                throw new ArgumentException("Artist not found.");

            var lineup = new Lineup
            {
                FestivalId = festivalId,
                ArtistId = artistId,
                Stage = stage,
                StartTime = startTime
            };

            _context.Lineups.Add(lineup);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveArtistFromFestivalAsync(int festivalId, int artistId)
        {
            var lineup = await _context.Lineups
                .FirstOrDefaultAsync(l => l.FestivalId == festivalId && l.ArtistId == artistId);

            if (lineup != null)
            {
                _context.Lineups.Remove(lineup);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IList<Artist>> GetFestivalArtistsAsync(int festivalId)
        {
            return await _context.Lineups
                .Where(l => l.FestivalId == festivalId)
                .Include(l => l.Artist)
                .Select(l => l.Artist)
                .ToListAsync();
        }

        public async Task<IList<Festival>> GetArtistFestivalsAsync(int artistId)
        {
            return await _context.Lineups
                .Where(l => l.ArtistId == artistId)
                .Include(l => l.Festival)
                .Select(l => l.Festival)
                .ToListAsync();
        }

        public async Task<Lineup?> GetLineupAsync(int festivalId, int artistId)
        {
            return await _context.Lineups
                .Include(l => l.Festival)
                .Include(l => l.Artist)
                .FirstOrDefaultAsync(l => l.FestivalId == festivalId && l.ArtistId == artistId);
        }

        public async Task UpdateLineupAsync(int festivalId, int artistId, string stage, DateTime startTime)
        {
            var lineup = await _context.Lineups
                .FirstOrDefaultAsync(l => l.FestivalId == festivalId && l.ArtistId == artistId);

            if (lineup == null)
                throw new ArgumentException("Lineup not found.");

            lineup.Stage = stage;
            lineup.StartTime = startTime;

            await _context.SaveChangesAsync();
        }
    }
}