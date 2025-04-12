using DinkToPdf;
using DinkToPdf.Contracts;
using Reserva.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reserva.Application.Services.PDF
{
    public class DinkPDFService
    {
        private readonly IConverter _converter;
        public DinkPDFService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GenerarPdf(ReservacionDTO reserva)
        {
            var culture = new CultureInfo("es-CR");

            string html = $@"
                <html>
                <head>
                    <meta charset='UTF-8'>
                    <style>
                        body {{ font-family: Arial; margin: 20px; }}
                        h1 {{ text-align: center; color: #003366; }}
                        table {{ width: 100%; border-collapse: collapse; margin-top: 15px; }}
                        th, td {{ border: 1px solid #ccc; padding: 8px; text-align: left; }}
                        th {{ background-color: #f2f2f2; }}
                        .right {{ text-align: right; }}
                        .total {{ font-weight: bold; background-color: #eef; }}
                        footer {{ text-align: center; margin-top: 40px; font-size: 0.9em; color: gray; }}
                    </style>
                </head>
                <body>
                  <div style='width: 100%; display: flex; align-items: center; justify-content: space-between;'>
                        <img src='https://localhost:7001/images/Logo.png' style='height: 120px;' />
                  </div>

                    <h1 style='text-align: center; margin-top: 10px;'>Resumen de Reserva – Crucero #{reserva.idReservacion}</h1>
                    <p><strong>Fecha de emisión:</strong> {DateTime.Now.ToString("dd/MM/yyyy")}</p>
                    <p><strong>Crucero:</strong> {reserva.NombreCrucero} <br/>
                       <strong>Barco:</strong> {reserva.NombreBarco} <br/>
                       <strong>Itinerario:</strong> {reserva.PuertoSalida} → {reserva.PuertoRegreso} <br/>
                       <strong>Fechas:</strong> {reserva.FechaInicio:dd/MM/yyyy} al {reserva.FechaFin:dd/MM/yyyy}
                    </p>

                    <h3>Habitaciones Reservadas</h3>
                    <table>
                        <thead>
                            <tr>
                                <th>Habitación</th>
                                <th>Huéspedes</th>
                                <th>Precio</th>
                            </tr>
                        </thead>
                        <tbody>";

            foreach (var h in reserva.Habitaciones)
            {
                html += $@"
                    <tr>
                        <td>{h.NombreHabitacion}</td>
                        <td>{h.CantidadHuespedes}</td>
                        <td class='right'>{h.PrecioTotal.ToString("C", culture)}</td>
                    </tr>";
            }

            html += $@"
                        <tr class='total'>
                            <td colspan='2'>Total Habitaciones</td>
                            <td class='right'>{reserva.TotalHabitaciones.ToString("C", culture)}</td>
                        </tr>
                        </tbody>
                    </table>

                    <h3>Complementos Adicionales</h3>
                    <table>
                        <thead>
                            <tr>
                                <th>Complemento</th>
                                <th>Cantidad</th>
                                <th>Total</th>
                            </tr>
                        </thead>
                        <tbody>";

            if (reserva.Complementos.Any())
            {
                foreach (var c in reserva.Complementos)
                {
                    html += $@"
                        <tr>
                            <td>{c.NombreComplemento}</td>
                            <td>{c.Cantidad}</td>
                            <td class='right'>{c.PrecioTotal.ToString("C", culture)}</td>
                        </tr>";
                }
            }
            else
            {
                html += "<tr><td colspan='3'>No se adquirieron complementos.</td></tr>";
            }

            html += $@"
                        <tr class='total'>
                            <td colspan='2'>Total Complementos</td>
                            <td class='right'>{reserva.TotalComplementos.ToString("C", culture)}</td>
                        </tr>
                        </tbody>
                    </table>

                    <h3>Resumen</h3>
                    <table>
                        <tr><td>Subtotal:</td><td class='right'>{reserva.SubTotal.ToString("C", culture)}</td></tr>
                        <tr><td>Impuestos (13%):</td><td class='right'>{reserva.Impuestos.ToString("C", culture)}</td></tr>
                        <tr class='total'><td>Total a Pagar:</td><td class='right'>{reserva.TotalPagar.ToString("C", culture)}</td></tr>
                    </table>

                    <h3>Estado de Pago</h3>
                    <p><strong>Estado:</strong> {reserva.EstadoPago}</p>";

            if (reserva.EstadoPago.ToLower() == "pendiente")
            {
                html += $@"<p><strong>Fecha límite de pago:</strong> {reserva.FechaLimitePago?.ToString("dd/MM/yyyy")} <br/>
                           <strong>Monto pendiente:</strong> {reserva.MontoPendiente?.ToString("C", culture)}</p>";
            }

            html += @"
                    <footer>
                        Contacto: info@thousandsunnycruceros.com | Tel: +506 85072408/62118391  | www.thousandsunnycruceros.com
                    </footer>
                </body>
                </html>";

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = new GlobalSettings
                {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                    Margins = new MarginSettings { Top = 10, Bottom = 10 },
                },
                Objects = {
                    new ObjectSettings
                    {
                        HtmlContent = html,
                        FooterSettings = new FooterSettings
                        {
                            FontSize = 9,
                            Right = "Página [page] de [toPage]",
                            Line = true,
                            Spacing = 5
                        }
                    }
                }
            };

            return _converter.Convert(doc);
        }

    }
}
