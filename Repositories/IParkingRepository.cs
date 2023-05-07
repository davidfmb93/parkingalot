using app.Models;

namespace app.Repositories
{
    public interface IParkingRepository
    {
        // Author Services
        // Task<List<Author>> GetAuthorsAsync(); // GET All Authors
        Task<Vehicle> GetVehicleAsync(String NumberPlate); // GET Single Vehicle
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle); // POST New Vehicle
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle); // PUT Vehicle
        Task<Vehicle> CheckoutVehicleAsync(Vehicle vehicle); // POST Vehicle
        // Task<(bool, string)> DeleteAuthorAsync(Author author); // DELETE Author

        // Book Services
        // Task<List<Book>> GetBooksAsync(); // GET All Books
        // Task<Book> GetBookAsync(Guid id); // Get Single Book
        Task<Time> GetTimeAsync(int VehicleId); // GET Single Time
        Task<Time> AddTimeAsync(Time time); // POST New Time
        Task<Time> UpdateTimeAsync(Time time); // PUT time
        // Task<(bool, string)> DeleteBookAsync(Book book); // DELETE Book
    }
}