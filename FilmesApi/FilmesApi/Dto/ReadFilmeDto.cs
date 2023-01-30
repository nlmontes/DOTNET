using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Dto;

public class ReadFilmeDto
{
    public int id { get; set; }
    public string titulo { get; set; }
    public string genero { get; set; }
    public int duracao { get; set; }
    public DateTime horaConsulta { get;set; } = DateTime.Now;
}
