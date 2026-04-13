namespace DispensarioMedicoUnapec.Models.ViewModels
{
    public class ReporteViewModel
    {
        public TipoReporte TipoSeleccionado { get; set; }
        public IEnumerable<dynamic> Resultados { get; set; }
    }
}