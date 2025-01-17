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

public class AdminRepository
{
    private ORMContext<Admin> _adminContext = new(new SqlConnection(AppConfig.GetInstance().ConnectionStrings["DefaultConnection"]));

    public List<Admin> GetAdmins()
    {
        return _adminContext.GetAll();
    }

    public Admin GetAdmin(string login, string password)
    {
        return _adminContext.FirstOrDefault(x => x.Login == login && x.Password == password);
    }


}