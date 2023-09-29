using DL; // Asegúrate de tener una referencia al proyecto DL
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetByIdPais(int IdPais)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.FlunaPruebaContext context = new DL.FlunaPruebaContext())
                {
                    // Ejecuta el procedimiento almacenado y mapea los resultados a objetos ML.Estado
                    var estados = context.Estados.FromSqlRaw($"EstadoGetByIdPais {IdPais}").ToList();

                    if (estados != null)
                    {
                        result.Objects = estados.Cast<object>().ToList(); // Conversión explícita
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo mostrar el registro";
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
