using System;

namespace Clinica.Models
{
    public class Turno
    {
        public int Id { get; set; }
        public DateTime FechaHora { get; set; }
        public Paciente Paciente { get; set; }
        public Doctor Doctor { get; set; }
        public string Estado { get; set; } // "Pendiente", "Atendido", "Cancelado"
        public string Motivo { get; set; }

        public Turno(int id, DateTime fechaHora, Paciente paciente, Doctor doctor, string motivo)
        {
            Id = id;
            FechaHora = fechaHora;
            Paciente = paciente;
            Doctor = doctor;
            Estado = "Pendiente";
            Motivo = motivo;
        }

        public override string ToString()
        {
            return $"Turno #{Id}\n" +
                   $"Fecha y Hora: {FechaHora:dd/MM/yyyy HH:mm}\n" +
                   $"Paciente: {Paciente.Nombre}\n" +
                   $"Doctor: {Doctor.Nombre} - {Doctor.Especialidad}\n" +
                   $"Motivo: {Motivo}\n" +
                   $"Estado: {Estado}";
        }
    }
}
