using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NwRestApi2021s.Models;

namespace NwRestApi2021s.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private static readonly northwindContext db = new northwindContext();

        [HttpGet]
        public ActionResult GetAll()
        {
           
            try
            {
                var customers = db.Customers.ToList();

                return Ok(customers);
            }
            catch(Exception e)
            {
                return BadRequest("Virhe tapahtui: " + e);
            }
        }


        // Get 1 customer by id
        // polku: https://localhost:5001/api/customers/alfki
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOneCustomer(string id)
        {
           Customer asiakas = db.Customers.Find(id);

            return Ok(asiakas);
        }


        // Get customers by Country
        // polku: https://localhost:5001/api/customers/country/finland
        [HttpGet]
        [Route("country/{key}")]
        public ActionResult GetCustomerByCountry(string key)
        {
            var asiakkaat = (from c in db.Customers where c.Country == key select c).ToList();

            return Ok(asiakkaat);
        }

        //Add new customer
        [HttpPost]
        public ActionResult AddCustomer([FromBody] Customer cust)
        {
            try
            {
                db.Customers.Add(cust);
                db.SaveChanges();
                return Ok("Lisättiin asiakas id:llä: " + cust.CustomerId);
            }
            catch (Exception e)
            {
                return BadRequest("Virhe. Lue lisää tästä: " + e);
            }

        }
    }
}
