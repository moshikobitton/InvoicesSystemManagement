using AirBNB.Models;
using Server_side.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Server_side.Controllers
{
    public class InvoicesController : ApiController
    {
        // GET
        public IHttpActionResult Get()
        {
            try
            {
                Invoice i = new Invoice();
                return Ok(i.GetAllInvoices());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Post
        public IHttpActionResult Post([FromBody] Invoice invoice)
        {
            try
            {
                Invoice newInvoice = invoice.PostInvoice();
                return Ok(newInvoice);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Invoice invoice = new Invoice();
                int res = invoice.DeleteInvoice(id);
                if(res == 1)
                {
                    return Ok();
                }
                return Content(HttpStatusCode.NotFound, $"invoice with id={id} was not found!!!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        public IHttpActionResult Put(int id, [FromBody] Invoice invoice)
        {
            try
            {
                Invoice newInvoice = invoice.PutInvoice(id);
                return Created(new Uri(Request.RequestUri.AbsoluteUri + newInvoice.Id), newInvoice);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}