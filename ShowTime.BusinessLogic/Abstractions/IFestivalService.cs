using ShowTime.BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IFestivalService
    {
        Task<FestivalGetDto> GetFestivalByIdAsync(int id);
        Task<IList<FestivalGetDto>> GetAllFestivalsAsync();
        Task AddFestivalAsync(FestivalCreateDto festival);
        Task UpdateFestivalAsync(int id, FestivalUpdateDto festival);
        Task DeleteFestivalAsync(int id);
        Task<IList<ArtistGetDto>> GetFestivalArtistsAsync(int id);
    }
}
