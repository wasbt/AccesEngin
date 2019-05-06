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
        [Test]
        public void Index_GetList_ReturnList()
        {
            //Arrange
            var model = new StandardModel<REF_TypeCheckList>();
            TypeCheckListsController obj = new TypeCheckListsController();

            var foo = new Mock<TypeCheckListsController> ();
            var bar = new Mock<StandardModel<REF_TypeCheckList>> ();
           // foo.Setup(mk => mk.Index(model)).Returns();

            //Act
            var result =  obj.Index(model) as Task<ActionResult>;
            result.Wait();
            var viewresult = result.Result;
            var data = (StandardModel<REF_TypeCheckList>)((ViewResult)viewresult).Model;

            //Assert
            Assert.IsNotNull(viewresult);
            Assert.AreEqual(2, data.resultList.Count);
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
            TypeCheckListsController obj = new TypeCheckListsController();

            obj.ControllerContext = new ControllerContext(context.Object, new RouteData(), obj);

            ////Act
            //var result =  obj.Index(model) as Task<ActionResult>;
            //result.Wait();
            //var viewresult = result.Result;
            //var data = (StandardModel<REF_TypeCheckList>)((ViewResult)viewresult).Model;

            ////Assert
            //Assert.IsNotNull(viewresult);
            //Assert.AreEqual(4, data.resultList.Count);
        }

    }
}
