using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultRepository
  {
    private readonly IDbConnection _db;

    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }
    public Vault Add(Vault vaultData)
    {
      Guid g;
      g = Guid.NewGuid();
      string id = g.ToString();
      Vault vault = new Vault()
      {
        Id = id,
        Name = vaultData.Name,
        Description = vaultData.Description,
      };
      int success = _db.Execute(@"
      INSERT INTO vaults (
          id,
          name,
          description
              ) 
          VALUES (@Id, @Name, @Description)", vault);
      if (success < 1)
      {
        throw new Exception("Vault already Created");
      }
      return new Vault()
      {
        Id = vault.Id,
        Name = vault.Name,
        Description = vault.Description
      };
    }

    public Vault GetById(string id)
    {
      Vault vault = _db.QueryFirstOrDefault<Vault>(@"
      SELECT * FROM vaults WHERE id = @Id
      ", new { Id = id });
      return new Vault()
      {
        Id = vault.Id,
        Name = vault.Name,
        Description = vault.Description
      };
    }

    public Vault UpdateVault(Vault vault)
    {
      var i = _db.Execute(@"
      UPDATE vaults SET
      name = @Name
      description = @Description
      WHERE id = @Id
      ", vault);
      if (i > 0)
      {
        return vault;
      }
      return null;
    }

    public void DeleteVault(Vault vault)
    {
      _db.QuerySingleOrDefault<Vault>(@"
      DELETE FROM vaults WHERE id = @Id
      ", vault);
      System.Console.WriteLine("vault Deleted");
    }
  }
}