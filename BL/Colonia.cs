using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaPruebaContext context = new DL.FlunaPruebaContext())
                {
                    var objquery = context.Colonia
                        .FromSqlInterpolated($"EXEC ColoniaGetByIdMunicipio {IdMunicipio}")
                        .AsEnumerable()
                        .ToList();

                    if (objquery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in objquery)
                        {
                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = item.IdColonia;
                            colonia.Nombre = item.Nombre;
                            colonia.CodigoPostal = item.CodigoPostal;

                            colonia.Municipio = new ML.Municipio();
                            colonia.Municipio.IdMunicipio = item.IdMunicipio.Value;

                            result.Objects.Add(colonia);
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
