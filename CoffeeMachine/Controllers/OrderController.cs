using CoffeeMachine.Models.Exceptions;
using CoffeeMachine.Models.Services;
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
        /* 
        * Get request will fetch last order detail
        */

        // GET: api/Order
        public HttpResponseMessage Get()
        {
            try
            {
                OrderLogger orderLogger = new OrderLogger(new OrderDetails());
                OrderDetails lastOrder = orderLogger.GetLastOrder();
                return Request.CreateResponse(HttpStatusCode.OK, lastOrder);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
            //return Request.CreateResponse(HttpStatusCode.OK, "hello world");
        }

        // POST: api/Order/
        public HttpResponseMessage Post([FromBody] OrderDetails orderDetail)
        {
            try
            {
                if (!orderDetail.GetAllDrinkOptions().Contains(orderDetail.Drink))
                {
                    throw new DrinkOutOfRangeException();
                }
                if(orderDetail.OwnMug != 1 && orderDetail.OwnMug != 0)
                {
                    throw new OwnMugOutOfRangeException();
                }
                //if(orderDetail.SugarAmount)
                try
                {
                    int sugarQty = Convert.ToInt32(orderDetail.SugarAmount);
                }
                catch(Exception ex)
                {
                    throw new SugarAmountTypeErrorException();
                }
                //var sugarQty = (int) orderDetail.SugarAmount;
                int orderId = orderDetail.InsertOrder();
                orderDetail.Id = orderId;
                OrderLogger orderLogger = new OrderLogger(orderDetail);
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
