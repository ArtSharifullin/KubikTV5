using HttpServerLibrary.Models;
using MyHttpServer.Models;
using MyORMLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHttpServer.Repositories;

public class UserRepository
{
    private ORMContext<User> _userContext = new(new SqlConnection(AppConfig.GetInstance().ConnectionStrings["DefaultConnection"]));

    public List<User> GetUsers()
    {
        return _userContext.GetAll();
    }

    public User GetUser(string login, string password)
    {
        return _userContext.FirstOrDefault(x => x.Login == login && x.Password == password);
    }

    public User GetByLogin(string login)
    {
        return _userContext.GetUserByLogin(login);
    }

    public void Create(User newUser)
    {
        _userContext.Create(newUser);
    }

    public User GetById(int id)
    {
        return _userContext.GetById(id);
    }

    public void Delete(string id)
    {
        _userContext.Delete(id, "Users");
    }

    public void UpdateUser(User user)
    {
        _userContext.Update(user);
    }
}