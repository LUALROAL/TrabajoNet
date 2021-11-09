using ConsultaMVCWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConsultaMVCWeb.Dto
{
    public class ClienteDto
    {
        public Clientes Cliente { get; set; }
        public List<Contacto_Cliente> DatosContacto { get; set; }
    }
}