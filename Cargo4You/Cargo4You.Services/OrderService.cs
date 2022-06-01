using Cargo4You.Data.Database.Cargo4You.Context;
using Cargo4You.Data.Database.Cargo4You.Model;
using Cargo4You.Services.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo4You.Services
{
    public class OrderService
    {
        private readonly Courier4YouContext _context;

        public OrderService(Courier4YouContext context)
        {
            _context = context;
        }

        public async Task<CourierOfferPrice> GetPrice(PackageDetailData packageDetail)
        {
            

            var package = new Package
            {
                Depth = packageDetail.Depth,
                Weight = packageDetail.Weight,
                Height = packageDetail.Height,
                Width = packageDetail.Width,
                UserId = packageDetail.UserId

            };
            await _context.AddAsync(package);

            var packageDimension = packageDetail.Depth * packageDetail.Height * packageDetail.Width;
            var packageWeight = packageDetail.Weight;

            var couriers = await RetrieveCouriersForPackage(packageDimension, packageWeight);

            var courierOfferPrice = new List<CourierOfferPrice>();
            foreach (var courier in couriers ?? new List<Courier>())
            {
                var prices = await RetrivePrice(courier.Id);

                var priceForWeight = 0.0m;
                var priceForDimension = 0.0m;
                foreach (var price in prices)
                {
                    if (price.IsWeight)
                    {
                        if ((price.From ?? 0) < packageWeight && packageWeight <= price.To)
                        {
                            priceForWeight = price.Price;
                        }
                    }
                    else
                    {
                        if ((price.From ?? 0) < packageDimension && packageDimension <= price.To)
                        {
                            priceForDimension = price.Price;
                        }
                    }
                }

                var courierPrice = priceForWeight > priceForDimension ? priceForWeight : priceForDimension;

                courierOfferPrice.Add(new CourierOfferPrice
                {
                    Price = courierPrice,
                    Courier = new CourierData
                    {
                        Id = courier.Id,
                        Name = courier.Name
                    }
                });
            }

            var cheapestCourier = courierOfferPrice.OrderBy(x => x.Price).FirstOrDefault();

            return cheapestCourier;
        }

        private async Task<List<Courier>> RetrieveCouriersForPackage(decimal packageDimension, decimal packageWeight)
        {
            var courierQuery = from courier in _context.Couriers
                               where
                                        (
                                           (courier.WeightFrom == null || courier.WeightFrom < packageWeight) &&
                                           (courier.WeightTo == null || packageWeight <= courier.WeightTo)
                                        )
                                        &&
                                        (
                                            (courier.DimensionFrom == null || courier.DimensionFrom < packageDimension) &&
                                            (courier.DimensionTo == null || packageDimension <= courier.DimensionTo)
                                        )
                               select courier;

            return await courierQuery.ToListAsync();
        }
        private async Task<List<CourierPrice>> RetrivePrice(int courierId)
        {
            var courierQuery = from courier in _context.Couriers
                               join couriePrices in _context.CourierPrices
                                on courier.Id equals couriePrices.CourierId
                               where courier.Id == courierId

                               select couriePrices;

            return await courierQuery.ToListAsync();
        }
    }
}
