using ShowTime.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.DataAccess.Repositories.Abstractions
{
    public interface IFestivalRepo : IRepo<Festival>
    {
        Task<ICollection<Artist>> GetArtistsByFestivalIdAsync(int festivalId);
        Task<ICollection<Lineup>> GetLineupsByFestivalIdAsync(int festivalId);
        Task<ICollection<Ticket>> GetTicketsByFestivalIdAsync(int festivalId);
        Task UpdateFestivalArtistsAsync(int festivalId, ICollection<Artist> artists);
        Task UpdateFestivalLineupsAsync(int festivalId, ICollection<Lineup> lineups);
        Task AddTicketAsync(int festivalId, Ticket ticket);
        Task<List<Ticket>> GetFestivalTicketsAsync(int festivalId);

    }
}
