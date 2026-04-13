using DispensarioMedicoUnapec.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DispensarioMedicoUnapec.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Aplica cualquier migración pendiente automáticamente
            context.Database.Migrate();

            // =========================================================================
            // ZONA DE LIMPIEZA (Peligro: Borra todo)
            // Comentado para evitar el borrado automático después de la primera carga.
            // =========================================================================
            /*
            context.Ubicacion_Medicamentos.RemoveRange(context.Ubicacion_Medicamentos);
            context.Estantes.RemoveRange(context.Estantes);
            context.Visitas.RemoveRange(context.Visitas);
            context.Medicamentos.RemoveRange(context.Medicamentos);
            context.Tipo_Farmacos.RemoveRange(context.Tipo_Farmacos);
            context.Marcas.RemoveRange(context.Marcas);
            context.Medicos.RemoveRange(context.Medicos);
            context.Pacientes.RemoveRange(context.Pacientes);
            context.SaveChanges();
            */
            // =========================================================================


            // Si ya hay pacientes (y no activaste la limpieza), asumimos que la base de datos ya tiene datos de prueba y salimos
            if (context.Pacientes.Any())
            {
                return;
            }

            // 1. Crear Marcas
            var marcas = new Marca[]
            {
                new Marca { Nombre = "Bayer", Descripcion = "Laboratorio multinacional líder en Aspirinas y Analgésicos", Pais = "Alemania" },
                new Marca { Nombre = "Pfizer", Descripcion = "Investigación y desarrollo de antibióticos y vacunas", Pais = "Estados Unidos" },
                new Marca { Nombre = "Rowe", Descripcion = "Laboratorios Rowe - Calidad Nacional", Pais = "República Dominicana" },
                new Marca { Nombre = "Sued & Fargesa", Descripcion = "Distribuidor farmacéutico líder", Pais = "República Dominicana" },
                new Marca { Nombre = "Sanofi", Descripcion = "Especialistas en tratamientos crónicos", Pais = "Francia" }
            };
            context.Marcas.AddRange(marcas);
            context.SaveChanges(); // Guardamos marcas para obtener IDs

            // 2. Crear Tipos de Fármacos
            var tiposFarmaco = new Tipo_Farmaco[]
            {
                new Tipo_Farmaco { Nombre = "Pastillas y Tabletas", EstadoFarmaco = EstadoFarmaco.Solido },
                new Tipo_Farmaco { Nombre = "Jarabes y Suspensiones", EstadoFarmaco = EstadoFarmaco.Liquido },
                new Tipo_Farmaco { Nombre = "Cremas y Ungüentos", EstadoFarmaco = EstadoFarmaco.Semisolido },
                new Tipo_Farmaco { Nombre = "Inhaladores y Aerosoles", EstadoFarmaco = EstadoFarmaco.Gaseoso }
            };
            context.Tipo_Farmacos.AddRange(tiposFarmaco);
            context.SaveChanges(); // Guardamos para obtener los IDs

            // 3. Crear Medicamentos con Marcas asignadas
            var medicamentos = new Medicamento[]
            {
                new Medicamento { Nombre = "Acetaminofén", Descripcion = "Analgésico contra la fiebre y dolor leve", Dosis = "500mg", Estado = "Activo", Id_Tipo_Farmaco = tiposFarmaco[0].Id, MarcaId = marcas[0].Id },
                new Medicamento { Nombre = "Amoxicilina", Descripcion = "Antibiótico de amplio espectro", Dosis = "500mg", Estado = "Activo", Id_Tipo_Farmaco = tiposFarmaco[0].Id, MarcaId = marcas[1].Id },
                new Medicamento { Nombre = "Diclofenac Gel", Descripcion = "Antiinflamatorio tópico", Dosis = "1%", Estado = "Activo", Id_Tipo_Farmaco = tiposFarmaco[2].Id, MarcaId = marcas[2].Id },
                new Medicamento { Nombre = "Salbutamol", Descripcion = "Broncodilatador para el asma", Dosis = "100mcg", Estado = "Activo", Id_Tipo_Farmaco = tiposFarmaco[3].Id, MarcaId = marcas[3].Id },
                new Medicamento { Nombre = "Ibuprofeno", Descripcion = "Antiinflamatorio no esteroideo", Dosis = "400mg", Estado = "Activo", Id_Tipo_Farmaco = tiposFarmaco[0].Id, MarcaId = marcas[0].Id },
                new Medicamento { Nombre = "Loratadina Jarabe", Descripcion = "Antihistamínico para alergias", Dosis = "5mg/5ml", Estado = "Activo", Id_Tipo_Farmaco = tiposFarmaco[1].Id, MarcaId = marcas[2].Id },
                new Medicamento { Nombre = "Clotrimazol Crema", Descripcion = "Antimicótico para la piel", Dosis = "1%", Estado = "Activo", Id_Tipo_Farmaco = tiposFarmaco[2].Id, MarcaId = marcas[3].Id },
                new Medicamento { Nombre = "Budesonida Spray", Descripcion = "Corticosteroide nasal", Dosis = "50mcg", Estado = "Activo", Id_Tipo_Farmaco = tiposFarmaco[3].Id, MarcaId = marcas[0].Id },
                new Medicamento { Nombre = "Omeprazol", Descripcion = "Protector gástrico y antiácido", Dosis = "20mg", Estado = "Activo", Id_Tipo_Farmaco = tiposFarmaco[0].Id, MarcaId = marcas[1].Id },
                new Medicamento { Nombre = "Insulina Glargina", Descripcion = "Tratamiento para diabetes avanzada", Dosis = "100 UI/ml", Estado = "Activo", Id_Tipo_Farmaco = tiposFarmaco[1].Id, MarcaId = marcas[4].Id }
            };
            context.Medicamentos.AddRange(medicamentos);
            context.SaveChanges();

            // 4. Crear Médicos
            // ... (keep consistent)
            var medicos = new Medico[]
            {
                new Medico { Nombre = "Carlos", Apellido = "Méndez", Cedula = "00112345678", Especialidad = "Medicina General", FechaNacimiento = DateTime.Parse("1980-05-14"), NumeroCarnet = "MED-001", EstadoMedico = EstadoMedico.A, TandaLaboral = TandaLaboral.Matutina },
                new Medico { Nombre = "Laura", Apellido = "Rosario", Cedula = "40298765432", Especialidad = "Pediatría", FechaNacimiento = DateTime.Parse("1985-08-22"), NumeroCarnet = "MED-002", EstadoMedico = EstadoMedico.A, TandaLaboral = TandaLaboral.Vespertina },
                new Medico { Nombre = "José", Apellido = "Taveras", Cedula = "03145678910", Especialidad = "Dermatología", FechaNacimiento = DateTime.Parse("1975-11-03"), NumeroCarnet = "MED-003", EstadoMedico = EstadoMedico.A, TandaLaboral = TandaLaboral.Matutina },
                new Medico { Nombre = "Ana", Apellido = "Bautista", Cedula = "22365498710", Especialidad = "Psicología Clínica", FechaNacimiento = DateTime.Parse("1990-02-18"), NumeroCarnet = "MED-004", EstadoMedico = EstadoMedico.A, TandaLaboral = TandaLaboral.Nocturna },
                new Medico { Nombre = "Roberto", Apellido = "Guzmán", Cedula = "00188888888", Especialidad = "Ortopedia", FechaNacimiento = DateTime.Parse("1968-12-05"), NumeroCarnet = "MED-005", EstadoMedico = EstadoMedico.A, TandaLaboral = TandaLaboral.Vespertina },
                new Medico { Nombre = "Sofía", Apellido = "Reyes", Cedula = "40277777777", Especialidad = "Ginecología", FechaNacimiento = DateTime.Parse("1982-04-12"), NumeroCarnet = "MED-006", EstadoMedico = EstadoMedico.A, TandaLaboral = TandaLaboral.Matutina },
                new Medico { Nombre = "Miguel", Apellido = "Castillo", Cedula = "03166666666", Especialidad = "Cardiología", FechaNacimiento = DateTime.Parse("1970-09-30"), NumeroCarnet = "MED-007", EstadoMedico = EstadoMedico.A, TandaLaboral = TandaLaboral.Nocturna },
                new Medico { Nombre = "Elena", Apellido = "Vargas", Cedula = "00155555555", Especialidad = "Oftalmología", FechaNacimiento = DateTime.Parse("1992-06-25"), NumeroCarnet = "MED-008", EstadoMedico = EstadoMedico.A, TandaLaboral = TandaLaboral.Vespertina }
            };
            context.Medicos.AddRange(medicos);
            context.SaveChanges();

            // 5. Crear Pacientes (keep consistent)
            // ...
            var pacientes = new List<Paciente>();
            string[] nombres = { "Juan", "María", "Pedro", "Carmen", "Luis", "Rosa", "Esmirna", "Héctor", "Silvia", "Diego", "Julieta", "Andrés", "Valeria", "Ricardo", "Lucía", "Alejandro", "Gabriela", "Fernando", "Isabel", "Marcos", "Camila", "Javier", "Valentina", "Oscar", "Carolina", "Manuel", "Daniela", "Sebastián", "Mariana", "Jorge", "Andrea", "Roberto" };
            string[] apellidos = { "Pérez", "Gómez", "Suárez", "López", "Martínez", "Fernández", "Peralta", "Domínguez", "Ortiz", "Molina", "Jiménez", "Cruz", "Núñez", "Medina", "Castro", "Ruiz", "Soto", "Peña", "Méndez", "Vargas", "Rojas", "Flores", "Acosta", "Cabrera", "Santos", "Arias", "Vega", "Luna", "Gil", "Paredes", "Montes", "Silva" };
            
            for (int i = 0; i < nombres.Length; i++)
            {
                pacientes.Add(new Paciente 
                { 
                    Nombre = nombres[i], 
                    Apellido = apellidos[i], 
                    Cedula = "402" + (10000000 + i).ToString(),
                    FechaNacimiento = DateTime.Now.AddYears(-20 - (i % 30)).AddDays(i),
                    Numero_Carnet = (i % 3 == 0 ? "EST-" : i % 3 == 1 ? "EMP-" : "PROF-") + (2020000 + i).ToString(),
                    Telefono = "809555" + (1000 + i).ToString(),
                    Estado_Paciente = i == 11 || i == 21 || i == 31 ? EstadoPaciente.I : EstadoPaciente.A,
                    Tipo_Paciente = (TipoPaciente)(i % 4)
                });
            }
            context.Pacientes.AddRange(pacientes);
            context.SaveChanges();

            // 6. Crear Visitas
            var visitas = new List<Visita>();
            var random = new Random();
            var motivos = new string[] {
                "Chequeo de rutina", "Dolor de cabeza severo", "Fiebre y malestar general",
                "Revisión de análisis", "Dolor estomacal", "Reacción alérgica leve",
                "Consulta de seguimiento", "Dolor muscular crónico", "Dificultad para respirar"
            };

            foreach (var paciente in pacientes)
            {
                int cantidadVisitas = random.Next(3, 9);
                for (int i = 0; i < cantidadVisitas; i++)
                {
                    var medicoAsignado = medicos[random.Next(medicos.Length)];
                    var fechaVisita = DateTime.Now.AddDays(-random.Next(1, 180)).AddHours(random.Next(-5, 5));
                    visitas.Add(new Visita { PacienteId = paciente.Id, MedicoId = medicoAsignado.Id, Motivo = motivos[random.Next(motivos.Length)], Fecha = fechaVisita, temp = 0 });
                }
            }
            context.Visitas.AddRange(visitas.OrderBy(v => v.Fecha));
            context.SaveChanges();

            // 7. Crear Estantes Especializados
            var estantes = new Estante[]
            {
                new Estante { Nombre = "Estante A - Antibióticos y Antivirales", Descripcion = "Almacena bactericidas, fungicidas y antivirales de uso general." },
                new Estante { Nombre = "Estante B - Analgésicos y Antiinflamatorios", Descripcion = "Medicamentos de venta libre y analgésicos comunes." },
                new Estante { Nombre = "Estante C - Jarabes y Suspensiones", Descripcion = "Diseñado para frascos de vidrio y líquidos." },
                new Estante { Nombre = "Estante R - Cadena de Frío", Descripcion = "Refrigerador para insulinas y medicamentos termolábiles." },
                new Estante { Nombre = "Gabinete S - Medicamentos Controlados", Descripcion = "Área restringida para estupefacientes y psicotrópicos." }
            };
            context.Estantes.AddRange(estantes);
            context.SaveChanges();

            // 8. Asignar Ubicaciones Inteligentes
            var ubicaciones = new List<Ubicacion_Medicamento>
            {
                new Ubicacion_Medicamento { MedicamentoId = medicamentos[0].Id, EstanteId = estantes[1].Id, Tramo = TramoEstante.A, Celda = 1, EstadoMedicamento = EstadoMedicamento.A }, // Acetaminofén -> Estante B
                new Ubicacion_Medicamento { MedicamentoId = medicamentos[1].Id, EstanteId = estantes[0].Id, Tramo = TramoEstante.A, Celda = 1, EstadoMedicamento = EstadoMedicamento.A }, // Amoxicilina -> Estante A
                new Ubicacion_Medicamento { MedicamentoId = medicamentos[2].Id, EstanteId = estantes[1].Id, Tramo = TramoEstante.B, Celda = 1, EstadoMedicamento = EstadoMedicamento.A }, // Diclofenac -> Estante B
                new Ubicacion_Medicamento { MedicamentoId = medicamentos[3].Id, EstanteId = estantes[2].Id, Tramo = TramoEstante.A, Celda = 5, EstadoMedicamento = EstadoMedicamento.A }, // Salbutamol -> Estante C
                new Ubicacion_Medicamento { MedicamentoId = medicamentos[4].Id, EstanteId = estantes[1].Id, Tramo = TramoEstante.A, Celda = 2, EstadoMedicamento = EstadoMedicamento.A }, // Ibuprofeno -> Estante B
                new Ubicacion_Medicamento { MedicamentoId = medicamentos[5].Id, EstanteId = estantes[2].Id, Tramo = TramoEstante.B, Celda = 1, EstadoMedicamento = EstadoMedicamento.A }, // Loratadina -> Estante C
                new Ubicacion_Medicamento { MedicamentoId = medicamentos[6].Id, EstanteId = estantes[0].Id, Tramo = TramoEstante.C, Celda = 2, EstadoMedicamento = EstadoMedicamento.A }, // Clotrimazol -> Estante A
                new Ubicacion_Medicamento { MedicamentoId = medicamentos[7].Id, EstanteId = estantes[2].Id, Tramo = TramoEstante.A, Celda = 10, EstadoMedicamento = EstadoMedicamento.A }, // Budesonida -> Estante C
                new Ubicacion_Medicamento { MedicamentoId = medicamentos[8].Id, EstanteId = estantes[1].Id, Tramo = TramoEstante.E, Celda = 1, EstadoMedicamento = EstadoMedicamento.A }, // Omeprazol -> Estante B
                new Ubicacion_Medicamento { MedicamentoId = medicamentos[9].Id, EstanteId = estantes[3].Id, Tramo = TramoEstante.A, Celda = 1, EstadoMedicamento = EstadoMedicamento.A }  // Insulina -> Estante R
            };
            context.Ubicacion_Medicamentos.AddRange(ubicaciones);
            context.SaveChanges();
        }
    }
}