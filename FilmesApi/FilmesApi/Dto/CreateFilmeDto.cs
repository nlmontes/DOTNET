using System.ComponentModel.DataAnnotations;

namespace FilmesApi.Dto;

public class CreateFilmeDto
{

    [Required(ErrorMessage = "O titulo é obrigatorio")]
    public string titulo { get; set; }

    [Required(ErrorMessage = "O titulo é obrigatorio")]
    [StringLength(50, ErrorMessage = "Tamanho deve ser menor que 50 caracteres")]
    public string genero { get; set; }

    [Required(ErrorMessage = "O titulo é obrigatorio")]
    [Range(70, 600, ErrorMessage = "Duração deve ser entre 70 e 600 min")]
    public int duracao { get; set; }

}
