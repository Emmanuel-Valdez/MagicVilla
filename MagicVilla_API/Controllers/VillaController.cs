using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VillaController : ControllerBase
	{
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public ActionResult<IEnumerable<VillaDto>> GetVillas()
		{
			return Ok(VillaStore.villasList);
		}
		[HttpGet("id:int", Name = "Getvilla")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public ActionResult<VillaDto> GetVilla(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = VillaStore.villasList.FirstOrDefault(v => v.Id == id);
			if (villa == null) {
				return NotFound();
			}
			return Ok(villa);
		}
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villaDto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (VillaStore.villasList.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
			{
				ModelState.AddModelError("NombreExiste", "La villa ya existe");
				return BadRequest(ModelState);
			}
			if (villaDto == null)
			{
				return BadRequest(villaDto);
			}
			if (villaDto.Id > 0)
			{
				return StatusCode(StatusCodes.Status500InternalServerError);
			}
			villaDto.Id = VillaStore.villasList.OrderByDescending(v => v.Id).FirstOrDefault().Id + 1;
			VillaStore.villasList.Add(villaDto);
			return CreatedAtRoute("GetVilla", new { id = villaDto.Id }, villaDto);

		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public IActionResult DeleteVilla(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var villa = VillaStore.villasList.FirstOrDefault(v => v.Id == id);
			if (villa == null)
			{
				return NotFound();
			}
			VillaStore.villasList.Remove(villa);
			return NoContent();
		}
	}
}
