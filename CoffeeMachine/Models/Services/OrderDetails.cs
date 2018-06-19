/*
 * A class for order detail consisting fields like
 *  - type of drink
 *  - sugar amount
 *  - use of your own mug or not 
 * for each order requested.
 * 
 * It handles the operation of fetching and inserting orders,
 * and also recalling Id of last operation
 * 
 * */

using CoffeeMachine.Models.Connection;
using CoffeeMachine.Models.Interfaces;
using System;
using System.Data.SQLite;

namespace CoffeeMachine.Models.Services
{
    public class OrderDetails : IOrderDetails
    {
        private readonly string[] _allowedDrink = { "coffee", "tea", "chocolate" };

        public int Id { get; set; }
        public string Drink { get; set; }
        public string SugarAmount { get; set; }
        public int OwnMug { get; set; }

        // constructor
        public OrderDetails()
        {

        }
    
        // overload
        public OrderDetails(string drink, string sugarAmount, int ownMug)
        {
            Drink = drink;
            SugarAmount = sugarAmount;
            OwnMug = ownMug;
        }

        
        // Main function to insert order
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
            OrderDetails orderDetail = new OrderDetails("coffee", "11", 1);
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