using Reserva.Application.DTOs;
using Reserva.Application.Services.Interfaces;
using Reserva.Infraestructure.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.Services.Implementations
{
    public class ServiceDashBoard : IServiceDashBoard
    {
        private readonly IRepositoryReservacion _repositoryReservacion;
        private readonly IRepositoryCrucero _repositoryCrucero;
        private readonly IRepositoryHabitacion _repositoryHabitacion;

        public ServiceDashBoard(IRepositoryReservacion repoReserva, IRepositoryCrucero repoCrucero, IRepositoryHabitacion repoHabitacion)
        {
            _repositoryReservacion = repoReserva;
            _repositoryCrucero = repoCrucero;
            _repositoryHabitacion = repoHabitacion;
        }

        public async Task<List<DashboardDTO>> ObtenerDashboardAsync()
        {
            var cruceros = await _repositoryCrucero.ListAsync();
            var reservasTodas = await _repositoryReservacion.ListAsync();
            var habitacionesTodas = await _repositoryHabitacion.ListAsync();

            var resultado = new List<DashboardDTO>();

            foreach (var crucero in cruceros)
            {
                var reservasDelCrucero = reservasTodas
                    .Where(r => r.IdCrucero == crucero.IdCrucero)
                    .ToList();

                var reservasPorFecha = reservasDelCrucero
                    .GroupBy(r => r.IdFechaNavigation.FechaSalida)
                    .ToList();

                foreach (var grupoFecha in reservasPorFecha)
                {
                    var habitacionesDisponibles = new Dictionary<string, int>();

                    var habitacionesDelBarco = habitacionesTodas
                        .Where(h => h.BarcoHabitacion.Any(bh => bh.IdBarco == crucero.IdBarco))
                        .ToList();

                    foreach (var habitacion in habitacionesDelBarco)
                    {
                        var relacionBarcoHabitacion = habitacion.BarcoHabitacion
                            .FirstOrDefault(bh => bh.IdBarco == crucero.IdBarco);

                        int totalHabitaciones = relacionBarcoHabitacion?.CantHabitaciones ?? 0;

                        int habitacionesReservadas = reservasDelCrucero
                            .Where(r => r.IdFechaNavigation.FechaSalida == grupoFecha.Key)
                            .SelectMany(r => r.DetalleReservacion)
                            .Count(dr => dr.IdHabitacion == habitacion.IdHabitacion);

                        int disponibles = totalHabitaciones - habitacionesReservadas;
                        habitacionesDisponibles[habitacion.Nombre] = disponibles >= 0 ? disponibles : 0;
                    }

                    int totalHuespedes = reservasDelCrucero
                        .Where(r => r.IdFechaNavigation.FechaSalida == grupoFecha.Key)
                        .Sum(r => r.Huesped.Count);

                    resultado.Add(new DashboardDTO
                    {
                        NombreCrucero = crucero.Nombre,
                        FechaSalida = grupoFecha.Key,
                        CantidadReservas = grupoFecha.Count(),
                        HabitacionesDisponiblesPorTipo = habitacionesDisponibles,
                        CantidadHuespedes = totalHuespedes
                    });
                }
            }

            return resultado;
        }
    }
}
