namespace Backend.DTOs
{
    public record ReservasDTORequest( 
    string Titulo, 
    DateTime StartTime,
     DateTime EndTime, 
     int SalaId
     );
}