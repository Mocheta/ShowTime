using Microsoft.EntityFrameworkCore;
using ShowTime.BusinessLogic.Abstractions;
using ShowTime.BusinessLogic.Dtos;
using ShowTime.DataAccess;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Services
{

    public class LineupService : ILineupService
    {
        private readonly ILineupRepo _lineupRepository;

        public LineupService(ILineupRepo lineupRepository)
        {
            _lineupRepository = lineupRepository;
        }

        public async Task<IEnumerable<LineupGetDto>> GetLineupAsync(int festivalId)
        {
            try
            {
                var lineup = await _lineupRepository.GetByFestivalIdAsync(festivalId);
                return lineup.Select(l => new LineupGetDto
                {
                    FestivalId = l.FestivalId,
                    ArtistName = l.Artist.Name,
                    Stage = l.Stage,
                    StartTime = l.StartTime
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to retrieve the lineup: {ex.Message}");
            }
        }

        public async Task AddToLineupAsync(LineupCreateDto lineupCreateDto)
        {
            var entity = new Lineup
            {
                FestivalId = lineupCreateDto.FestivalId,
                ArtistId = lineupCreateDto.ArtistId,
                Stage = lineupCreateDto.Stage,
                StartTime = lineupCreateDto.StartTime
            };
            try
            {
                await _lineupRepository.AddLineupAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to add artist to lineup: {ex.Message}");
            }
        }

        public async Task UpdateLineupAsync(LineupCreateDto lineupCreateDto)
        {
            try
            {
                var entity = new Lineup
                {
                    FestivalId = lineupCreateDto.FestivalId,
                    ArtistId = lineupCreateDto.ArtistId,
                    Stage = lineupCreateDto.Stage,
                    StartTime = lineupCreateDto.StartTime
                };

                await _lineupRepository.UpdateLineupAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to update lineup: {ex.Message}");
            }
        }

        public async Task RemoveFromLineupAsync(int festivalId, int artistId)
        {
            try
            {
                await _lineupRepository.DeleteLineupAsync(festivalId, artistId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to remove artist from lineup: {ex.Message}");
            }
        }
    }
    }