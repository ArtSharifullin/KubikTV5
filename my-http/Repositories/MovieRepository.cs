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

public class MovieRepository
{
    private ORMContext<Movie> _movieContext = new(new SqlConnection(AppConfig.GetInstance().ConnectionStrings["DefaultConnection"]));

    public List<Movie> GetMovies()
    {
        return _movieContext.GetAll();
    }

    public Movie GetByName(string name)
    {
        return _movieContext.GetByName(name);
    }

    public void Create(Movie newMovie)
    {
        _movieContext.Create(newMovie);
    }

    public Movie GetById(int id)
    {
        return _movieContext.GetById(id);
    }

    public void Delete(string id)
    {
        _movieContext.Delete(id, "Movies");
    }

    public void UpdateMovie(Movie Movie)
    {
        _movieContext.Update(Movie);
    }


}