﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaAPI.Model;

public class VillaNumber
{
    [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int VillaNo { get; set; }
    [ForeignKey("Villa")]
    public int VillaID { get; set; }
    public Villa Villa { get; set; }
    public string? SpecialDetails { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;   
    public DateTime? UpdateDate { get; set; }=DateTime.Now;
}