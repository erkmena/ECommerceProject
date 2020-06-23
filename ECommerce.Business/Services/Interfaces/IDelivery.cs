using ECommerce.Business.Models.DTO;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.Interfaces
{
    public interface IDelivery
    {
        /// <summary>
        /// Calculates the Delivery Cost of Shopping Cart and saves with specific formula and return the cost
        /// </summary>
        /// <param name="cartId">CartId of calculating shopping Cart </param>
        /// <param name="costPerDelivery">Cost of 1 Delivery</param>
        /// <param name="costPerProduct">Cost of 1 Product to Deliver</param>
        /// <param name="fixedCost">Fixed Cost of Delivery</param>
        /// <returns></returns>
        Task<double> CalculateDeliveryCostAndCountAsync(int cartId, double costPerDelivery, double costPerProduct, double fixedCost);
        /// <summary>
        /// Return the cost of calculated delivery of shopping cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        Task<double> GetDeliveryCostAsync(CartDTO cart);
    }
}
