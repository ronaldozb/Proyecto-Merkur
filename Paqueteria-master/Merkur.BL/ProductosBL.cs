using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merkur.BL
{
    public class ProductosBL
    {
        public BindingList<Producto> ListadeProductos { get; set; }

        public ProductosBL()
        {
            ListadeProductos = new BindingList<Producto>();

            var Paquete1 = new Producto();
            Paquete1.Id = 1;
            Paquete1.Descripcion = "documentos personales";
            Paquete1.Destino = "chapultepec";
            Paquete1.FechadeEntrega = DateTime.Now;
            Paquete1.Activo = true;

            var Paquete2 = new Producto();
            Paquete2.Id = 2;
            Paquete2.Descripcion = "Artefacto Electronico";
            Paquete2.Destino = "huanajuato";
            Paquete2.FechadeEntrega = DateTime.Now;
            Paquete2.Activo = true;

            var Paquete3 = new Producto();
            Paquete3.Id = 3;
            Paquete3.Descripcion = "Medicina";
            Paquete3.Destino = "queretaro";
            Paquete3.FechadeEntrega = DateTime.Now;
            Paquete3.Activo = true;

            ListadeProductos.Add(Paquete1);
            ListadeProductos.Add(Paquete2);
            ListadeProductos.Add(Paquete3);

        }
        public BindingList<Producto> Obtenerproductos()
        {

            return ListadeProductos;
        }
        public Resultado GuardarProductos(Producto producto)
        {
            var resultado = Validar(producto); 
            if (resultado.Exitoso == false )
            {

                return resultado;
            }

            if (producto.Id == 0)
            {
                producto.Id = ListadeProductos.Max(item => item.Id + 1);
            }
            resultado.Exitoso = true;
            return resultado;
        }
        public void AgregarProducto()
        {
            var nuevoProducto = new Producto();

            ListadeProductos.Add(nuevoProducto);
        }
        public bool EliminarProducto(int id)
        {
            foreach (var producto in ListadeProductos)
            {
                if (producto.Id == id)
                {
                    ListadeProductos.Remove(producto);
                    return true;
                }

            }
            return false;
        }
        private Resultado Validar (Producto producto)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if(producto.Descripcion== " ")
            {
                resultado.Mensaje = "Ingrese una Descripcion";
                resultado.Exitoso = false;
            }

            if (producto.Id<0)
            {
                resultado.Mensaje = "el Id debe ser mayor que 0";
                resultado.Exitoso = false;
            }


            return resultado;
        }
    }
    public class Producto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Destino { get; set; }
        public DateTime FechadeEntrega { get; set; }
        public bool Activo { get; set; }

    }
    public class Resultado
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
    }

}
