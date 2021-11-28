using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace lab14.Controllers
{
    public class ProductsController : Controller
    {
        private northwndEntities _contexto;

        public northwndEntities Contexto
        {
            set
            {
                _contexto = value;
            }
            get
            {
                if (_contexto == null)
                    _contexto = new northwndEntities();
                return _contexto;
            }
        }

        public ActionResult Index()
        {
            return View(Contexto.Products.ToList());
        }

        public ActionResult Details(int id)
        {
            var productos = from p in Contexto.Products
                            orderby p.ProductName ascending
                            where p.ProductID == id
                            select p;
            return View(productos.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Products nuevoProducto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Contexto.Products.Add(nuevoProducto);
                    Contexto.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(nuevoProducto);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Products ProductoEditar = Contexto.Products.Find(id);

            if (ProductoEditar == null)
                return HttpNotFound();

            return View(ProductoEditar);
        }

        [HttpPost]
        public ActionResult Edit(Products ProductoEditar)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    Contexto.Entry(ProductoEditar).State = EntityState.Modified;
                    Contexto.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(ProductoEditar);
            }
            catch (Exception e)
            {
                throw e;
                return View();
            }
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Products ProductoEliminar = Contexto.Products.Find(id);

            if (ProductoEliminar == null)
                return HttpNotFound();

            return View(ProductoEliminar);
        }

        [HttpPost]
        public ActionResult Delete(int? id, Products Producto1)
        {
            try
            {
                Products ProductoEliminar = new Products();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                    ProductoEliminar = Contexto.Products.Find(id);

                    if (ProductoEliminar == null)
                        return HttpNotFound();

                    Contexto.Products.Remove(ProductoEliminar);
                    Contexto.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(ProductoEliminar);
            }
            catch
            {
                return View();
            }
        }
    }
}