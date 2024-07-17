namespace ReadDB.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;
using ReadDB.Models;

    public class DataAccess
    {
        private readonly string _connectionString;
        public DataAccess(IConfiguration configuration)
        {
            _connectionString =configuration.GetConnectionString("DefaultConnection");
        }
        public List<YourModel> GetData()
        {
            List<YourModel> dataList = new List<YourModel>();
            using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT pid,productname,price FROM pricess", conn))
                {
using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        YourModel data = new YourModel
                        {
// Assuming YourModel has properties that match your table
pid = reader.GetInt32(reader.GetOrdinal("pid")),
productname =reader.GetString(reader.GetOrdinal("productname")),
price = reader.GetInt32(reader.GetOrdinal("price"))
                            // Add other properties here
                        };
                        dataList.Add(data);
                    }
                }
            }
        }
return dataList;
}
}