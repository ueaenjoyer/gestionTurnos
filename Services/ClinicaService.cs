using System;
using System.Collections.Generic;
using System.Linq;
using Clinica.Models;

namespace Clinica.Services
{
    public class ClinicaService
    {
        private List<Paciente> pacientes;
        private List<Doctor> doctores;
        private List<Turno> turnos;
        private int proximoIdTurno;

        public ClinicaService()
        {
            pacientes = new List<Paciente>();
            doctores = new List<Doctor>();
            turnos = new List<Turno>();
            proximoIdTurno = 1;
            
            // Datos de ejemplo
            CargarDatosEjemplo();
        }

        private void CargarDatosEjemplo()
        {
            // Agregar doctores de ejemplo
            doctores.Add(new Doctor("12345", "Juan Pérez", "Cardiología"));
            doctores.Add(new Doctor("67890", "María Gómez", "Pediatría"));
            doctores.Add(new Doctor("54321", "Carlos López", "Traumatología"));

            // Agregar pacientes de ejemplo
            pacientes.Add(new Paciente("40123456", "Ana Torres", "3512345678", "ana@email.com"));
            pacientes.Add(new Paciente("35987654", "Luis Medina", "3518765432", "luis@email.com"));
        }

        // Métodos para gestionar pacientes
        public void AgregarPaciente(Paciente paciente)
        {
            if (!pacientes.Any(p => p.CI == paciente.CI))
            {
                pacientes.Add(paciente);
                Console.WriteLine($"Paciente {paciente.Nombre} registrado exitosamente.");
            }
            else
            {
                Console.WriteLine("Ya existe un paciente con ese CI.");
            }
        }

        public void ListarPacientes()
        {
            Console.WriteLine("\n--- LISTA DE PACIENTES ---");
            foreach (var paciente in pacientes)
            {
                Console.WriteLine($"- {paciente.Nombre} (CI: {paciente.CI})");
            }
        }

        // Métodos para gestionar doctores
        public void ListarDoctores()
        {
            Console.WriteLine("\n--- LISTA DE DOCTORES ---");
            foreach (var doctor in doctores)
            {
                Console.WriteLine($"- {doctor.Nombre} - {doctor.Especialidad} (ID: {doctor.Matricula})");
            }
        }

        // Métodos para gestionar turnos
        public void AgregarTurno(DateTime fechaHora, string dniPaciente, string matriculaDoctor, string motivo)
        {
            var paciente = pacientes.FirstOrDefault(p => p.CI == dniPaciente);
            var doctor = doctores.FirstOrDefault(d => d.Matricula == matriculaDoctor);

            if (paciente == null)
            {
                Console.WriteLine("Paciente no encontrado.");
                return;
            }

            if (doctor == null)
            {
                Console.WriteLine("Doctor no encontrado.");
                return;
            }

            // Verificar disponibilidad del doctor en esa fecha/hora
            bool disponible = !turnos.Any(t => 
                t.Doctor.Matricula == matriculaDoctor && 
                t.FechaHora == fechaHora &&
                t.Estado != "Cancelado");

            if (!disponible)
            {
                Console.WriteLine("El doctor ya tiene un turno asignado en ese horario.");
                return;
            }

            var turno = new Turno(proximoIdTurno++, fechaHora, paciente, doctor, motivo);
            turnos.Add(turno);
            Console.WriteLine($"Turno #{turno.Id} registrado exitosamente para el {fechaHora:dd/MM/yyyy} a las {fechaHora:HH:mm}.");
        }

        public void ListarTurnos()
        {
            Console.WriteLine("\n--- LISTA DE TURNOS ---");
            foreach (var turno in turnos.OrderBy(t => t.FechaHora))
            {
                Console.WriteLine($"\nTurno #{turno.Id}");
                Console.WriteLine($"Fecha: {turno.FechaHora:dd/MM/yyyy HH:mm}");
                Console.WriteLine($"Paciente: {turno.Paciente.Nombre} (CI: {turno.Paciente.CI})");
                Console.WriteLine($"Doctor: {turno.Doctor.Nombre} - {turno.Doctor.Especialidad}");
                Console.WriteLine($"Motivo: {turno.Motivo}");
                Console.WriteLine($"Estado: {turno.Estado}");
            }
        }

        public void BuscarTurnosPorPaciente(string dni)
        {
            var turnosPaciente = turnos.Where(t => t.Paciente.CI == dni).OrderBy(t => t.FechaHora).ToList();
            
            if (!turnosPaciente.Any())
            {
                Console.WriteLine("No se encontraron turnos para este paciente.");
                return;
            }

            Console.WriteLine($"\n--- TURNOS DEL PACIENTE {turnosPaciente.First().Paciente.Nombre} ---");
            foreach (var turno in turnosPaciente)
            {
                Console.WriteLine($"\nTurno #{turno.Id} - {turno.FechaHora:dd/MM/yyyy HH:mm}");
                Console.WriteLine($"Doctor: {turno.Doctor.Nombre} - {turno.Doctor.Especialidad}");
                Console.WriteLine($"Motivo: {turno.Motivo}");
                Console.WriteLine($"Estado: {turno.Estado}");
            }
        }

        public void CambiarEstadoTurno(int idTurno, string nuevoEstado)
        {
            var turno = turnos.FirstOrDefault(t => t.Id == idTurno);
            if (turno != null)
            {
                turno.Estado = nuevoEstado;
                Console.WriteLine($"El turno #{idTurno} ha sido actualizado a: {nuevoEstado}");
            }
            else
            {
                Console.WriteLine("Turno no encontrado.");
            }
        }
    }
}
