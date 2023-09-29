using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario(); // instancia para las propiedades
            ML.Result result = BL.Usuario.GetAll();

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            return View(usuario);
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultRol = BL.Rol.GetAll();
            ML.Result resultPais = BL.Pais.GetAll();

            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            if (IdUsuario == null) // add
            {

                usuario.Rol.Roles = resultRol.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                return View(usuario);
            }
            else//update
            {
                ML.Result result = BL.Usuario.GetById(IdUsuario.Value);

                if (result.Correct)
                {
                    usuario = ((ML.Usuario)result.Object); // Unboxing
                    //Instancias para acceder a la informacion del unboxing
                    ML.Result resultEstado = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    ML.Result resultColonia = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                    //son necesarios para mantener la informacion del unboxing
                    usuario.Rol.Roles = resultRol.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                    return View(usuario);
                }
                else
                {
                    ViewBag.Mensaje = "Se produjo un error" + result.ErrorMessage;
                    return View("Modal");
                }
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario, IFormFile imagenFile)
        {
            ML.Result result = new ML.Result();

            // Verificar si se ha cargado un archivo de imagen
            if (imagenFile != null && imagenFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Copiar los datos binarios de la imagen al MemoryStream
                    imagenFile.CopyTo(memoryStream);
                    usuario.Imagen = memoryStream.ToArray();
                }
            }

            if (usuario.IdUsuario == 0)
            {
                result = BL.Usuario.Add(usuario);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "Registro de manera exitosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrió un problema al agregar el registro";
                }
            }
            else
            {
                result = BL.Usuario.Update(usuario);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "Actualización exitosa";
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrió un problema al actualizar el registro";
                }
                return PartialView("Modal");
            }
            return View(usuario);
        }


        public ActionResult Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            result = BL.Usuario.Delete(usuario);

            if (result.Correct)
            {
                ViewBag.Mensaje = "Registro Eliminado Con Exitoso";
            }
            else
            {
                ViewBag.Mensaje = "Se A Producido Un Error" + result.ErrorMessage;

            }
            return PartialView("Modal");
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using (var memoryStream = new MemoryStream())
            {
                try
                {
                    imagen.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
                catch (Exception ex)
                {
                    // Manejar la excepción adecuadamente (por ejemplo, registrarla o lanzarla nuevamente)
                    // En lugar de retornar null, puedes manejar el error de otra manera según tus necesidades
                    throw new Exception("Error al convertir la imagen a bytes: " + ex.Message, ex);
                }
            }
        }
        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.GetByIdPais(IdPais);

            return Json(result.Objects);
        }

        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.MunicipioGetByIdEstado(IdEstado);
            return Json(result.Objects);
        }

        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);
            return Json(result.Objects);
        }
    }
}

