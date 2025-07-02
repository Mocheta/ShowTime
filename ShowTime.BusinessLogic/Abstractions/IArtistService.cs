using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;

namespace ShowTime.BusinessLogic.Abstractions
{
    public interface IArtistService
    {
        Task<ArtistGetDto> GetArtistbyIdAsync(int id);
        Task<IList<ArtistGetDto>> GetAllArtistsAsync();
        Task AddArtistAsync(ArtistCreateDto artist);
        Task UpdateArtistAsync(int id,ArtistUpdateDto artist);
        Task DeleteArtistAsync(int id);

    }
}
