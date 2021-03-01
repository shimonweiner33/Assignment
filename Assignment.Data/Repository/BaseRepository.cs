using System;
using System.Collections.Generic;
using System.Text;
//using Assignment.Data.Repository.Interface;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Assignment.Data.Repository
{
    public abstract class BaseRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionName;

        public BaseRepository(IConfiguration configuration, string connectionName = "DefaultConnection")
        {
            _configuration = configuration;
            this.connectionName = connectionName;
        }

        public IDbConnection Connection
        {
            get
            {
                var connectionStringFromTests = "";
                var connectionString = string.IsNullOrEmpty(_configuration.GetConnectionString(connectionName)) ? connectionStringFromTests : _configuration.GetConnectionString(connectionName);
                return new SqlConnection(connectionString);
            }
        }
    }
}
