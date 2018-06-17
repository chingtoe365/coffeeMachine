using CoffeeMachine.Models.Connection;
using CoffeeMachine.Models.Exceptions;
using CoffeeMachine.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;

namespace CoffeeMachine.Models.Services
{
    public class OrderDetails : IOrderDetails
    {
        private readonly string[] _allowedDrink = {"coffee", "tea", "chocolate"};

        public int Id { get; set; }
        public string Drink { get; set; }
        public string SugarAmount { get; set; }
        public int OwnMug { get; set; }

        public OrderDetails()
        {
            //var check = this.SugarAmount;
        }

        public OrderDetails(string drink, string sugarAmount, int ownMug)
        {
            Drink = drink;
            SugarAmount = sugarAmount;
            OwnMug = ownMug;
        }

        //public OrderDetails(string Drink, int SugarAmount, int OwnMug)
        //{
        //    string type = SugarAmount.GetType().ToString();
        //    if (!type.Equals("Integer"))
        //    {
        //        throw new Exception("Sugar amount not right");
        //    }
        //}

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

        public OrderDetails GetOrderById(int id)
        {
            OrderDetails orderDetail = new OrderDetails();
            try
            {
                using (var db = new Database())
                {
                    db.OpenConnection();
                    var query =
                        "SELECT * FROM OrderHistory WHERE `Id` == @id";
                    var cmd = new SQLiteCommand(query, db.Connection);
                    cmd.Parameters.AddWithValue("@id", id);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int drinkOrdinal = reader.GetOrdinal("Drink");
                        int sugarQtOrdinal = reader.GetOrdinal("SugarAmount");
                        int ownMugOrdinal = reader.GetOrdinal("BroughtMug");
                        orderDetail.Drink = reader.GetFieldValue<string>(drinkOrdinal);
                        orderDetail.SugarAmount = reader.GetValue(sugarQtOrdinal).ToString();
                        orderDetail.OwnMug = Convert.ToInt32(reader.GetValue(ownMugOrdinal));
                    }
                    db.CloseConnection();
                }
                return orderDetail;
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

        public string[] GetAllDrinkOptions()
        {
            return _allowedDrink;
        }

    }
}