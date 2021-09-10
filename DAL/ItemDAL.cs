using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
  public static class ItemFilter
  {
    public const int GET_ALL = 0;
    public const int FILTER_BY_ITEM_NAME = 1;
  }
  public class ItemDAL
  {
    private string query;
    private MySqlConnection connection = DbConfig.GetConnection();

    public Item GetItemById(int itemId)
    {
      Item item = null;
      try
      {
        connection.Open();
        query = @"select item_id, item_name, unit_price, amount, item_status,
                        ifnull(item_description, '') as item_description
                        from Items where item_id=@itemId;";
        MySqlCommand command = new MySqlCommand(query, connection);
        command.Parameters.AddWithValue("@itemId", itemId);
        MySqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
          item = GetItem(reader);
        }
        reader.Close();
      }
      catch { }
      finally { connection.Close(); }
      return item;
    }
    internal Item GetItem(MySqlDataReader reader)
    {
      Item item = new Item();
      item.ItemId = reader.GetInt32("item_id");
      item.ItemName = reader.GetString("item_name");
      item.ItemPrice = reader.GetDecimal("unit_price");
      item.Amount = reader.GetInt32("amount");
      item.Status = reader.GetInt16("item_status");
      item.Description = reader.GetString("item_description");
      return item;
    }
    public List<Item> GetItems(int itemFilter, Item item)
    {
      List<Item> lst = null;
      try
      {
        connection.Open();
        MySqlCommand command = new MySqlCommand("", connection);
        switch (itemFilter)
        {
          case ItemFilter.GET_ALL:
            query = @"select item_id, item_name, unit_price, amount, item_status, ifnull(item_description, '') as item_description from Items";
            break;
          case ItemFilter.FILTER_BY_ITEM_NAME:
            query = @"select item_id, item_name, unit_price, amount, item_status, ifnull(item_description, '') as item_description from Items
                                where item_name like concat('%',@itemName,'%');";
            command.Parameters.AddWithValue("@itemName", item.ItemName);
            break;
        }
        command.CommandText = query;
        MySqlDataReader reader = command.ExecuteReader();
        lst = new List<Item>();
        while (reader.Read())
        {
          lst.Add(GetItem(reader));
        }
        reader.Close();
      }
      catch { }
      finally
      {
        connection.Close();
      }
      return lst;
    }
  }
}