using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using app.Context;
using app.Models;

namespace app.Repositories
{
    public class ParkingRepository : IParkingRepository
    {
        private readonly AppDbContext _db;

        public ParkingRepository(AppDbContext db)
        {
            _db = db;
        }

        #region Vehicles

        public async Task<Vehicle> GetVehicleAsync(String NumberPlate)
        {
            try
            {
                return await _db.Vehicle.Include(b => b.Times)
                    .FirstOrDefaultAsync(i => i.NumberPlate == NumberPlate);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            try
            {
                var record = await _db.Vehicle.FindAsync(vehicle.Id);
                if(record == null){
                    await _db.Vehicle.AddAsync(vehicle);
                    await _db.SaveChangesAsync();
                }

                Time newTime = new Time();
                newTime.VehicleId = record == null ? vehicle.Id : record.Id;

                var recordTime = await this.AddTimeAsync(newTime);

                return record == null ? await _db.Vehicle.FindAsync(vehicle.Id) : record ; // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                _db.Entry(vehicle).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return vehicle;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Vehicle> CheckoutVehicleAsync(Vehicle Vehicle)
        {
            try
            {
                var time = await this.GetTimeAsync(Vehicle.Id);
                time.EndTime = DateTime.Now;

                _db.Entry(time).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return await _db.Vehicle.FindAsync(Vehicle.Id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion Vehicles

        #region Times

        public async Task<Time> GetTimeAsync(int VehicleId)
        {
            try
            {
                return await _db.Time.Include(b => b.Vehicle)
                    .FirstOrDefaultAsync(i => i.VehicleId == VehicleId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Time> AddTimeAsync(Time time)
        {
            try
            {
                await _db.Time.AddAsync(time);
                await _db.SaveChangesAsync();
                return await _db.Time.FindAsync(time.Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Time> UpdateTimeAsync(Time time)
        {
            try
            {
                _db.Entry(time).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return time;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        #endregion Times
    }
}