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
  public class VaultsController : Controller
  {
    private readonly VaultRepository _repo;

    public VaultsController(VaultRepository repo)
    {
      _repo = repo;
    }

    [HttpPost]
    public Vault addVault([FromBody]CreateVault vaultData)
    {
      if (ModelState.IsValid)
      {
        Vault vault = _repo.Add(vaultData);
        return vault;
      }
      System.Console.WriteLine("Vault Not Added");
      return null;
    }

    [HttpGet("{id}")]
    public Vault GetById(string id)
    {
      return _repo.GetById(id);
    }

    [HttpGet("user/{userId}")]
    public IEnumerable<Vault> GetByUserId(string userId)
    {
      return _repo.GetByUserId(userId);
    }

    [HttpPut("{id}")]
    public Vault UpdateVault([FromBody]Vault vault)
    {
      if (ModelState.IsValid)
      {
        return _repo.UpdateVault(vault);
      }
      System.Console.WriteLine("Vault Not Updated");
      return null;
    }

    [HttpDelete("{id}")]
    public void DeleteVault(Vault vault)
    {
      _repo.DeleteVault(vault);
    }

  }
}