using Basket.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetByUserName(string userName);
        Task<ShoppingCart> Update(ShoppingCart shoppingCart);
        Task DeleteByUserName(string userName);
    }
}
