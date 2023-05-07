using Swashbuckle.AspNetCore.Annotations;

namespace app.Models
{
    [SwaggerSchema(ReadOnly = true)]
    public enum MemberShip
    {
        Ofical,
        Resident,
        NoResident
    }
}