using System;

namespace Clinica.Models
{
    public class Paciente
    {
        public string CI { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }

        public Paciente(string dni, string nombre, string telefono, string email)
        {
            CI = dni;
            Nombre = nombre;
            Telefono = telefono;
            Email = email;
        }

        public override string ToString()
        {
            return $"Paciente: {Nombre} (CI: {CI})\nTeléfono: {Telefono}\nEmail: {Email}";
        }
    }
}
