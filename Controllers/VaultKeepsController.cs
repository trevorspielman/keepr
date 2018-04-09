using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using keepr.Repositories;
using keepr.Models;

namespace keepr.Controllers
{
  [Route("api/[controller]")]
  public class VaultKeepsController : Controller
  {
    private readonly VaultKeepRepository _repo;

    public VaultKeepsController(VaultKeepRepository repo)
    {
      _repo = repo;
    }

    [HttpPost]
    public VaultKeep AddVaultKeep([FromBody]VaultKeep vaultKeepData)
    {
      if (ModelState.IsValid)
      {
        VaultKeep vaultKeep = _repo.AddVaultKeep(vaultKeepData);
        return vaultKeep;
      }
      System.Console.WriteLine("VaulKeep Not Added");
      return null;
    }

    [HttpGet("{vaultId}")]
    public IEnumerable<Keep> GetVaultKeeps(string vaultId)
    {
      return _repo.GetVaultKeeps(vaultId);
    }    
  }
}