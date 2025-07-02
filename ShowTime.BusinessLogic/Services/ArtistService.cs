using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IRepo<Artist>   _artistRepo;

        public ArtistService(IRepo<Artist> artistRepo)
        {
            _artistRepo = artistRepo ?? throw new ArgumentNullException(nameof(artistRepo));
        }
        public async Task<ArtistGetDto> GetArtistbyIdAsync(int id)
        {
            try
            {
                var artist = await _artistRepo.GetByIdAsync(id);
                if (artist == null)
                {
                    throw new KeyNotFoundException($"Artist with ID {id} not found.");
                }
                return new ArtistGetDto
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Image = artist.Image,
                    Genre = artist.Genre
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching the artist with ID {id}.", ex);
            }
        }
        public async Task<IList<ArtistGetDto>> GetAllArtistsAsync()
        {
            try {
                var artists = await _artistRepo.GetAllAsync();
                return artists.Select(artist => new ArtistGetDto
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Image = artist.Image,
                    Genre = artist.Genre
                }).ToList();
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                throw new Exception("An error occurred while fetching artists.", ex);
            }
        }
        public async Task AddArtistAsync(ArtistCreateDto artistCreateDto)
        {
            try
            {
                var artist = new Artist
                {
                    Name = artistCreateDto.Name,
                    Genre = artistCreateDto.Genre,
                    Image = artistCreateDto.Image
                };
                await _artistRepo.AddAsync(artist);

            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                throw new Exception("An error occurred while creating the artist.", ex);
            }
        }
        public async Task UpdateArtistAsync(int id,ArtistUpdateDto artist)
        {
            try
            {
                var existingArtist = await _artistRepo.GetByIdAsync(id);
                if (existingArtist == null)
                {
                    throw new KeyNotFoundException($"Artist with ID {id} not found.");
                }
                existingArtist.Name = artist.Name;
                existingArtist.Genre = artist.Genre;
                existingArtist.Image = artist.Image;
                await _artistRepo.UpdateAsync(existingArtist);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the artist.", ex);
            }
        }
        public async Task DeleteArtistAsync(int id)
        {
            try 
            {
                var artist = await _artistRepo.GetByIdAsync(id);
                if (artist == null)
                {
                    throw new KeyNotFoundException($"Artist with ID {id} not found.");
                }
                await _artistRepo.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the artist with ID {id}.", ex);
            }
        }
    }
}
