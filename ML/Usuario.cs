using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario {  get; set; }
        [Required]
        [DisplayName("Nombre:")]
        [StringLength(50)]
        public string? Nombre {  get; set; }
        public string? ApellidoPaterno {  get; set; }
        public string? ApellidoMaterno {  get; set; }
        public string? Email {  get; set; }
        public string? UserName {  get; set; }
        public string? Password { get; set; }
        public DateTime FechaNacimiento {  get; set; }
        public string? Sexo {  get; set; }
        public string? Telefono {  get; set; }
        public string? Celular {  get; set; }
        public string? CURP { get; set; }
        public byte[]? Imagen {  get; set; }
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }
        public List<object> Roles { get; set; }

        public List<object> Usuarios { get; set; }
    }
}
