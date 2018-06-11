using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;

namespace CoffeeMachine.Models
{
    public class OrderDetails
    {
        private readonly string[] _allowedDrink = {"coffee", "tea", "chocolate"};
        //private string _drink;

        [Browsable(false)]
        public int? Id { get; set; }
        //public string Drink
        //{
        //    get
        //    {
        //        return _drink;
        //    }
        //    set
        //    {
        //        //if (!_allowedDrink.All(x => x != value))
        //        if(!_allowedDrink.Contains(value))
        //        {
        //            throw new Exception("Not valid drink type (coffer/tea/chocolate only)");
        //        }    
        //        else
        //        {
        //            this._drink = value;
        //        }
        //    }
        //}
        public string Drink { get; set; }
        public int SugarAmount { get; set; }
        public int OwnMug { get; set; }

        public OrderDetails(string drink, int sugarAmount, int ownMug)
        {
            if (!_allowedDrink.Contains(drink))
            {
                var msg = "Not valid drink type (coffer/tea/chocolate only)";
                throw new Exception(msg);
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, msg);
            }

            Drink = drink;
            SugarAmount = sugarAmount;
            OwnMug = ownMug;
        }
        /**
         * INSERT RECORD INTO DATABASE
         * *
         */
        public int InsertOrder()
        {
            try
            {
                var id = -1;
                using (var db = new Database())
                {
                    db.OpenConnection();
                    var query =
                        "INSERT INTO OrderHistory ('Drink', 'SugarAmount', 'BroughtMug') "
                        + "VALUES (@drink, @sugarAmount, @ownMug);";
                    var cmd = new SQLiteCommand(query, db.Connection);
                    cmd.Parameters.AddWithValue("@drink", Drink);
                    cmd.Parameters.AddWithValue("@sugarAmount", SugarAmount);
                    cmd.Parameters.AddWithValue("@ownMug", OwnMug);
                    cmd.ExecuteNonQuery();
                    id = RecallLastOrderId(db);
                    db.CloseConnection();
                }
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred: " + ex);
            }
        }

        private int RecallLastOrderId(Database db)
        {
            string lastId;
            //var id = (int) lastId;
            try
            {
                var query = "SELECT last_insert_rowid() FROM OrderHistory;";
                var cmdGetLastId = new SQLiteCommand(query, db.Connection);
                lastId = cmdGetLastId.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred: " + ex);
            }

            return Convert.ToInt32(lastId);
        }
    }
}