using CoffeeMachine.Models.Connection;
using CoffeeMachine.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace CoffeeMachine.Models.Services
{
    public class OrderLogger : IOrderLogger
    {
        public int Id { get; set; }
        public int LastOrderId { get; set; }
        public string Drink { get; set; }
        public string SugarAmount { get; set; }
        public int OwnMug { get; set; }

        public OrderLogger(OrderDetails orderDetails)
        {
            this.Id = 1;
            this.LastOrderId = orderDetails.Id;
            this.Drink = orderDetails.Drink;
            this.SugarAmount = orderDetails.SugarAmount;
            this.OwnMug = orderDetails.OwnMug;
        }

        public void UpdateLogger()
        {
            try
            {
                using (Database db = new Database())
                {
                    db.OpenConnection();
                    string query =
                        "INSERT OR REPLACE INTO OrderLogger "
                        + "('Id', 'OrderId', 'Drink', 'SugarAmount', 'BroughtMug') "
                        + "VALUES (@id, @lastOrderId, @drink, @sugarAmount, @ownMug);";
                    SQLiteCommand cmd = new SQLiteCommand(query, db.Connection);
                    cmd.Parameters.AddWithValue("@id", this.Id);
                    cmd.Parameters.AddWithValue("@lastOrderId", this.LastOrderId);
                    cmd.Parameters.AddWithValue("@drink", this.Drink);
                    cmd.Parameters.AddWithValue("@sugarAmount", this.SugarAmount);
                    cmd.Parameters.AddWithValue("@ownMug", this.OwnMug);
                    int result = cmd.ExecuteNonQuery();
                    db.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred: " + ex);
            }
            
        }

        public OrderDetails GetLastOrder()
        {
            OrderDetails orderDetail = new OrderDetails();
            try
            {
                using (Database db = new Database())
                {
                    db.OpenConnection();
                    string query = "SELECT * FROM OrderLogger WHERE `Id` == 1";
                    SQLiteCommand cmd = new SQLiteCommand(query, db.Connection);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        int drinkOrdinal = reader.GetOrdinal("Drink");
                        int sugarQtOrdinal = reader.GetOrdinal("SugarAmount");
                        int ownMugOrdinal = reader.GetOrdinal("BroughtMug");
                        orderDetail.Drink = reader.GetFieldValue<string>(drinkOrdinal);
                        orderDetail.SugarAmount = reader.GetFieldValue<string>(sugarQtOrdinal);
                        orderDetail.OwnMug = Convert.ToInt32(reader.GetValue(ownMugOrdinal));
                    }
                    db.CloseConnection();
                }
            }
            catch(Exception ex)
            {

            }
            
            return orderDetail;
        }
    }
}