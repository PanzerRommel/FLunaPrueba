using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaPruebaContext context = new DL.FlunaPruebaContext())
                {
                    var query = context.Usuarios.FromSqlRaw("UsuarioGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Usuario usuario = new ML.Usuario
                            {
                                IdUsuario = obj.IdUsuario,
                                Nombre = obj.Nombre,
                                ApellidoPaterno = obj.ApellidoPaterno,
                                ApellidoMaterno = obj.ApellidoMaterno,
                                Email = obj.Email,
                                UserName = obj.UserName,
                                Password = obj.Password,
                                FechaNacimiento = obj.FechaNacimiento,
                                Sexo = obj.Sexo,
                                Telefono = obj.Telefono,
                                Celular = obj.Celular,
                                CURP = obj.Curp,
                                Imagen = obj.Imagen,
                                Rol = new ML.Rol
                                {
                                    IdRol = obj.IdRol.Value,
                                    Nombre = obj.RolNombre
                                },
                                Direccion = new ML.Direccion
                                {
                                    IdDireccion = obj.IdDireccion,
                                    Calle = obj.Calle,
                                    NumeroInterior = obj.NumeroInterior,
                                    NumeroExterior = obj.NumeroExterior,
                                    Colonia = new ML.Colonia
                                    {
                                        IdColonia = obj.IdColonia,
                                        Nombre = obj.ColoniaNombre,
                                        Municipio = new ML.Municipio
                                        {
                                            IdMunicipio = obj.IdMunicipio,
                                            Nombre = obj.MunicipioNombre,
                                            Estado = new ML.Estado
                                            {
                                                IdEstado = obj.IdEstado,
                                                Nombre = obj.EstadoNombre,
                                                Pais = new ML.Pais
                                                {
                                                    IdPais = obj.IdPais,
                                                    Nombre = obj.PaisNombre
                                                }
                                            }
                                        }
                                    }
                                }
                            };

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaPruebaContext context = new DL.FlunaPruebaContext())
                {
                    var objquery = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                    if (objquery != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objquery.IdUsuario;
                        usuario.Nombre = objquery.Nombre;
                        usuario.ApellidoPaterno = objquery.ApellidoPaterno;
                        usuario.ApellidoMaterno = objquery.ApellidoMaterno;
                        usuario.Email = objquery.Email;
                        usuario.UserName = objquery.UserName;
                        usuario.Password = objquery.Password;
                        usuario.FechaNacimiento = objquery.FechaNacimiento;
                        usuario.Sexo = objquery.Sexo;
                        usuario.Telefono = objquery.Telefono;
                        usuario.Celular = objquery.Celular;
                        usuario.CURP = objquery.Curp;
                        usuario.Imagen = objquery.Imagen;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = objquery.IdRol.Value;
                        usuario.Rol.Nombre = objquery.RolNombre;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = objquery.IdDireccion;
                        usuario.Direccion.Calle = objquery.Calle;
                        usuario.Direccion.NumeroInterior = objquery.NumeroInterior;
                        usuario.Direccion.NumeroExterior = objquery.NumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = objquery.IdColonia;
                        usuario.Direccion.Colonia.Nombre = objquery.ColoniaNombre;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = objquery.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = objquery.MunicipioNombre;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = objquery.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = objquery.EstadoNombre;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = objquery.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = objquery.PaisNombre;

                        result.Object = usuario;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo completar los registros de la tabla";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaPruebaContext context = new DL.FlunaPruebaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Error al Eliminar Registro";
                    }
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaPruebaContext context = new DL.FlunaPruebaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario} , '{usuario.Nombre}' , '{usuario.ApellidoPaterno}' , '{usuario.ApellidoMaterno}' , '{usuario.Email}' , '{usuario.UserName}' , '{usuario.Password}, '{usuario.FechaNacimiento}' , '{usuario.Sexo}', '{usuario.Telefono}' , '{usuario.Celular}' , '{usuario.CURP}', '{usuario.Imagen}' , {usuario.Rol.IdRol}, '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia} ");
                    if(query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";

                    }
                    result.Correct = true;
                }  
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using(DL.FlunaPruebaContext context = new DL.FlunaPruebaContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}' , '{usuario.ApellidoPaterno}' , '{usuario.ApellidoMaterno}' , '{usuario.Email}' , '{usuario.UserName}' , '{usuario.Password}, '{usuario.FechaNacimiento}' , '{usuario.Sexo}', '{usuario.Telefono}' , '{usuario.Celular}' , '{usuario.CURP}', '{usuario.Imagen}' , {usuario.Rol.IdRol}, '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia} ");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto el registro";

                    }
                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
