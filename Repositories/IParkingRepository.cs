using app.Models;
using app.Responses;

namespace app.Repositories
{
    public interface IParkingRepository
    {
        // Vehicle Repository
        Task<Vehicle> GetVehicleAsync(String NumberPlate); // GET Single Vehicle
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle); // POST New Vehicle
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle); // PUT Vehicle
        Task<Checkout> CheckoutVehicleAsync(Vehicle vehicle); // POST Vehicle

        // Time Repository
        Task<Time> GetTimeAsync(int VehicleId); // GET Single Time
        Task<Time> AddTimeAsync(Time time); // POST New Time
        Task<Time> UpdateTimeAsync(Time time); // PUT time

        //Extra Repository
        Task<List<Checkout>> GetPaymentResidentsAsync(); // GET Payment Residents
        Task<(bool, string)> DeleteTimesAsync(); // GET Delete Times

    }
}