using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.IO;

namespace DAL
{
    public class DbConfig
    {
        private static MySqlConnection connection = new MySqlConnection();
        public static MySqlConnection OpenDefaultConnection()
        {
            try{
                connection.ConnectionString = "server=localhost;user id=vtca;password=vtcacademy;port=3306;database=OrderDB;";
                connection.Open();
                return connection;
            }catch{
                return null;
            }
        }

        public static MySqlConnection OpenConnection()
        {
            try{
                string conString;
                using (System.IO.FileStream fileStream = System.IO.File.OpenRead("DbConfig.txt"))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                    {
                        conString = reader.ReadLine();
                    }
                }
                return OpenConnection(conString);
            }catch{
                return null;
            }
        }

        public static MySqlConnection OpenConnection(string connectionString)
        {
            try{
                connection.ConnectionString = connectionString;
                connection.Open();
                return connection;
            }catch{
                return null;
            }
        }
    }
}