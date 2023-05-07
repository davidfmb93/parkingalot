using System.ComponentModel.DataAnnotations;
using app.Context;

namespace app.Models
{
    public class Time : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }  = DateTime.Now;
        public DateTime? EndTime { get; set; }

        // One-to-many relation with author
        public int VehicleId { get; set; }

        public Vehicle? Vehicle { get; set; }

    }
}