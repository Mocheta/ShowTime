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
    public class LineupImplement : GenericImplement<Lineup>, ILineupRepo
    {
        private readonly ShowTimeDBContext _context;
        private readonly DbSet<Lineup> _lineups;

        public LineupImplement(ShowTimeDBContext context) : base(context)
        {
            _context = context;
            _lineups = context.Set<Lineup>();
        }

        public async Task<Lineup?> GetLineupAsync(int festivalId, int artistId)
        {
            return await _lineups.SingleOrDefaultAsync(l => l.FestivalId == festivalId && l.ArtistId == artistId);
        }

        public async Task<IEnumerable<Lineup>> GetByFestivalIdAsync(int festivalId)
        {
            return await _lineups
                .Include(l => l.Artist)
                .Where(l => l.FestivalId == festivalId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Lineup>> GetByArtistIdAsync(int artistId)
        {
            return await _lineups
                .Include(l => l.Festival)
                .Where(l => l.ArtistId == artistId)
                .ToListAsync();
        }

        public async Task AddLineupAsync(Lineup lineup)
        {
            try
            {
                await _lineups.AddAsync(lineup);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to add lineup: {ex.Message}");
            }
        }

        public async Task UpdateLineupAsync(Lineup lineup)
        {
            try
            {
                var existingEntity = await _lineups.FindAsync(lineup.FestivalId, lineup.ArtistId);
                if (existingEntity == null)
                {
                    throw new Exception($"Lineup with festivalId {lineup.FestivalId} and artistId {lineup.ArtistId} not found.");
                }

                existingEntity.Stage = lineup.Stage;
                existingEntity.StartTime = lineup.StartTime;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update lineup: {ex.Message}");
            }
        }

        public  async Task DeleteLineupAsync(int festivalId, int artistId)
        {
            try
            {
                var entity = await _lineups.FindAsync(festivalId, artistId);
                if (entity == null)
                {
                    throw new Exception($"Lineup with festivalId {festivalId} and artistId {artistId} not found.");
                }
                _lineups.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to delete lineup with festivalId {festivalId} and artistId {artistId}: {ex.Message}");
            }
        }

        
    }
    }
