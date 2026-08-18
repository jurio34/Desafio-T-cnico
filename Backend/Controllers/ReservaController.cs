using Backend.Data;
using Backend.DTOs;
using Backend.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservaController : ControllerBase
{
   private readonly AppDbContext _context;

    public ReservaController(AppDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public  IActionResult GetReservas()
    {
       List<Reserva> reservations = _context.Reservas
        .Include("Sala")
        .OrderBy(r => r.StartTime)
        .ToList();

    // 2. Agrupa por dia (mantendo os objetos Reserva originais)
    var reservasAgrupadas = reservations
        .GroupBy(r => r.StartTime.Date)
        .Select(g => new
        {
            Date = g.Key.ToString("yyyy-MM-dd"),
            Reservations = g.ToList()
        });

    // 3. Busca as salas cadastradas para o dropdown
    var salas = _context.Salas.ToList();

    // Retorna os dados agrupados e a lista de salas
    return Ok(new 
    { 
        reservasAgrupadas = reservasAgrupadas, 
        salas = salas 
    });
    }

    [HttpPost]
    public IActionResult CreateReserva(ReservasDTORequest reservasDTORequest)
    {
        var sala = _context.Salas.Find(reservasDTORequest.SalaId);
        if (sala == null)
    {
        return BadRequest($"A sala com ID {reservasDTORequest.SalaId} não existe.");
    }
        if(reservasDTORequest.SalaId <= 0 || string.IsNullOrEmpty(reservasDTORequest.Titulo))
        {
            return BadRequest("O ID da sala e o título são obrigatórios.");
        }


        if(reservasDTORequest.EndTime <= reservasDTORequest.StartTime)
        {
            return BadRequest("O horário de término deve ser posterior ao horário de início.");
        }

        bool temConflito = _context.Reservas.Any(r =>
        r.SalaId == reservasDTORequest.SalaId &&
        reservasDTORequest.StartTime < r.EndTime &&
        reservasDTORequest.EndTime > r.StartTime
    );
        if(temConflito)
        {
            return Conflict("Já existe uma reserva para esta sala no período especificado.");
        }

        var reserva = new Reserva
        {
            Titulo = reservasDTORequest.Titulo,
            StartTime = reservasDTORequest.StartTime,
            EndTime = reservasDTORequest.EndTime,
            SalaId = reservasDTORequest.SalaId
        };
        _context.Reservas.Add(reserva);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetReservas), new { id = reserva.Id }, reserva);
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteReserva(int id)
    {
        var reserva = _context.Reservas.Find(id);
        if (reserva == null)
        {
            return NotFound();
        }

        _context.Reservas.Remove(reserva);
        _context.SaveChanges();
        return NoContent();
    }
}
