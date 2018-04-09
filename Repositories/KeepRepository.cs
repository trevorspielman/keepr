using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class KeepRepository
  {
    private readonly IDbConnection _db;

    public KeepRepository(IDbConnection db)
    {
      _db = db;
    }
    public Keep Add(CreateKeep keepData)
    {
      Guid g;
      g = Guid.NewGuid();
      string id = g.ToString();
      Keep keep = new Keep()
      {
        Id = id,
        Name = keepData.Name,
        Description = keepData.Description,
        UserId = keepData.UserId,
        VaultId = keepData.VaultId
      };
      int success = _db.Execute(@"
      INSERT INTO keeps (
          id,
          name,
          description,
          userId,
          vaultId
              )
          VALUES (@Id, @Name, @Description, @UserId, @VaultId)", keep);
      if (success < 1)
      {
        throw new Exception("Keep already Created");
      };
      return new Keep()
      {
        Id = keep.Id,
        Name = keep.Name,
        Description = keep.Description,
        Saves = 0,
        UserId = keep.UserId,
        VaultId = keep.VaultId
      };
    }

    public Keep GetById(string id)
    {
      Keep keep = _db.QueryFirstOrDefault<Keep>(@"
      SELECT * FROM keeps WHERE id = @Id
      ", new { Id = id });
      return new Keep()
      {
        Id = keep.Id,
        Name = keep.Name,
        Description = keep.Description,
        Saves = keep.Saves,
        UserId = keep.UserId,
        VaultId = keep.VaultId
      };
    }
    // public IEnumerable<Keep> GetByUserId(string userId)
    // {
    //   return _db.Query<Keep>(@"
    //   SELECT * FROM keeps WHERE userId = @UserId
    //   ", new { UserId = userId });
    // }
    public IEnumerable<Keep> GetByVaultId(string vaultId)
    {
      return _db.Query<Keep>(@"
      SELECT * FROM keeps WHERE vaultId = @VaultId
      ", new { VaultId = vaultId });
    }

    public Keep UpdateKeep(Keep keep)
    {
      var i = _db.Execute(@"
      UPDATE keeps SET
      name = @Name,
      description = @Description,
      saves = @Saves,
      userId = @UserId,
      vaultId = @VaultId
      WHERE id = @Id
      ", keep);
      if (i > 0)
      {
        return keep;
      }
      return null;
    }

    public void DeleteKeep(Keep keep)
    {
      _db.QuerySingleOrDefault<Keep>(@"
      DELETE FROM keeps WHERE id = @Id
      ", keep);
      System.Console.WriteLine("Keep Deleted");
    }
  }
}