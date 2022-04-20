using System;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary
{
    public class Vehicle
    {
        [Required]
        public String LicensePlate { get; set; }
        [Required]
        public String Color { get; set; }
        [Required]
        public String Owner { get; set; }
        [Required]
        [Range(-90, 90)]
        public Double Latitude { get; set; }
        [Required]
        [Range(-180, 180)]
        public Double Longitude { get; set; }

    }
}
