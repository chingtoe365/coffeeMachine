using CoffeeMachine.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CoffeeMachine.Controllers
{
    public class OrderController : ApiController
    {
        // GET: api/Order
        public IEnumerable<string> Get()
        {
            return new string[] { "coffee", "tea", "chocolate" };
        }

        // GET: api/Order/tea/2/true
        public HttpResponseMessage Post([FromBody] OrderDetails orderDetail)
        {
            try
            {
                int orderId = orderDetail.InsertOrder();
                orderDetail.Id = orderId;
                OrderLogger orderLogger = new OrderLogger(orderId);
                orderLogger.UpdateLogger();
                var message = Request.CreateResponse(HttpStatusCode.Created, orderDetail);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }


        
        
    }
}
