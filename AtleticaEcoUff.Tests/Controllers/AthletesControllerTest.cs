using System;
using AtleticaEcoUff.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;

using Moq;
using AtleticaEcoUff.Models;
using System.Collections.Generic;
using System.Linq;

namespace AtleticaEcoUff.Tests.Controllers
{
    [TestClass]
    public class AthletesControllerTest
    { 
        // global variables needed for multiple tests in this class
        AthletesController controller;
        Mock<IAthletesMock> mock;
        List<Athlete> athletes;

        [TestInitialize]
        public void TestInitalize()
        {
            // create a new mock data object to hold a fake list of athletes
            mock = new Mock<IAthletesMock>();

            // populate mock list
            athletes = new List<Athlete>
            {
                new Athlete { athlete_id = 82,
                    athlete_name = "Rodrigo",
                    athlete_age = 25,
                    sport_id =2
                },

                new Athlete {
                    athlete_id = 100,
                    athlete_name = "Hanna",
                    athlete_age = 250,
                    sport_id=3
                },

                new Athlete {
                    athlete_id = 81,
                    athlete_name = "ROMULERA",
                    athlete_age = 35,
                    sport_id= 4
                }

            };

            mock.Setup(m => m.Athlete).Returns(athletes.AsQueryable());
            controller = new AthletesController(mock.Object);

        }
        // GET: Athletes/Index
        [TestMethod]
        public void IndexLoadsView()
        {
            // act
            ViewResult result = controller.Index() as ViewResult;

            // assert
            Assert.AreEqual("Index", result.ViewName);

        }

        [TestMethod]

        public void IndexReturnsAthletes()
        {
            // act
            var result = (List<Athlete>)((ViewResult)controller.Index()).Model;

            // assert
            CollectionAssert.AreEqual(athletes, result);
        }

        // GET: Athletes/Details/100
        #region

        [TestMethod]
        public void DetailsWithoutId()
        {
            // act
            ViewResult result = (ViewResult)controller.Details(null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }


        [TestMethod]
        public void DetailsNotValidId()
        {
            // act
            ViewResult result = (ViewResult)controller.Details(104);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]

        public void DetailsValidIdLoadsView()
        {
            // act
            ViewResult result = (ViewResult)controller.Details(100);

            // assert
            Assert.AreEqual("Details", result.ViewName);

        }

        [TestMethod]
        public void DetailsValidIdLoadsAthlete()
        {
            // act
            Athlete result = (Athlete)((ViewResult)controller.Details(100)).Model;

            // assert
            Assert.AreEqual(athletes[1], result);
        }

        #endregion

        //GET: Athletes/Create
        #region
        [TestMethod]
        public void CreateLoadsView()
        {
            //act
            ViewResult result = (ViewResult)controller.Create();

            //assert
            Assert.AreEqual("Create", result.ViewName);
        }

        [TestMethod]
        public void CreateSportViewBagNotNull()
        {
            //act
            var result = ((ViewResult)controller.Create());

            //assert
            Assert.IsNotNull(result.ViewBag.sport_id);
        }

        #endregion

        #region

        //POST:Athletes/Create

        [TestMethod]
        public void AthleteSavedAndRedirected()
        {
            //act
            var result = (RedirectToRouteResult)controller.Create(athletes[0]);

            //assert
            Assert.AreEqual("Index", result.RouteValues["action"]);

        }

        [TestMethod]
        public void SportIdNotNull()
        {
            //act
            controller.ModelState.AddModelError("Random error", "Fake error summary");
            var result = (ViewResult)this.controller.Create(this.athletes[0]);

            //assert
            Assert.IsNotNull(result.ViewBag.sport_id);
        }

        #endregion
        // GET: Athletes/Edit/200     
        #region 
        [TestMethod]
        public void EditWithoutId()
        {
            // arrange
            int? id = null;

            // act 
            ViewResult result = (ViewResult)controller.Edit(id);
            // assert         
            Assert.AreEqual("Error", result.ViewName);        
        }

        [TestMethod]
        public void EditInvalidId()
        {
            // act 
            ViewResult result = (ViewResult)controller.Edit(1234567);

            // assert 
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void EditValidIdLoadsView()
        {
            // act 
            ViewResult result = (ViewResult)controller.Edit(100);

            // assert 
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditValidIdLoadsCorrectAthlete()
        {
            // act 
            Athlete res = (Athlete)((ViewResult)controller.Edit(82)).Model;

            // assert 
            Assert.AreEqual(athletes[0], res);
        }

        [TestMethod]
        public void EditReturnsSportIdViewBag()
        {      
            // act 
            ViewResult result = controller.Edit(100) as ViewResult;

            // assert 
            Assert.IsNotNull(result.ViewBag.sport_id);

        }

        #endregion
        // POST: Athletes/Edit
        #region
        [TestMethod]
        public void ModelValidIndexReturned()
        {
            // act
            RedirectToRouteResult res = (RedirectToRouteResult)controller.Edit(athletes[0]);

            // assert
            Assert.AreEqual("Index", res.RouteValues["action"]);
        }

        [TestMethod]
        public void ModelInvalidViewbagsRightValues ()
        {
            // arrange
            controller.ModelState.AddModelError("Random error", "Fake error summary");

            // Act
            ViewResult result = (ViewResult)controller.Edit(athletes[0]);

            // Assert
            Assert.IsNotNull(result.ViewBag.sport_id);
        }


        [TestMethod]
        public void ModelInValidLoadsView()
        {
            // arrange
            controller.ModelState.AddModelError("Random error", "Fake error summary");

            // act 
            ViewResult result = (ViewResult)controller.Edit(athletes[0]);

            // assert
            Assert.AreEqual("Edit", result.ViewName);

        }

        [TestMethod]
        public void ModelInvalidHasAthletesLoaded()
        {
            // arrange
            controller.ModelState.AddModelError("Random error", "Fake error summary");

            // act 
            Athlete res = (Athlete)((ViewResult)controller.Details(82)).Model;

            // assert
            Assert.AreEqual(athletes[0], res);

        }

        #endregion
        // GET: Athletes/Delete
        #region
        [TestMethod]
        public void DeleteNoId()
        {
            // act
            ViewResult result = (ViewResult)controller.Delete(null);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidId()
        {
            // act
            ViewResult result = (ViewResult)controller.Delete(3869);

            // assert     
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdReturnAthlete()
        {
            // act     
            Athlete result = (Athlete)((ViewResult)controller.Delete(82)).Model;

            // assert
            Assert.AreEqual(athletes[0], result);
        }

        [TestMethod]
        public void DeleteCorrectIdLoadView()
        {
            // act
            ViewResult result = (ViewResult)controller.Delete(100);

            // assert
            Assert.AreEqual("Delete", result.ViewName);
        }

        #endregion

        // POST: Athletes/Delete
        #region

        [TestMethod]
        public void DeleteConfirmedWithoutIdLoadsError()
        {
            // act
            ViewResult result = (ViewResult)controller.DeleteConfirmed(null);

            // assert
            Assert.AreEqual("Error", result.ViewName); 
        }

        [TestMethod]
        public void DeleteConfirmedInvalidId()
        {
            // act
            ViewResult result = (ViewResult)controller.DeleteConfirmed(435652);

            // assert
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void DeleteisSuccessfullyWorking()

        {
            // act
            RedirectToRouteResult result = (RedirectToRouteResult)controller.DeleteConfirmed(82);

            // assert
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        #endregion

    }

}