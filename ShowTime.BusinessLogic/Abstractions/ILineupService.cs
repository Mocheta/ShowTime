using ShowTime.BusinessLogic.Dtos;
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
        Task AddToLineupAsync(LineupCreateDto lineupCreateDto);
        Task UpdateLineupAsync(LineupCreateDto lineupCreateDto);
        Task<IEnumerable<LineupGetDto>> GetLineupAsync(int festivalId);
        Task RemoveFromLineupAsync(int festivalId, int artistId);
    }

}
