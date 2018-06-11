using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace CoffeeMachine.Models
{
    public class OrderLogger
    {
        [Browsable(false)]
        public int? Id { get; set; }
        public int LastOrderId { get; set; }

        public OrderLogger(int lastOrderId)
        {
            this.Id = 1;
            this.LastOrderId = lastOrderId;
        }

        public void UpdateLogger()
        {
            try
            {
                using (Database db = new Database())
                {
                    db.OpenConnection();
                    string query =
                        "INSERT OR REPLACE INTO OrderLogger ('Id', 'OrderId') "
                        + "VALUES (@id, @lastOrderId);";
                    SQLiteCommand cmd = new SQLiteCommand(query, db.Connection);
                    cmd.Parameters.AddWithValue("@id", this.Id);
                    cmd.Parameters.AddWithValue("@lastOrderId", this.LastOrderId);
                    int result = cmd.ExecuteNonQuery();
                    db.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occurred: " + ex);
            }
            
        }
    }
}