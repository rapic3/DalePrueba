using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dale.Domain
{
    /// <summary>
    /// Clase para mantener un formato unico en todo el dominio
    /// </summary>
    /// <author>
    /// Robert Pineda
    /// </author>
    /// <remarks>
    /// 14/09/2022
    /// </remarks>
    public class BaseModel
    {
        public BaseModel()
        {
            DateTimeOffset date = DateTimeOffset.UtcNow;
            CreatedAt = CreatedAt != null ? CreatedAt : date;
            UpdatedAt = UpdatedAt != null ? UpdatedAt : date;
            IsActive = true;
        }

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Fecha Creación")]
        public DateTimeOffset CreatedAt { get; set; }

        [Display(Name = "Fecha Actualización")]
        public DateTimeOffset? UpdatedAt { get; set; }

        [Display(Name = "Activo?")]
        public bool IsActive { get; set; }
    }
}
