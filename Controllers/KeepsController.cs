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
  public class KeepsController : Controller
  {
    private readonly KeepRepository _repo;

    public KeepsController(KeepRepository repo)
    {
      _repo = repo;
    }

    [HttpPost]
    public Keep addKeep([FromBody]CreateKeep keepData)
    {
      if (ModelState.IsValid)
      {
        Keep keep = _repo.Add(keepData);
        return keep;
      }
      System.Console.WriteLine("Vault Not Added");
      return null;
    }

    [HttpGet("{id}")]
    public Keep GetById(string id)
    {
      return _repo.GetById(id);
    }

    // [HttpGet("all/{userId}")]
    // public IEnumerable<Keep> GetByUserId(string userId)
    // {
    //   return _repo.GetByUserId(userId);
    // }

    [HttpGet("vault/{vaultId}")]
    public IEnumerable<Keep> GetByVaultId(string vaultId)
    {
      return _repo.GetByVaultId(vaultId);
    }

    [HttpPut("{id}")]
    public Keep UpdateKeep([FromBody]Keep keep)
    {
      if (ModelState.IsValid)
      {
        return _repo.UpdateKeep(keep);
      }
      System.Console.WriteLine("Keep Not Updated");
      return null;
    }

    [HttpDelete("{id}")]
    public void DeleteKeep(Keep keep)
    {
      _repo.DeleteKeep(keep);
    }

  }
}