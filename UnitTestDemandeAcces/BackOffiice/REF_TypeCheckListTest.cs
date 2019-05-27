using DATAAL;
using Front.Areas.BackOffice.Controllers;
using Front.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using X.PagedList;


namespace UnitTestDemandeAcces.BackOffiice
{
    [TestFixture]
    public class REF_TypeCheckListTest
    {
        private TypeCheckListsController typeCheckListsController;
        [SetUp]
        public void SetUp()
        {
            typeCheckListsController = new TypeCheckListsController();
        }

        [Test]
        public void Index_GetList_ReturnList()
        {
            //Arrange
            var model = new StandardModel<REF_TypeCheckList>();
            IList<REF_TypeCheckList> typeCheckLists = new List<REF_TypeCheckList>
                {
                    new REF_TypeCheckList { Id = 1, Name = "tmd",CreatedBy = Guid.NewGuid().ToString(),CreatedOn = DateTime.Now},
                    new REF_TypeCheckList { Id = 1, Name = "SousP",CreatedBy = Guid.NewGuid().ToString(),CreatedOn = DateTime.Now},
                    new REF_TypeCheckList { Id = 1, Name = "Pemp",CreatedBy = Guid.NewGuid().ToString(),CreatedOn = DateTime.Now},
                };

            var mock = new Moq.Mock<TypeCheckListsController>();
           // mock.Setup(m => m.Index(model)).Returns(model);
        }

        [Test]
        public void Detail_GetItem_ReturnList()
        {
            //Arrange
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            context
                .Setup(c => c.Request)
                .Returns(request.Object);
            var model = new StandardModel<REF_TypeCheckList>();

            typeCheckListsController.ControllerContext = new ControllerContext(context.Object, new RouteData(), typeCheckListsController);

            ////Act
            //var result =  obj.Index(model) as Task<ActionResult>;
            //result.Wait();
            //var viewresult = result.Result;
            //var data = (StandardModel<REF_TypeCheckList>)((ViewResult)viewresult).Model;

            ////Assert
            //Assert.IsNotNull(viewresult);
            //Assert.AreEqual(4, data.resultList.Count);
        }


        [Test]
        public void DeleteEmployee_WhenCalled_DeleteTheEmployeeFromDb()
        {
            var storage = new Mock<REF_TypeCheckList>();
            var controller = new TypeCheckListsController();

            var tt = controller.Delete((int)storage.Object.Id) as Task<ActionResult>;

            storage.Verify(s => s.(1));
        }

    }
}
