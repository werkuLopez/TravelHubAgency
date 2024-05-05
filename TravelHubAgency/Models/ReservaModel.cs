namespace TravelHubAgency.Models
{
    public class ReservaModel
    {
        public List<ReservaDestino> ReservaDestinos { get; set; }
        public List<ReservaPaquete> ReservaPaquetes { get; set; }
        public List<ReservaVuelo> ReservaVuelos { get; set; }
        public List<ReservaHotel> ReservaHoteles { get; set; }
    }
}
