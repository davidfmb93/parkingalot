using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using app.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Swashbuckle.AspNetCore.Annotations;

namespace app.Models
{
    public class Vehicle : BaseEntity
    {
        [Key]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public string? NumberPlate { get; set; }

        // One-to-many relation with author
        public MemberShip? MemberShip { get; set; }

        // One-to-many relationship with times
        [SwaggerSchema(ReadOnly = true)]
        public List<Time>? Times { get; set; }

    }
}