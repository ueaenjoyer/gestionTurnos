using System;
using System.Globalization;
using Clinica.Models;
using Clinica.Services;

class Program
{
    static void Main(string[] args)
    {

        var clinicaService = new ClinicaService();
        bool salir = false;

        Console.WriteLine("=== SISTEMA DE GESTIÓN DE TURNOS - CLÍNICA MÉDICA ===\n");

        while (!salir)
        {
            Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
            Console.WriteLine("1. Listar pacientes");
            Console.WriteLine("2. Listar doctores");
            Console.WriteLine("3. Listar todos los turnos");
            Console.WriteLine("4. Buscar turnos por paciente");
            Console.WriteLine("5. Agregar nuevo turno");
            Console.WriteLine("6. Cambiar estado de un turno");
            Console.WriteLine("7. Agregar nuevo paciente");
            Console.WriteLine("0. Salir");
            Console.Write("\nSeleccione una opción: ");

            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    clinicaService.ListarPacientes();
                    break;

                case "2":
                    clinicaService.ListarDoctores();
                    break;

                case "3":
                    clinicaService.ListarTurnos();
                    break;

                case "4":
                    Console.Write("Ingrese CI del paciente: ");
                    var dniBusqueda = Console.ReadLine();
                    clinicaService.BuscarTurnosPorPaciente(dniBusqueda);
                    break;

                case "5":
                    Console.WriteLine("\n--- NUEVO TURNO ---");
                    try
                    {
                        Console.Write("Fecha y hora (dd/MM/yyyy HH:mm): ");
                        var fechaHora = DateTime.ParseExact(Console.ReadLine() ?? string.Empty, "dd/MM/yyyy HH:mm", null);
                        
                        Console.Write("CI del paciente: ");
                        var dni = Console.ReadLine();
                        
                        Console.WriteLine("\nDoctores disponibles:");
                        clinicaService.ListarDoctores();
                        Console.Write("\nID del doctor: ");
                        var matricula = Console.ReadLine();
                        
                        Console.Write("Motivo de la consulta: ");
                        var motivo = Console.ReadLine();
                        
                        clinicaService.AgregarTurno(fechaHora, dni ?? string.Empty, matricula ?? string.Empty, motivo ?? string.Empty);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Formato de fecha inválido. Use dd/MM/yyyy HH:mm");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    break;

                case "6":
                    Console.WriteLine("\n--- CAMBIAR ESTADO DE TURNO ---");
                    Console.Write("ID del turno: ");
                    if (int.TryParse(Console.ReadLine(), out int idTurno))
                    {
                        Console.WriteLine("Estados disponibles: Pendiente, Atendido, Cancelado");
                        Console.Write("Nuevo estado: ");
                        var estado = Console.ReadLine();
                        clinicaService.CambiarEstadoTurno(idTurno, estado ?? string.Empty);
                    }
                    else
                    {
                        Console.WriteLine("ID inválido.");
                    }
                    break;

                case "7":
                    Console.WriteLine("\n--- NUEVO PACIENTE ---");
                    try
                    {
                        Console.Write("CI: ");
                        var dni = Console.ReadLine();
                        
                        Console.Write("Nombre completo: ");
                        var nombre = Console.ReadLine();
                        
                        Console.Write("Teléfono: ");
                        var telefono = Console.ReadLine();
                        
                        Console.Write("Email: ");
                        var email = Console.ReadLine();
                        
                        if (string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(nombre))
                        {
                            Console.WriteLine("CI y Nombre son campos obligatorios.");
                            break;
                        }
                        
                        var paciente = new Paciente(dni, nombre, telefono ?? string.Empty, email ?? string.Empty);
                        clinicaService.AgregarPaciente(paciente);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al agregar paciente: {ex.Message}");
                    }
                    break;

                case "0":
                    salir = true;
                    Console.WriteLine("¡Gracias por usar el sistema de gestión de turnos!");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Por favor, intente nuevamente.");
                    break;
            }

            if (!salir)
            {
                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
