using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emart.UserService.Models;

namespace Emart.UserService.Repositories
{
    public interface IUserRepository
    {
        Buyer BuyerSignin(string username, string password);
        Seller SellerSignin(string username, string password);
        void BuyerSignup(Buyer buyer);
        void SellerSignup(Seller seller);

        
    }
}
