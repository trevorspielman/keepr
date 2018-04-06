using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

namespace keepr.Repositories
{
  public class UserRepository
  {
    private readonly IDbConnection _db;

    public UserRepository(IDbConnection db)
    {
      _db = db;
    }

    public UserReturnModel Register(UserCreateModel userData)
    {
      Guid g;
      g = Guid.NewGuid();
      string id = g.ToString();
      User user = new User()
      {
        Id = id,
        Username = userData.Username,
        Email = userData.Email,
        Password = BCrypt.Net.BCrypt.HashPassword(userData.Password)
      };
      int success = _db.Execute(@"
      INSERT INTO users (
          id,
          username,
          email,
          password
              ) 
          VALUES (@Id, @Username, @Email, @Password)", user);
      if (success < 1)
      {
        throw new Exception("Email already in use");
      }
      return new UserReturnModel()
      {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email
      };
    }

    public UserReturnModel Login(UserLoginModel userData)
    {
      User user = _db.QueryFirstOrDefault<User>(@"
      SELECT * FROM users WHERE email = @Email
      ", userData);

      Boolean valid = BCrypt.Net.BCrypt.Verify(userData.Password, user.Password);
      if (valid)
      {
        return new UserReturnModel()
        {
          Id = user.Id,
          Username = user.Username,
          Email = user.Email
        };
      }
      throw new Exception("Invalid Credentials");
    }

    public UserReturnModel GetUserById(string id)
    {
      User user = _db.QueryFirstOrDefault<User>(@"
      SELECT * FROM users WHERE id = @Id
      ", new { Id = id });

      if (user == null) { throw new Exception("Oh Boy, something very bad happened. Hacking in session"); }
      return new UserReturnModel()
      {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email
      };
    }

    public UserReturnModel UpdateAccount(UserReturnModel user, UserReturnModel userData)
    {
      var i = _db.Execute(@"
      UPDATE users SET
      email = @Email
      username = @Username
      WHERE id = @Id
      ", userData);
      if (i > 0)
      {
        return user;
      }
      return null;

    }

    internal UserReturnModel GetUserByEmail(string email)
    {
      User user = _db.QueryFirstOrDefault<User>(@"
      SELECT * FROM users WHERE email = @Email
      ", new { Email = email });
      if (user == null) { throw new Exception("Oh Boy, something very bad happened. Hacking in session"); }
      return new UserReturnModel()
      {
        Id = user.Id,
        Username = user.Username,
        Email = user.Email
      };
    }
    public string ChangeUserPassword(ChangeUserPasswordModel user)
    {
      User savedUser = _db.QueryFirstOrDefault<User>(@"
      SELECT * FROM users WHERE id = @Id
      ", user);

      var valid = BCrypt.Net.BCrypt.Verify(user.OldPassword, savedUser.Password);
      if (valid)
      {
        user.NewPassword = BCrypt.Net.BCrypt.HashPassword(user.NewPassword);
        var i = _db.Execute(@"
                    UPDATE users SET
                        password = @NewPassword
                    WHERE id = @id
                ", user);
        return "Good Job";
      }
      return "Umm nope!";
    }

  }
}