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
    public class FestivalService : IFestivalService
    {
        private readonly IRepo<Festival> _festivalRepository;
        public FestivalService(IRepo<Festival> festivalRepository)
        {
            _festivalRepository = festivalRepository ?? throw new ArgumentNullException(nameof(festivalRepository));
        }

        public async Task<FestivalGetDto> GetFestivalByIdAsync(int id)
        {
            try
            {
                var festival = await _festivalRepository.GetByIdAsync(id);
                if (festival == null)
                {
                    throw new KeyNotFoundException($"Festival with ID {id} not found.");
                }
                return new FestivalGetDto
                {
                    Id = festival.Id,
                    Name = festival.Name,
                    Location = festival.Location,
                    StartDate = festival.StartDate,
                    EndDate = festival.EndDate,
                    SplashArt = festival.SplashArt,
                    Capacity = festival.capacity
                };
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Error fetching festival by ID: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error fetching festival by ID: {ex.Message}");
                throw new InvalidOperationException("An unexpected error occurred while fetching the festival", ex);
            }
        }
        public async Task<IList<FestivalGetDto>> GetAllFestivalsAsync()
        {
            try
            {
                var festivals = await _festivalRepository.GetAllAsync();
                return festivals.Select(festival => new FestivalGetDto
                {
                    Id = festival.Id,
                    Name = festival.Name,
                    Location = festival.Location,
                    StartDate = festival.StartDate,
                    EndDate = festival.EndDate,
                    SplashArt = festival.SplashArt,
                    Capacity = festival.capacity
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching festivals: {ex.Message}");
                throw new InvalidOperationException("An error occurred while fetching festivals", ex);

            }
        }
        public async Task AddFestivalAsync(FestivalCreateDto festival)
        {
            try
            {
                if (festival == null)
                {
                    throw new ArgumentNullException(nameof(festival));
                }
                var newFestival = new Festival
                {
                    Name = festival.Name,
                    Location = festival.Location,
                    StartDate = festival.StartDate,
                    EndDate = festival.EndDate,
                    SplashArt = festival.SplashArt,
                    capacity = festival.Capacity
                };
                await _festivalRepository.AddAsync(newFestival);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Validation error in Add: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in Add: {ex.Message}");
                throw new InvalidOperationException("An unexpected error occurred while adding the festival", ex);
            }
        }

        public async Task UpdateFestivalAsync(int id, FestivalUpdateDto festival)
        {
            try
            {
                if (festival == null)
                {
                    throw new ArgumentNullException(nameof(festival));
                }
                var existingFestival = await _festivalRepository.GetByIdAsync(id);
                if (existingFestival == null)
                {
                    throw new KeyNotFoundException($"Festival with ID {id} not found.");
                }
                existingFestival.Name = festival.Name;
                existingFestival.Location = festival.Location;
                existingFestival.StartDate = festival.StartDate;
                existingFestival.EndDate = festival.EndDate;
                existingFestival.SplashArt = festival.SplashArt;
                existingFestival.capacity = festival.Capacity;
                await _festivalRepository.UpdateAsync(existingFestival);

            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Validation error in Update: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in Update: {ex.Message}");
                throw new InvalidOperationException("An unexpected error occurred while updating the festival", ex);
            }
        }
        public async Task DeleteFestivalAsync(int id)
        {
            try
            {
                var festival = await _festivalRepository.GetByIdAsync(id);
                if (festival == null)
                {
                    throw new KeyNotFoundException($"Festival with ID {id} not found.");
                }
                await _festivalRepository.DeleteAsync(festival.Id);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Validation error in Delete: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in Delete: {ex.Message}");
                throw new InvalidOperationException("An unexpected error occurred while deleting the festival", ex);
            }
        }

        public async Task<IList<ArtistGetDto>> GetFestivalArtistsAsync(int id)
        {
            try
            {
                var festival = await _festivalRepository.GetByIdAsync(id);
                if (festival == null)
                {
                    throw new KeyNotFoundException($"Festival with ID {id} not found.");
                }
                return festival.Artists.Select(artist => new ArtistGetDto
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Image = artist.Image,
                    Genre = artist.Genre
                }).ToList();
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Error fetching artists for festival ID {id}: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error fetching artists for festival ID {id}: {ex.Message}");
                throw new InvalidOperationException("An unexpected error occurred while fetching the festival artists", ex);
            }
        }
    }
    }
