using app.Models;

namespace app.Responses
{
    public class Checkout
    {
        public long Minutes { get; set; } = 0;
        public Double Payment { get; set; } = 0;
        public MemberShip Membership { get; set; }
        public String? NumberPlate { get; set; }
    }
}