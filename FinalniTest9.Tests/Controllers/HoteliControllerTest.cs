using System;
using FinalniTest9.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FinalniTest9.Models;
using FinalniTest9.Controllers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Collections.Generic;
using System.Linq;

namespace FinalniTest9.Tests.Controllers
{
    [TestClass]
    public class HoteliControllerTest
    {
        [TestMethod]
        public void GetHoteliWithSameId()
        {
            var mockRepository = new Mock<IHotelRepository>();
            mockRepository.Setup(x => x.GetById(3)).Returns(new Hotel { Id = 3 });
            var controller = new HotelsController(mockRepository.Object);

            IHttpActionResult actionResult = controller.Get(3);
            var contentResult = actionResult as OkNegotiatedContentResult<Hotel>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(3, contentResult.Content.Id);
        }

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            var mockRepository = new Mock<IHotelRepository>();
            var controller = new HotelsController(mockRepository.Object);

            IHttpActionResult actionResult = controller.Put(2, new Hotel { Id = 3, Naziv = "Hotel Park" });

            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
           
        }

        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            List<Hotel> hotels = new List<Hotel>();
            hotels.Add(new Hotel { Id = 1, Naziv = "Hotel Park" });
            hotels.Add(new Hotel { Id = 2, Naziv = "Hotel Putnik" });



            var mockRepository = new Mock<IHotelRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(hotels.AsEnumerable());
            var controller = new HotelsController(mockRepository.Object);

            IEnumerable<Hotel> rezultat = controller.Get();

            Assert.IsNotNull(rezultat);
            Assert.AreEqual(hotels.Count(), rezultat.ToList().Count());
            Assert.AreEqual(hotels.ElementAt(0), rezultat.ElementAt(0));
            Assert.AreEqual(hotels.ElementAt(1), rezultat.ElementAt(1));
        }

        [TestMethod]
        public void GetBySizeReturnsMultipleObjects()
        {
            List<Hotel> hotels = new List<Hotel>();
            hotels.Add(new Hotel { Id = 1, Naziv = "Hotel Park" });
            hotels.Add(new Hotel { Id = 2, Naziv = "Hotel Putnik" });



            var mockRepository = new Mock<IHotelRepository>();
            mockRepository.Setup(x => x.GetBySize(30,70)).Returns(hotels.AsEnumerable());
            var controller = new HotelsController(mockRepository.Object);

            IEnumerable<Hotel> rezultat = controller.GetBySize(30, 70);

            Assert.IsNotNull(rezultat);
            Assert.AreEqual(hotels.Count(), rezultat.ToList().Count());
            Assert.AreEqual(hotels.ElementAt(0), rezultat.ElementAt(0));
            Assert.AreEqual(hotels.ElementAt(1), rezultat.ElementAt(1));
        }



    }
}
