using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public Usuario()
    {
        Direccions = new HashSet<Direccion>();
    }
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Sexo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Celular { get; set; }

    public string? Curp { get; set; }

    public byte[]? Imagen { get; set; }

    public int? IdRol { get; set; }
    //Rol
    public string RolNombre { get; set; }
    //Direccion
    public int IdDireccion { get; set; }
    public string Calle { get; set; }
    public string NumeroExterior { get; set; }
    public string NumeroInterior { get; set; }
    //Colonia
    public int IdColonia { get; set; }
    public string ColoniaNombre { get; set; }
    //Municipio 
    public int IdMunicipio { get; set; }
    public string MunicipioNombre { get; set; }
    //Estado
    public int IdEstado { get; set; }
    public string EstadoNombre { get; set; }
    //Pais
    public int IdPais { get; set; }
    public string PaisNombre { get; set; }
    public virtual ICollection<Direccion> Direccions { get; set; } = new List<Direccion>();

    public virtual Rol? IdRolNavigation { get; set; }
}
