using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class VaultKeepRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepRepository(IDbConnection db)
    {
      _db = db;
    }

    public VaultKeep AddVaultKeep(VaultKeep vaultKeepData)
    {
      Guid g;
      g = Guid.NewGuid();
      string id = g.ToString();
      VaultKeep vaultKeep = new VaultKeep()
      {
        Id = id,
        KeepId = vaultKeepData.KeepId,
        VaultId = vaultKeepData.VaultId,
        UserId = vaultKeepData.UserId
      };
      int success = _db.Execute(@"
      INSERT INTO vaultkeeps (
          id,
          keepId,
          vaultId,
          userId
              )
          VALUES (@Id, @KeepId, @VaultId, @UserId)", vaultKeep);
      if (success < 1)
      {
        throw new Exception("Vault already Created");
      };
      return new VaultKeep()
      {
        Id = vaultKeep.Id,
        KeepId = vaultKeep.KeepId,
        VaultId = vaultKeep.VaultId,
        UserId = vaultKeep.UserId
      };
    }

    public IEnumerable<Keep> GetVaultKeeps(string vaultId)
    {
      return _db.Query<Keep>(@"
        SELECT
          keep.name,
          keep.description,
          keep.picture,
          keep.userId,
          keep.saves,
          keep.views,
          keep.public,
          keep.id
        FROM vaultkeeps vaultKeep
        JOIN keeps keep ON keep.id = vaultKeep.keepId
        WHERE vaultId = @id;
      ", new { id = vaultId });
    }

  }
}