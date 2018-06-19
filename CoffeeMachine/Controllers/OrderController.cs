/*
 * Main controller of the API
 * Two endpoints to
 * a) insert and record new order
 * b) fetch last order from db
 * 
 **/

using CoffeeMachine.Models.Exceptions;
using CoffeeMachine.Models.Services;
using CoffeeMachine.ViewModel;
using Newtonsoft.Json.Linq;
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
                OrderLogger orderLogger = new OrderLogger(new OrderDetails("coffee", "11", 1));
                OrderDetails lastOrder = orderLogger.GetLastOrder();
                return Request.CreateResponse(HttpStatusCode.OK, lastOrder);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /*
         * Post request will insert order to db 
         * comprising information of type of drink, sugar amount 
         * and using your own mug or not
         * 
         * */
        // POST: api/Order/
        public HttpResponseMessage Post(OrderDetailViewModel model)
        {
            OrderDetails orderDetail = new OrderDetails();
            try
            {
                if (!orderDetail.GetAllDrinkOptions().Contains(model.Drink))
                {
                    throw new DrinkOutOfRangeException();
                }
                if (model.OwnMug != 1 && model.OwnMug != 0)
                {
                    throw new OwnMugOutOfRangeException();
                }
                try
                {
                    int sugarQty = Convert.ToInt32(model.SugarAmount);
                }
                catch (Exception ex)
                {
                    throw new SugarAmountTypeErrorException();
                }
                orderDetail.Drink = model.Drink;
                orderDetail.SugarAmount = model.SugarAmount;
                orderDetail.OwnMug = model.OwnMug;
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
