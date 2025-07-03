using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Dtos
{
    public class FestivalUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SplashArt { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public IList<int> ArtistIds { get; set; } = new List<int>();
    }
}
