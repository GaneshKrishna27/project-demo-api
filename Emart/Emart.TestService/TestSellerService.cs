using Emart.SellerService.Models;
using Emart.SellerService.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Emart.TestService
{
    [TestFixture]
    public class TestSellerService
    {
        SellerRepository _repo;
        [SetUp]
        public void SetUp()
        {
            _repo = new SellerRepository(new SellerService.Models.EmartDBContext());
        }
        [Test]
        [Description("Test GetProfile")]
        public void TestGetProfile()
        {
            var result = _repo.Getprofile("S969");
            Assert.IsNotNull(result);
        }
        [Test]
        [Description("Test EditProfile")]
        public void TestEditProfile()
        {
            Seller seller = _repo.Getprofile("S000");
            seller.Username = "Eswar";
            _repo.Editprofile(seller);
            Seller seller1 = _repo.Getprofile("S000");

            Assert.AreSame(seller, seller1);
        }


    }
}
