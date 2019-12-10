﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Merkur.BL
{
    public class ClientesBL
    {
        Contexto _contexto;
        public BindingList<Cliente> listadeClientes { get; set; }

        public ClientesBL()
        {
            _contexto = new Contexto();
            listadeClientes = new BindingList<Cliente>();

        }
        public BindingList<Cliente> ObtenerClientes()
        {
            _contexto.Cliente.Load();
            listadeClientes = _contexto.Cliente.Local.ToBindingList();

            return listadeClientes;
        }
        public BindingList<Cliente> ObtenerClientes(string Buscar)
        {
            var query = _contexto.Cliente.Where(p => p.Nombres.ToLower()
            .Contains(Buscar.ToLower()) == true).ToList();
            var resultado = new BindingList<Cliente>(query);

            return resultado;
        }
        public Resultado3 GuardarClientes(Cliente cliente)
        {
            var resultado2 = Validar(cliente);
            if (resultado2.Exitoso == false)
            {

                return resultado2;
            }

            _contexto.SaveChanges();

            resultado2.Exitoso = true;
            return resultado2;
        }
        public void AgregarClientes()
        {
            var nuevoClientes = new Cliente();

            listadeClientes.Add(nuevoClientes);

        }

        public bool EliminarClientes(int id)
        {
            foreach (var Clientes in listadeClientes)
            {
                if (Clientes.Id == id)
                {
                    listadeClientes.Remove(Clientes);
                    _contexto.SaveChanges();
                    return true;
                }

            }
            return false;
        }
        private Resultado3 Validar(Cliente clientes)
        {
            var resultado3 = new Resultado3();
            resultado3.Exitoso = true;

           

            if (clientes.Nombres == " ")
            {
                resultado3.Mensaje = "Ingrese un Nombre";
                resultado3.Exitoso = false;
            }


            if (clientes.Apellidos == " ")
            {
                resultado3.Mensaje = "Ingrese un Apellido";
                resultado3.Exitoso = false;
            }
           

            if (clientes.Id < 0)
            {
                resultado3.Mensaje = "el Id debe ser mayor que 0";
                resultado3.Exitoso = false;
            }
            if (clientes.Cedula == " " )
            {
                resultado3.Mensaje = "Ingrese un valor de cedula";
                resultado3.Exitoso = false;
            }
            if (clientes.Email == " ")
            {
                resultado3.Mensaje = "Ingrese Un Correo Valido";
                resultado3.Exitoso = false;
            }


            return resultado3;
        }

        public void Actualizar(int id, string cedula, string nombres, string apellidos, string email, string telefono)
        {
            var clienteExistente = _contexto.Cliente.Find(id);
            clienteExistente.Cedula = cedula;
            clienteExistente.Nombres = nombres;
            clienteExistente.Apellidos = apellidos;
            clienteExistente.Email = email;
            clienteExistente.Telefono = telefono;
                         
            _contexto.SaveChanges();
        }
        public List<Cliente> Obtener()
        {
            return _contexto.Cliente.ToList();
        }
    }

    public class Cliente
    {
        public int Id { get; set; }
        public string Cedula { get; set; }
        public string Nombres { get; set; }        
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public byte[] Foto { get; set; }
      
    }

    public class Resultado3
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
    }

}
