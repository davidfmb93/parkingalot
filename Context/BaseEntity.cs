using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace app.Context
{
    public abstract class BaseEntity
    {
        [SwaggerSchema(ReadOnly = true)]
        public DateTime CreatedAt { get; set; }  = DateTime.Now;

        [SwaggerSchema(ReadOnly = true)]
        public DateTime UpdatedAt { get; set; }  = DateTime.Now;
    }
}