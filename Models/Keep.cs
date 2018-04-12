using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace keepr.Models
{
  public class Keep
  {
    [Key]
    [Required]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public string Picture { get; set; }
    public int Saves { get; set; }
    public int Views { get; set; }
    public int Shares { get; set; }
    public int Public { get; set; }

    public string UserId { get; set; }
  }
  public class CreateKeep
  {
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public string Picture { get; set; }
    public int Public { get; set; }

    public string UserId { get; set; }
  }

}