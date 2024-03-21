using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodosCS.Server.Controllers;

namespace TodosCS.Test
{
    public class TodosControllerTests
    {
        [Fact]
        public void GetAll_ReturnsEmptyList_WhenNoTodosExist()
        {
            var controller = new TodosController();
            var result = controller.GetAll();
            Assert.Empty(result);
        }
    }
}
