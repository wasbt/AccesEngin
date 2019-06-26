using AccesEnginTest.Extensions;
using DAL;
using Front.Areas.BackOffice.Controllers;
using Front.Core;
using Front.Core.Repositories;
using Front.Models;
using Front.Persistance.Repositories;
using Moq;
using NUnit.Framework;
using PagedList;
using Shared;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AccesEnginTest.BackOfficeTest
{
    [TestFixture]
    public class REF_TypeCheckListTest
    {
        private TypeCheckListsRepository _repository;
        private Mock<DbSet<REF_TypeCheckList>> _mockREF_TypeCheckList;
        private string _userId;

        [SetUp]
        public void SetUp()
        {
            var mockContext = new Mock<IOcpPerformanceDataContext>();
            _mockREF_TypeCheckList = new Mock<DbSet<REF_TypeCheckList>>();
            mockContext.SetupGet(c => c.REF_TypeCheckList).Returns(_mockREF_TypeCheckList.Object);
            _repository = new TypeCheckListsRepository(mockContext.Object);
        }
        /**
                public void Index_GetList_ReturnList()
                {
                  

                    var typeCheckLists = new List<REF_TypeCheckList>
                        {
                            new REF_TypeCheckList { Id = 1, Name = "tmd",CreatedBy = Guid.NewGuid().ToString(),CreatedOn = DateTime.Now},
                            //new REF_TypeCheckList { Id = 1, Name = "SousP",CreatedBy = Guid.NewGuid().ToString(),CreatedOn = CreatedOn(2019, 2, 15)},
                            //new REF_TypeCheckList { Id = 1, Name = "Pemp",CreatedBy = Guid.NewGuid().ToString(),CreatedOn =CreatedOn(20197, 6, 15)},
                        }.AsQueryable();

                    var mockSet = new Mock<DbSet<REF_TypeCheckList>>();
                    mockSet.As<IQueryable<REF_TypeCheckList>>().Setup(m => m.Provider).Returns(typeCheckLists.Provider);
                    mockSet.As<IQueryable<REF_TypeCheckList>>().Setup(m => m.Expression).Returns(typeCheckLists.Expression);
                    mockSet.As<IQueryable<REF_TypeCheckList>>().Setup(m => m.ElementType).Returns(typeCheckLists.ElementType);
                    mockSet.As<IQueryable<REF_TypeCheckList>>().Setup(m => m.GetEnumerator()).Returns(typeCheckLists.GetEnumerator());

                    _context = new Mock<IOcpPerformanceDataContext>();


                    //Set the context of mock object to  the data we created.
                    _context.Setup(c => c.REF_TypeCheckList).Returns(mockSet.Object);

                    //Create instance of WorldRepository by injecting mock DbContext we created
                    var controller = new TypeCheckListsController(_context.Object);


                    Mock<ControllerContext> controllerContextMock = new Mock<ControllerContext>();
                    controllerContextMock.Setup(
                        x => x.HttpContext.User.IsInRole(It.Is<string>(s => s.Equals(ConstsAccesEngin.ROLE_BACKOFFICE)))
                        ).Returns(true);

                    controller.ControllerContext = controllerContextMock.Object;


                    StandardModel<REF_TypeCheckList> StandartypeCheckLists = new StandardModel<REF_TypeCheckList>
                    {
                        resultList = typeCheckLists.ToPagedList(1, 2),
                    };


                    // Act:
                    var results = controller.Index(StandartypeCheckLists) as Task<ActionResult>;
                    results.Wait();
                    var viewresult = results.Result;
                    var data = (StandardModel<REF_TypeCheckList>)((ViewResult)viewresult).Model;

                    //Assert
                    Assert.IsNotNull(viewresult);
                    Assert.AreEqual(1, data.resultList.Count);

                }

            */

        [Test]
        public void Cancel_Gig_ShouldReturnIsCanceledTrue()
        {
            var typeCheckLists = new REF_TypeCheckList
            {
                Id = 1,
                Name = "tmd",
                CreatedBy = Guid.NewGuid().ToString(),
                CreatedOn = DateTime.Now,
                // new REF_TypeCheckList { Id = 1, Name = "tmd",CreatedBy = Guid.NewGuid().ToString(),CreatedOn = DateTime.Now }
                //new REF_TypeCheckList { Id = 1, Name = "SousP",CreatedBy = Guid.NewGuid().ToString(),CreatedOn = CreatedOn(2019, 2, 15)},
                //new REF_TypeCheckList { Id = 1, Name = "Pemp",CreatedBy = Guid.NewGuid().ToString(),CreatedOn =CreatedOn(20197, 6, 15)},
            };

            _mockREF_TypeCheckList.SetSource(new[] { typeCheckLists });

            var data = _repository.GetAllTypeCheckList();

            Assert.AreEqual(1, data.Count());

        }
















        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
        private DateTime CreatedOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
