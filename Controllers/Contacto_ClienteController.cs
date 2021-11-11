using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConsultaMVCWeb.Models;

namespace ConsultaMVCWeb.Controllers
{
    public class ContactoClienteController : Controller
    {
        private Practica_PatronesEntities2 db = new Practica_PatronesEntities2();

        // GET: Contacto_Cliente
        public ActionResult Index()
        {
            var contacto_Cliente = db.Contacto_Cliente.Include(c => c.Clientes).Include(c => c.Tipo_Contacto);
            return View(contacto_Cliente.ToList());
        }

        // GET: Contacto_Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto_Cliente contacto_Cliente = db.Contacto_Cliente.Find(id);
            if (contacto_Cliente == null)
            {
                return HttpNotFound();
            }
            return View(contacto_Cliente);
        }

        // GET: Contacto_Cliente/Create
        public ActionResult Create(int IdCliente)
        {
            // ViewBag.ID_Tipo = new SelectList(db.Clientes, "ID", "Nombre");
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Contacto, "ID", "Tipo");
            var modelo = new Contacto_Cliente() { ID_Cliente= IdCliente };
            return View(modelo);
        }

        // POST: Contacto_Cliente/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ID_Cliente,ID_Tipo,Valor")] Contacto_Cliente contacto_Cliente)
        {
            if (ModelState.IsValid)
            {
                db.Contacto_Cliente.Add(contacto_Cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Tipo = new SelectList(db.Clientes, "ID", "Nombre", contacto_Cliente.ID_Tipo);
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Contacto, "ID", "Tipo", contacto_Cliente.ID_Tipo);
            return View(contacto_Cliente);
        }

        // GET: Contacto_Cliente/Edit/5
        public ActionResult Edit(int? id, int IdCliente)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto_Cliente contacto_Cliente = db.Contacto_Cliente.Find(id);
            if (contacto_Cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Tipo = new SelectList(db.Clientes, "ID", "Nombre", contacto_Cliente.ID_Tipo);
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Contacto, "ID", "Tipo", contacto_Cliente.ID_Tipo);
            ViewBag.IdCliente = IdCliente;
            return View(contacto_Cliente);
        }

        // POST: Contacto_Cliente/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ID_Cliente,ID_Tipo,Valor")] Contacto_Cliente contacto_Cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contacto_Cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Tipo = new SelectList(db.Clientes, "ID", "Nombre", contacto_Cliente.ID_Tipo);
            ViewBag.ID_Tipo = new SelectList(db.Tipo_Contacto, "ID", "Tipo", contacto_Cliente.ID_Tipo);
            return View(contacto_Cliente);
        }

        // GET: Contacto_Cliente/Delete/5
        public ActionResult Delete(int? id, int IdCliente)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto_Cliente contacto_Cliente = db.Contacto_Cliente.Find(id);
            if (contacto_Cliente == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCliente = IdCliente;
            return View(contacto_Cliente);
        }

        // POST: Contacto_Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contacto_Cliente contacto_Cliente = db.Contacto_Cliente.Find(id);
            db.Contacto_Cliente.Remove(contacto_Cliente);
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
