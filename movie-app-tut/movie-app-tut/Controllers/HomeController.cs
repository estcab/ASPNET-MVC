using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using movie_app_tut.Models;

namespace movie_app_tut.Controllers
{
    public class HomeController : Controller
    {
        // 
        // GET: /Home/

        public ActionResult Index()
        {
            // Creamos un ObjectContext que representa nuestr base de datos
            MoviesDBEntities ctx = new MoviesDBEntities();

            // Recuperamos el EntitySet que contiene todas las peliculas
            // Ejecutamos la consulta explicitamente con el metodo ToList
            IEnumerable<Movie> movies = ctx.Movies.ToList();

            // Generamos la vista por defecto, pasando el  modelo a utilizar
            return View(movies);
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        public ActionResult Create([Bind(Exclude = "Id")]Movie movie)
        {
            // Si los datos no son correctos volvemos a mostrar el  formulario
            // con los datos introducidos
            if (!ModelState.IsValid)
            {
                return View();
            }

            using (MoviesDBEntities ctx = new MoviesDBEntities())
            {
                ctx.AddToMovies(movie);
                ctx.SaveChanges();

            }

            return RedirectToAction("Index");

        }

        //
        // GET: /Home/Edit/5

        public ActionResult Edit(int id)
        {
            // Recuperamos la pelicula que vamos a editar
            using (MoviesDBEntities ctx = new MoviesDBEntities())
            {
                var movie = ctx.Movies.Where(m => m.Id == id).First();
                return View(movie);
            }
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Movie movieEdit )
        {
            using (MoviesDBEntities ctx = new MoviesDBEntities())
            {
                // Recuperamos la pelicula original
                var movieOriginal = ctx.Movies.Where(m => m.Id == movieEdit.Id).First();

                if (!ModelState.IsValid)
                {
                    return View(movieEdit);
                }

                // Aplicamos los cambios
                ctx.ApplyCurrentValues(movieOriginal.EntityKey.EntitySetName, movieEdit);
                ctx.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Home/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
