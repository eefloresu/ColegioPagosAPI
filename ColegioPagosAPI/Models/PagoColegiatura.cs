using System;

namespace ColegioPagosAPI.Models
{
    public class PagoColegiatura
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public string NombreCliente { get; set; }
        public decimal Matricula { get; set; }
        public decimal Bus { get; set; }
        public decimal Mora { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Estado { get; set; }
    }
}