using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Dtos
{
    public class LineupGetDto
    {
        public int FestivalId { get; set; }
        public int  ArtistId { get; set; } 
        public string Stage { get; set; } = String.Empty;
        public DateTime StartTime { get; set; }
    }
}
