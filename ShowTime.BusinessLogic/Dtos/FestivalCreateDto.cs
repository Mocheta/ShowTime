using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowTime.BusinessLogic.Dtos
{
    public class FestivalCreateDto
    {
        [Required(ErrorMessage = "Festival name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Festival name must be between 2 and 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Location must be between 2 and 200 characters.")]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DateGreaterThan("StartDate", ErrorMessage = "End date must be after start date.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Splash art URL is required.")]
        [StringLength(500, ErrorMessage = "Splash art URL cannot exceed 500 characters.")]
        [Display(Name = "Splash Art URL")]
        public string SplashArt { get; set; } = string.Empty;

        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, 1000000, ErrorMessage = "Capacity must be between 1 and 1,000,000.")]
        public int Capacity { get; set; }
        public IList<int> ArtistIds { get; set; } = new List<int>();

        public class DateGreaterThanAttribute : ValidationAttribute
        {
            private readonly string _comparisonProperty;

            public DateGreaterThanAttribute(string comparisonProperty)
            {
                _comparisonProperty = comparisonProperty;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value == null) return ValidationResult.Success;

                var currentValue = (DateTime)value;
                var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

                if (property == null)
                    throw new ArgumentException("Property with this name not found");

                var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

                if (currentValue <= comparisonValue)
                    return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} must be greater than {_comparisonProperty}");

                return ValidationResult.Success;
            }
        }
    }
}
