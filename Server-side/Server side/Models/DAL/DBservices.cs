using AirBNB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Server_side.Models
{
    public static class DBservices
    {
        // connect .
        private static SqlConnection Connect()
        {
            // read the connection string from the web.config file
            string connectionString = WebConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

            // create the connection to the db
            SqlConnection con = new SqlConnection(connectionString);

            // open the database connection
            con.Open();

            return con;
        }

        // Get All Invoices.
        public static List<Invoice> GetAllInvoices()
        {
            SqlConnection con = Connect();
            SqlCommand command;
            command = CreateCommand(con, "spGetAllInvoices");
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            List<Invoice> invoices = new List<Invoice>();

            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["Id"]);
                string status = dr["Status"].ToString();
                string date = dr["Date"].ToString();
                double amount = Convert.ToDouble(dr["Amount"]);
                invoices.Add(new Invoice(id, status, date, amount));
            }

            con.Close();
            return invoices;
        }

        // Get the last invoice inserted.
        public static Invoice GetTheLastInvoiceInserted()
        {
            SqlConnection con = Connect();
            SqlCommand command;
            command = CreateCommand(con, "spGetTheLastInvoiceInserted");
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            Invoice invoice = null;

            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["Id"]);
                string status = dr["Status"].ToString();
                string date = dr["Date"].ToString();
                double amount = Convert.ToDouble(dr["Amount"]);
                invoice = new Invoice(id, status, date, amount);
            }

            con.Close();
            return invoice;
        }

        // Create generic sql for 'GET' command.
        private static SqlCommand CreateCommand(SqlConnection con, string text)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = text;
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        // Get invoice by id.
        public static Invoice GetInvoiceById(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command;
            command = CreateGetCommand(con, id);
            SqlDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);
            Invoice invoice = null;

            while (dr.Read())
            {
                string status = dr["Status"].ToString();
                string date = dr["Date"].ToString();
                double amount = Convert.ToDouble(dr["Amount"]);
                invoice = new Invoice(id, status, date, amount);
            }

            con.Close();
            return invoice;
        }

        // Create 'GET' command.
        private static SqlCommand CreateGetCommand(SqlConnection con, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "spGetInvoiceById";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }


        // Post new invoice.
        public static Invoice PostInvoice(Invoice invoice)
        {
            SqlConnection con = Connect();
            SqlCommand command;
            command = CreatePostCommand(con, invoice);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            Invoice newInvoice = null;
            if (numAffected == 1)
            {
                newInvoice = GetTheLastInvoiceInserted();
            }
            return newInvoice;
        }

        //  Create 'POST' command.
        private static SqlCommand CreatePostCommand(SqlConnection con, Invoice invoice)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@date", invoice.Date);
            command.Parameters.AddWithValue("@status", invoice.Status);
            command.Parameters.AddWithValue("@amount", invoice.Amount);
            command.CommandText = "spPostInvoice";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        // Delete invoice by id.
        public static int DeleteInvoice(int id)
        {
            SqlConnection con = Connect();
            SqlCommand command;
            command = CreateDeleteCommand(con, id);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            return numAffected;
        }

        //  Create 'DELETE' command.
        private static SqlCommand CreateDeleteCommand(SqlConnection con, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.CommandText = "spDeleteInvoice";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }

        // Update invoice.
        public static Invoice PutInvoice(Invoice invoice, int id)
        {
            SqlConnection con = Connect();
            SqlCommand command;
            command = CreatePutCommand(con, invoice, id);
            int numAffected = command.ExecuteNonQuery();
            con.Close();
            Invoice newInvoice = null;
            if (numAffected == 1)
            {
                newInvoice = GetInvoiceById(id);
            }
            return newInvoice;
        }

        //  Create 'Put' command.
        private static SqlCommand CreatePutCommand(SqlConnection con, Invoice invoice, int id)
        {
            SqlCommand command = new SqlCommand();
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@date", invoice.Date);
            command.Parameters.AddWithValue("@status", invoice.Status);
            command.Parameters.AddWithValue("@amount", invoice.Amount);
            command.CommandText = "spPutInvoice";
            command.Connection = con;
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.CommandTimeout = 10; // in seconds
            return command;
        }
    }
}