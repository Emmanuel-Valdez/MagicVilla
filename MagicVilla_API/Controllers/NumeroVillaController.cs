using AutoMapper;
using MagicVilla_API.Datos;
using MagicVilla_API.Modelos;
using MagicVilla_API.Modelos.Dto;
using MagicVilla_API.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NumeroVillaController : ControllerBase
	{
		private readonly ILogger<NumeroVillaController> _logger;
		private readonly INumeroVillaRepositorio _numeroRepo;
		private readonly IVillaRepositorio _villaRepo;
		private readonly IMapper _mapper;
		protected APIResponse _response;
		public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepositorio villaRepo, INumeroVillaRepositorio numeroRepo,IMapper mapper)
		{
			_logger = logger;
			_villaRepo = villaRepo;
			_numeroRepo = numeroRepo;
			_mapper = mapper;
			_response = new();
		}
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetNumeroVillas()
		{
			try
			{
				_logger.LogInformation("Obtener numeros villas");

				IEnumerable<NumeroVilla> numeroVillaList = await _numeroRepo.ObtenerTodos(incluirPropiedades:"Villa");

				_response.Resultado = _mapper.Map<IEnumerable<NumeroVillaDto>>(numeroVillaList);
				_response.statusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsExitoso = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;
		}
		

		[HttpGet("{id:int}", Name = "GetNumerovilla")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]

		public async Task< ActionResult<APIResponse>> GetNumeroVilla(int id)
		{

			try
			{
				if (id == 0)
				{
					_logger.LogError("Error al traer numero de Villa con id " + id);
					_response.IsExitoso = false;
					_response.statusCode = HttpStatusCode.BadRequest;

					return BadRequest(_response);
				}
				//var villa = VillaStore.villasList.FirstOrDefault(v => v.Id == id);
				var numeroVilla = await _numeroRepo.Obtener(v => v.VillaNo == id, incluirPropiedades:"Villa");
				if (numeroVilla == null)
				{
					_response.statusCode = HttpStatusCode.NotFound;
					_response.IsExitoso = false;
					return NotFound(_response);
				}

				_response.Resultado = _mapper.Map<NumeroVillaDto>(numeroVilla);
				
				_response.statusCode = HttpStatusCode.OK;

				return Ok(_response);
			}
			catch (Exception ex)
			{
				_response.IsExitoso = false;
				_response.ErrorMessages = new List<string>() {ex.ToString()};
			}
			return _response;
			
		}
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]

		public async Task<ActionResult<APIResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto createDto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}
				if (createDto.VillaNo<=0)
				{
                    ModelState.AddModelError("ErrorMessages", "El numero de villa no puede ser menor o igual a 0");
                    return BadRequest(ModelState);
                }
                if (await _numeroRepo.Obtener(v => v.VillaNo == createDto.VillaNo) != null)
				{
					ModelState.AddModelError("ErrorMessages", "El numero de villa ya existe");
					return BadRequest(ModelState);
				}

				if(await _villaRepo.Obtener(v => v.Id == createDto.VillaId) == null)
				{
					ModelState.AddModelError("ErrorMessages", "El Id de la villa no existe");
					return BadRequest(ModelState);
				}
				if (createDto == null)
				{
					return BadRequest(createDto);
				}

				NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);

				modelo.FechaCreacion = DateTime.Now;
				modelo.FechaActualizacion = DateTime.Now;

				await _numeroRepo.Crear(modelo);
				_response.Resultado = modelo;
				_response.statusCode = HttpStatusCode.Created;
				return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNo }, _response);
			}
			catch (Exception ex)
			{
				_response.IsExitoso = false;
				_response.ErrorMessages = new List<string>() { ex.ToString() };
			}
			return _response;

		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> DeleteNumeroVilla(int id)
		{
			try
			{
				if (id == 0)
				{
					_response.IsExitoso = false;
					_response.statusCode = HttpStatusCode.BadRequest;

					return BadRequest(_response);
				}
				var numeroVilla = await _numeroRepo.Obtener(v => v.VillaNo == id);
				if (numeroVilla == null)
				{
					_response.IsExitoso = false;
					_response.statusCode = HttpStatusCode.NotFound;
					return NotFound(_response);
				}
				await _numeroRepo.Remover(numeroVilla);
				_response.statusCode = HttpStatusCode.NoContent;
				return Ok(_response);
			}
			catch (Exception ex)
			{

				_response.IsExitoso = false;
				_response.ErrorMessages = new List<string>() {ex.ToString()};
			}
			return BadRequest(_response);
		}
		[HttpPut("{id:int}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto updateDto)
		{
			if(updateDto==null || id!= updateDto.VillaNo)
			{
				_response.IsExitoso=false;
				_response.statusCode = HttpStatusCode.BadRequest;
				return BadRequest();
			}
			if(await _villaRepo.Obtener(V=>V.Id == updateDto.VillaId) == null)
			{
				ModelState.AddModelError("ErrorMessages", "El Id de la villa no existe");
				return BadRequest(ModelState);
			}

			NumeroVilla modelo = _mapper.Map<NumeroVilla>(updateDto);
			await _numeroRepo.Actualizar(modelo);
			_response.statusCode =HttpStatusCode.NoContent;
			return Ok(_response);
		}

		//[HttpPatch("{id:int}")]
		//[ProducesResponseType(StatusCodes.Status204NoContent)]
		//[ProducesResponseType(StatusCodes.Status400BadRequest)]
		//public async Task< IActionResult> UpdatePartialVilla(int id,JsonPatchDocument<VillaUpdateDto> patchDto)
		//{
		//	if (patchDto == null || id == 0)
		//	{
		//		return BadRequest();
		//	}
		//	//var villa = VillaStore.villasList.FirstOrDefault(v => v.Id == id); no es necesario

		//	var villa = await _villaRepo.Obtener(v=>v.Id == id, tracked:false);
		//	VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);
			
		//	if (villa == null) return BadRequest();

		//	patchDto.ApplyTo(villaDto, ModelState);
		//	if (!ModelState.IsValid)
		//	{
		//		return BadRequest(ModelState);
		//	}
		//	Villa modelo = _mapper.Map<Villa>(villaDto);
		//	await _villaRepo.Actualizar(modelo);
		//	_response.statusCode = HttpStatusCode.NoContent;

		//	return Ok(_response);
		//}
	}
}
