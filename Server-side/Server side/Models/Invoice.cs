
using Server_side.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AirBNB.Models
{
    public class Invoice
    {
        private int id;
        private string status;
        private string date;
        private double amount;

        public Invoice()
        {
        }

        public Invoice(int id, string status, string date, double amount)
        {
            Id = id;
            Status = status;
            Date = date;
            Amount = amount;
        }

        public int Id { get => id; set => id = value; }
        public string Status { get => status; set => status = value; }
        public string Date { get => date; set => date = value; }
        public double Amount { get => amount; set => amount = value; }

        public List<Invoice> GetAllInvoices()
        {
            return DBservices.GetAllInvoices();
        }

        public Invoice PostInvoice()
        {
            return DBservices.PostInvoice(this);
        }
        public int DeleteInvoice(int id)
        {
            return DBservices.DeleteInvoice(id);
        }

        public Invoice PutInvoice(int id)
        {
            return DBservices.PutInvoice(this, id);
        }


    }
}