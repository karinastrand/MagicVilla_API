﻿using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace MagicVilla_VillaAPI.Model;

public class Villa
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string? Details { get; set; }
    public double Rate { get; set; }
    public int Sqft { get; set; }
    public int Occupancy { get; set; }
    public string ImageUrl { get; set; }
    public string? Amenity { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? UpdatedDate { get; set; } = DateTime.Now;
}
