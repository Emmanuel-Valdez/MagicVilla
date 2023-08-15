using Microsoft.EntityFrameworkCore.Metadata;

namespace MagicVilla_API.Modelos.Especificaciones
{
    public class MetaData
    {
        public int TotalPages { get; set; } // totalidad de paginas
        public int PageSize { get; set; } // registros por pagina
        public int TotalCount { get; set; } //Totalidad de registros
    }
}
