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
    public class FestivalImplement : GenericImplement<Festival>, IFestivalRepo
    {
        private readonly DbSet<Festival> _festivals;

        public FestivalImplement(ShowTimeDBContext context) : base(context)
        {
            _festivals = context.Set<Festival>();
        }
        public async Task<IList<Artist>> GetAllArtistsForFestivalAsync(int festivalId)
        {
                return await _festivals
                    .Where(f => f.Id == festivalId)
                    .SelectMany(f => f.Artists)
                    .ToListAsync();

        }

    }
}
