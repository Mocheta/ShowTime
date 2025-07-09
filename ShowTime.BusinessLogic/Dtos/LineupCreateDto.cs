using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Dtos
{
    public class LineupCreateDto
    {
        [Required(ErrorMessage = "Festival ID is required")]
        public int FestivalId { get; set; }

        [Required(ErrorMessage = "Artist ID is required")]
        public int ArtistId { get; set; }

        [Required(ErrorMessage = "Stage is required")]
        [StringLength(100, ErrorMessage = "Stage name cannot exceed 100 characters")]
        public string Stage { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start time is required")]
        public DateTime StartTime { get; set; }
    }
}
