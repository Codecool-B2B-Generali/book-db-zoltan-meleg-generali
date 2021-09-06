using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using Codecool.BookDb.Model;

namespace Codecool.BookDb.Manager
{
    class BookDbManager
    {
        public static string Connect()
        {
            return ConfigurationManager.AppSettings["connectionString"];
        }
    }
}
