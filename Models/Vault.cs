using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace keepr.Models
{
  public class Vault
  {
    [Key]
    [Required]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
  }
}