using Cargo4You.Data.Database.Cargo4You.Context;
using Cargo4You.Data.Database.Cargo4You.Model;
using Cargo4You.Services.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cargo4You.Services
{
    public class CourierService
    {
        private readonly Courier4YouContext _context;

        public CourierService(Courier4YouContext context)
        {
            _context = context;
        }

        public async Task<List<CourierData>> GetAllCourier()
        {
            var couriers = await _context.Couriers.ToListAsync();

            var couriersData = couriers.Select(x => new CourierData
            {
                Id = x.Id,
                Name = x.Name,
                DimensionFrom = x.DimensionFrom,
                DimensionTo = x.DimensionTo,
                WeightFrom = x.WeightFrom,
                WeightTo = x.WeightTo
            }).ToList();

            var courierPrices = await _context.CourierPrices.ToListAsync();

            foreach (var courier in couriersData)
            {
                courier.Prices = courierPrices.Where(x => x.CourierId == courier.Id)
                                                        .Select(x => new CourierPriceData
                                                        {
                                                            CourierId = x.CourierId,
                                                            From = x.From,
                                                            To = x.To,
                                                            IsWeight = x.IsWeight,
                                                            Price = x.Price,
                                                        }).ToList();
            }

            return couriersData;
        }

        public async Task<bool> AddCourier(AddCourierData courierData)
        {
            var courier = new Courier
            {
                Name = courierData.Name,
                DimensionFrom = courierData.DimensionFrom,
                DimensionTo = courierData.DimensionTo,
                WeightFrom = courierData.WeightFrom,
                WeightTo = courierData.WeightTo
            };

            await _context.AddAsync(courier);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddCourierPrices(List<CourierPriceData> courierPrices)
        {
            // tx scope

            foreach (var courierPriceData in courierPrices)
            {
                var courierPrice = new CourierPrice
                {
                    CourierId = courierPriceData.CourierId,
                    From = courierPriceData.From,
                    To = courierPriceData.To,
                    IsWeight = courierPriceData.IsWeight,
                    Price = courierPriceData.Price,
                };

                await _context.AddAsync(courierPrice);
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}
