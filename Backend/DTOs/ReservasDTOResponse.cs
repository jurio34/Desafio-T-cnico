using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.DTOs
{
    public record ReservasDTOResponse
    (
        int Id,
        string Titulo,
        DateTime StartTime,
        DateTime EndTime,
        int SalaId,
        string  SalaNome
    );
}