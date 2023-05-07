using app.Context;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class Vehicle : BaseEntity
    {
        [Key]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public string? NumberPlate { get; set; }

        // One-to-many relation with Member
        [SwaggerSchema(ReadOnly = true)]
        public MemberShip MemberShip { get; set; } = MemberShip.NoResident;

        // One-to-many relationship with Times
        [SwaggerSchema(ReadOnly = true)]
        public List<Time>? Times { get; set; }
    }
}