using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
  public class CustomerDAL
  {
    private string query;
    private MySqlConnection connection = DbConfig.GetConnection();
    private MySqlDataReader reader;
    public CustomerDAL() { }
    public Customer GetById(int customerId)
    {
      Customer c = null;
      try
      {
        connection.Open();
        query = @"select customer_id, customer_name,
                        ifnull(customer_address, '') as customer_address
                        from Customers where customer_id=" + customerId + ";";
        reader = (new MySqlCommand(query, connection)).ExecuteReader();
        if (reader.Read())
        {
          c = GetCustomer(reader);
        }
        reader.Close();
      }
      catch { }
      finally
      {
        connection.Close();
      }
      return c;
    }
    internal Customer GetCustomer(MySqlDataReader reader)
    {
      Customer c = new Customer();
      c.CustmerId = reader.GetInt32("customer_id");
      c.CustomerName = reader.GetString("customer_name");
      c.CustomerAddress = reader.GetString("customer_address");
      return c;
    }
    public int? AddCustomer(Customer c)
    {
      int? result = null;
      if (connection.State == System.Data.ConnectionState.Closed)
      {
        connection.Open();
      }
      MySqlCommand cmd = new MySqlCommand("sp_createCustomer", connection);
      try
      {
        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@customerName", c.CustomerName);
        cmd.Parameters["@customerName"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters.AddWithValue("@customerAddress", c.CustomerAddress);
        cmd.Parameters["@customerAddress"].Direction = System.Data.ParameterDirection.Input;
        cmd.Parameters.AddWithValue("@customerId", MySqlDbType.Int32);
        cmd.Parameters["@customerId"].Direction = System.Data.ParameterDirection.Output;
        cmd.ExecuteNonQuery();
        result = (int)cmd.Parameters["@customerId"].Value;
      }
      catch { }
      finally
      {
        connection.Close();
      }
      return result;
    }
  }
}