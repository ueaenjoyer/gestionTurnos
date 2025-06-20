using System;

namespace Clinica.Models
{
    public class Doctor
    {
        public string Matricula { get; set; }
        public string Nombre { get; set; }
        public string Especialidad { get; set; }

        public Doctor(string matricula, string nombre, string especialidad)
        {
            Matricula = matricula;
            Nombre = nombre;
            Especialidad = especialidad;
        }

        public override string ToString()
        {
            return $"Dr. {Nombre} - {Especialidad} (Matrícula: {Matricula})";
        }
    }
}
