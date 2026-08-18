
namespace Backend.Model
{
    public class Reserva
    {
      
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int SalaId { get; set; }

        public Sala ? Sala { get; set; }
    }
}