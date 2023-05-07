using app.Context;
using System.ComponentModel.DataAnnotations;

namespace app.Models
{
    public class Time : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }

        // One-to-many relation with Vehicle
        public int VehicleId { get; set; }

        public Vehicle? Vehicle { get; set; }
    }
}