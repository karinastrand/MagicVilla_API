using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.Dto;

public class VillaNumberCreateDTO
{
    [Required]
    public int VillaNo { get; set; }
    [Required]
    public int VillaID {get; set;}
    public DateTime Created { get; set;}= DateTime.Now;
    public string SpecialDetails { get; set; }

}
