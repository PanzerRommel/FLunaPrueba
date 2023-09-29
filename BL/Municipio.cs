using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaPruebaContext context = new DL.FlunaPruebaContext())
                {
                    var objquery = context.Municipios.FromSqlRaw($"MunicipioGetByIdEstado {IdEstado}").AsEnumerable().ToList();
                    if (objquery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in objquery)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = item.IdMunicipio;
                            municipio.Nombre = item.Nombre;

                            municipio.Estado = new ML.Estado();
                            municipio.Estado.IdEstado = item.IdEstado.Value;

                            result.Objects.Add(municipio);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se puede mostrar registro";
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
    }
}