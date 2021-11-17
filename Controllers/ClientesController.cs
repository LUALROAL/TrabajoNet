using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConsultaMVCWeb.Dto;
using ConsultaMVCWeb.Models;

namespace ConsultaMVCWeb.Controllers
{
    public class ClientesController : Controller
    {
        private Practica_PatronesEntities2 db = new Practica_PatronesEntities2();

        // GET: Clientes
        public ActionResult Index(string buscar="")
        {
            if(buscar != "")
            {
                List<Clientes> listaParaPintarF = db.Clientes.Where(c => c.Nombre.Contains(buscar)).ToList();
                return View(listaParaPintarF);
            }
            List<Clientes> listaParaPintar = db.Clientes.OrderBy(c =>c.ID).Take(10).ToList();
            return View(listaParaPintar);

            //return View(db.Clientes.ToList());
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Apellido,Ocupacion")] Clientes clientes)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(clientes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(clientes);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes cliente = db.Clientes.Find(id);
            var datosContacto = db.Contacto_Cliente.Where(cc => cc.ID_Cliente == id)
                .Include(i => i.Tipo_Contacto)
                .ToList();

            ClienteDto modelo = new ClienteDto() { Cliente = cliente, DatosContacto = datosContacto };



            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(modelo);
        }

        // POST: Clientes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( ClienteDto clienteDto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clienteDto.Cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clienteDto);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Clientes clientes = db.Clientes.Find(id);
            if (clientes == null)
            {
                return HttpNotFound();
            }
            return View(clientes);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Clientes clientes = db.Clientes.Find(id);
            db.Clientes.Remove(clientes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
