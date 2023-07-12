using System.ComponentModel.DataAnnotations;

namespace MagicVilla_API.Modelos.Dto
{
	public class VillaDto
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(30)]
		public String? Nombre { get; set; }
       
    }
}
