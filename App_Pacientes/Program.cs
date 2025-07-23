
using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace App_Pacientes
{
    /// <summary>
    /// Programa de consola para gestionar un sistema básico de citas médicas en un hospital.
    /// Permite registrar pacientes y asignarles citas según especialidades y doctores disponibles.
    /// </summary>
    /// <author>Jusef Falcón</author>
 
    internal class Program
    {
        /// <summary>
        /// Clase que representa un paciente registrado en el sistema de salud.
        /// </summary>
        public class Paciente
        {
            public string Nombre { get; set; }
            public string NroHistoria { get; set; }
            public string Dni { get; set; }
        }

        /// <summary>
        /// Clase que representa una cita médica asignada a un paciente.
        /// Contiene información sobre especialidad, doctor, horario y el paciente asociado.
        /// </summary>
        public class CitaMedica
        {
            public int Id { get; set; }
            public string Especialidad { get; set; }
            public string Doctor { get; set; }
            public string Horario { get; set; }
            public Paciente Paciente { get; set; }
        }

        /// <summary>
        /// Clase que representa el hospital, con sus especialidades y doctores disponibles por especialidad.
        /// </summary>
        public class Hospital
        {
            public List<string> Especialidades { get; set; } = new List<string> { "Pediatría", "Cardiología", "Dermatología" };

            public Dictionary<string, List<string>> Doctor_Especialidad { get; set; } = new Dictionary<string, List<string>>()
            {
                { "Pediatría", new List<string> { "Dr. Pérez", "Dr. Jiménez" } },
                { "Cardiología", new List<string> { "Dr. Rojas", "Dr. Ramos" } },
                { "Dermatología", new List<string> { "Dra. Torres", "Dra. Vega" } }
            };
        }

        /// <summary>
        /// Punto de entrada principal del programa. Muestra un menú interactivo que permite registrar pacientes y agendar citas médicas.
        /// </summary>
        /// <param name="args">Argumentos de línea de comandos (no utilizados).</param>
        static void Main(string[] args)
        {
            List<Paciente> pacientes = new List<Paciente>();
            List<CitaMedica> citas = new List<CitaMedica>();
            Hospital hospital = new Hospital();

            int citaContador = 1;

            while (true)
            {
                Console.WriteLine("\n=== Sistema de Citas Médicas ===");
                Console.WriteLine("1. Registrar paciente");
                Console.WriteLine("2. Crear cita médica");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarPaciente();
                        break;
                    case "2":
                        CrearCita();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }

            /// <summary>
            /// Registra un nuevo paciente solicitando su nombre, DNI y número de historia clínica.
            /// </summary>
            void RegistrarPaciente()
            {
                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();

                Console.Write("DNI: ");
                string dni = Console.ReadLine();

                Console.Write("N° Historia: ");
                string nroHistoria = Console.ReadLine();

                pacientes.Add(new Paciente { Nombre = nombre, Dni = dni, NroHistoria = nroHistoria });
                Console.WriteLine("Paciente registrado.");
            }

            /// <summary>
            /// Crea una nueva cita médica para un paciente ya registrado, seleccionando especialidad, doctor y horario.
            /// </summary>
            void CrearCita()
            {
                Console.Write("DNI del paciente: ");
                string dni = Console.ReadLine();
                Paciente paciente = null;
                foreach (var p in pacientes)
                {
                    if (p.Dni == dni)
                    {
                        paciente = p;
                        break;
                    }
                }

                if (paciente == null)
                {
                    Console.WriteLine("Paciente no encontrado.");
                    return;
                }

                Console.WriteLine("\nSeleccione especialidad:");
                for (int i = 0; i < hospital.Especialidades.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {hospital.Especialidades[i]}");
                }

                int especialidadContador = int.Parse(Console.ReadLine()) - 1;
                string especialidad = hospital.Especialidades[especialidadContador];

                var doctores = hospital.Doctor_Especialidad[especialidad];

                Console.WriteLine("\nSeleccione doctor:");
                for (int i = 0; i < doctores.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {doctores[i]}");
                }

                int doctorContador = int.Parse(Console.ReadLine()) - 1;
                string doctor = doctores[doctorContador];

                Console.Write("Ingrese horario HH:MM ");
                string horario = Console.ReadLine();

                citas.Add(new CitaMedica
                {
                    Id = citaContador++,
                    Especialidad = especialidad,
                    Doctor = doctor,
                    Horario = horario,
                    Paciente = paciente
                });

                Console.WriteLine("Cita médica registrada.");

                Console.WriteLine("\n=== Detalle de la Cita ===");
                Console.WriteLine($"ID: {citaContador - 1}");
                Console.WriteLine($"Paciente: {paciente.Nombre} (DNI: {paciente.Dni}, Historia: {paciente.NroHistoria})");
                Console.WriteLine($"Especialidad: {especialidad}");
                Console.WriteLine($"Doctor: {doctor}");
                Console.WriteLine($"Horario: {horario}");
            }
        }
    }
}
