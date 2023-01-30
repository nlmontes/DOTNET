using AutoMapper;
using FilmesApi.Data;
using FilmesApi.Dto;
using FilmesApi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{

    //private static List<Filme> filmes = new List<Filme>();
    //private static int id = 0;

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    // public void AdicionaFilme([FromBody] Filme filme)
    public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
    {

        //filme.id = id++;
        //filmes.Add(filme);
        /* Console.WriteLine("id: " + filme.id);
         Console.WriteLine(filme.titulo);
         Console.WriteLine(filme.genero);
         Console.WriteLine(filme.duracao);*/

        Filme filme = _mapper.Map<Filme>(filmeDto);

        _context.filmes.Add(filme);
        _context.SaveChanges();
        //return CreatedAtAction(nameof(RecuperaFilmeId), new { id = filme.id }, filme);
        return CreatedAtAction(nameof(RecuperaFilmeId), new { filme.id }, filme);
    }

    [HttpGet]
    //public List<Filme>  RecuperaFilmes()
    public IEnumerable<ReadFilmeDto> RecuperaFilmes([FromQuery] int skip = 0, [FromQuery] int take = 10)
    {
        //return filmes;
        //return filmes.Skip(skip).Take(take);
        //return _context.filmes.Skip(skip).Take(take);
        return _mapper.Map<List<ReadFilmeDto>>(_context.filmes.Skip(skip).Take(take));

    }

    [HttpGet("{id}")]
    //public Filme? RecuperaFilmeId(int id)
    public IActionResult RecuperaFilmeId(int id)
    {
        //var filme = filmes.FirstOrDefault(filme => filme.id == id);
        var filme = _context.filmes.FirstOrDefault(filme => filme.id == id);
        if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto) 
    {
        var filme = _context.filmes.FirstOrDefault(filme => filme.id == id);
        if (filme == null) return NotFound();
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id}")]
    public IActionResult AtualizaFilmeParcial(int id, 
        JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.filmes.FirstOrDefault(filme => filme.id == id);
        if (filme == null) return NotFound();
        
        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if(!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }
        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    //public Filme? RecuperaFilmeId(int id)
    public IActionResult DeletarFilmeId(int id)
    {
        //var filme = filmes.FirstOrDefault(filme => filme.id == id);
        var filme = _context.filmes.FirstOrDefault(filme => filme.id == id);
        if (filme == null) return NotFound();
        _context.filmes.Remove(filme);
        _context.SaveChanges();
        return NoContent() ;
    }

}
