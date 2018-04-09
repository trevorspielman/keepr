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
    public string UserId {get; set;}
  }
  public class CreateVault
  {
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public string UserId {get; set;}
  }

}