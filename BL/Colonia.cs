﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    var query = context.Colonia.FromSqlRaw($"ColoniaGetByIdMunicipio").AsEnumerable().ToList();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();

                            colonia.IdColonia = obj.IdColonia;
                            colonia.Nombre = obj.Nombre;
                            colonia.CodigoPostal = obj.CodigoPostal;

                            colonia.Municipio = new ML.Municipio();
                            colonia.Municipio.IdMunicipio = obj.IdMunicipio.Value;
                            result.Objects.Add(colonia);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo completar el registro";
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