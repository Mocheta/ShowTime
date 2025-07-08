using ShowTime.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface ILineupService
    {
        Task AddArtistToFestivalAsync(int festivalId, int artistId, string stage, DateTime startTime);
        Task RemoveArtistFromFestivalAsync(int festivalId, int artistId);
        Task<IList<Artist>> GetFestivalArtistsAsync(int festivalId);
        Task<IList<Festival>> GetArtistFestivalsAsync(int artistId);
        Task<Lineup?> GetLineupAsync(int festivalId, int artistId);
        Task UpdateLineupAsync(int festivalId, int artistId, string stage, DateTime startTime);
    }

}
