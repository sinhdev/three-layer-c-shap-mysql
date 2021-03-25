using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class ItemsDAL
    {
        private string query;
        private MySqlConnection connection;
        private MySqlDataReader reader;

        public ItemsDAL()
        {
            connection = DbConfiguration.OpenConnection();
            if(connection==null){
                connection = DBHelper.GetConnection();
            }
        }

        public Item GetItemById(int itemId)
        {
            if(connection.State == System.Data.ConnectionState.Closed){
                connection.Open();
            }
            query = @"select item_id, item_name, unit_price, amount, item_status,
                        ifnull(item_description, '') as item_description
                        from Items where item_id=" + itemId + ";";
            MySqlCommand command = new MySqlCommand(query, connection);
            reader = command.ExecuteReader();
            Item item = null;
            if (reader.Read())
            {
                item = GetItem(reader);
            }
            reader.Close();
            connection.Close();
            return item;
        }
        private Item GetItem(MySqlDataReader reader)
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
        private List<Item> GetItems(MySqlCommand command)
        {
            List<Item> lstItem = new List<Item>();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                lstItem.Add(GetItem(reader));
            }
            reader.Close();
            connection.Close();
            return lstItem;
        }
        public List<Item> GetItems(int itemFilter, Item item)
        {
            if(connection==null){
                return null;
            }
            if(connection.State == System.Data.ConnectionState.Closed){
                connection.Open();
            }
            MySqlCommand command = new MySqlCommand("", connection);
            switch(itemFilter)
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
            return GetItems(command);
        }
    }
}