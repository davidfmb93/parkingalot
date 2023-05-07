using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using app.Context;
using app.Models;
using app.Responses;

namespace app.Repositories
{
    public class ParkingRepository : IParkingRepository
    {
        private Double PRICE_RESIDENT = 0.05;
        private Double PRICE_NO_RESIDENT = 0.5;

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
                var record = await this.GetVehicleAsync(vehicle.NumberPlate);
                if(record == null){
                    await _db.Vehicle.AddAsync(vehicle);
                    await _db.SaveChangesAsync();

                    Time newTime = new Time();
                    newTime.VehicleId = record == null ? vehicle.Id : record.Id;
                    await this.AddTimeAsync(newTime);
                }

                record = await this.GetVehicleAsync(vehicle.NumberPlate);

                if (record.Times.Count == 0 || record.Times.Last().EndTime != null)
                {
                    Time newTime = new Time();
                    newTime.VehicleId = record == null ? vehicle.Id : record.Id;
                    await this.AddTimeAsync(newTime);
                }

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

        public async Task<Checkout> CheckoutVehicleAsync(Vehicle Vehicle)
        {
            try
            {
                var time = await this.GetTimeAsync(Vehicle.Id);
                if(time != null)
                {
                    time.EndTime = time.EndTime != null ? time.EndTime : DateTime.Now;

                    _db.Entry(time).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                }

                Checkout response = new Checkout();

                if(Vehicle.MemberShip == MemberShip.Ofical)
                {
                    response.Minutes = this.CalculateMinutes(time.StartTime, time.EndTime);
                } else if(Vehicle.MemberShip == MemberShip.Resident)
                {
                    response.Minutes = await this.CalculateMinutesMonthly(Vehicle);
                    response.Payment = response.Minutes * PRICE_RESIDENT;
                }
                else if (Vehicle.MemberShip == MemberShip.NoResident)
                {
                    response.Minutes = this.CalculateMinutes(time.StartTime, time.EndTime);
                    response.Payment = response.Minutes * PRICE_NO_RESIDENT;
                }

                response.Membership = Vehicle.MemberShip;
                response.NumberPlate = Vehicle.NumberPlate;

                return response;
                // }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public long CalculateMinutes(DateTime StartTime, DateTime? EndTime)
        {
                if(EndTime == null){
                    return 0;
                }

                long StartTimeParse = ((DateTimeOffset)StartTime).ToUnixTimeSeconds();
                long EndTimeParse = ((DateTimeOffset)EndTime).ToUnixTimeSeconds();

                long minutes =  (EndTimeParse - StartTimeParse) / 60;
                return minutes == 0 ? 1 : minutes;
        }

        public async Task<long> CalculateMinutesMonthly(Vehicle Vehicle)
        {
            var dbTimesVehicle = await _db.Time.Where(x => x.VehicleId == Vehicle.Id)
                .Where(x => x.StartTime.Month == DateTime.Today.Month )
                .ToListAsync();

            long minutes = 0;

            foreach (var ValueTime in dbTimesVehicle)
            {
                minutes += this.CalculateMinutes(ValueTime.StartTime, ValueTime.EndTime);
            }
       
            return minutes;
        }

        public async Task<List<Checkout>> GetPaymentResidentsAsync()
        {
            try
            {
                var dbTimesVehicles = await _db.Time
                                    .Where(x => x.StartTime.Month == DateTime.Today.Month)
                                    .Where(x => x.Vehicle.MemberShip == MemberShip.Resident)
                                    .GroupBy(x => x.VehicleId)
                                    .Select(x => x.First().Vehicle)
                                    .ToListAsync();

                List<Checkout> report = new List<Checkout>();

                foreach (Vehicle Vehicle in dbTimesVehicles)
                {
                    Checkout element = new Checkout();
                    element.NumberPlate = Vehicle.NumberPlate;
                    element.Minutes = await this.CalculateMinutesMonthly(Vehicle);
                    element.Payment = element.Minutes * PRICE_RESIDENT;
                    element.Membership = MemberShip.Resident;
                    report.Add(element);
                }

                return report;
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
                    .Where(x => x.EndTime == null)
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

        public async Task<(bool, string)> DeleteTimesAsync()
        {
            try
            {
                var Times = await _db.Time.ToListAsync();

                foreach (Time Time in Times)
                {
                    _db.Time.Remove(Time);
                    await _db.SaveChangesAsync();
                }

                return (true, "Times has been removed.");
            }
            catch (Exception ex)
            {
                return (false, "Times hasn't been removed.");
            }
        }
        
        #endregion Times
    }
}