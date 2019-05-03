using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessService.Interface;
using GOSDataModel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TOTURIAL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;

        public ProductsController(IRepositoryWrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            var products = _repoWrapper.ProductService.GetAndDoSomeThing();
            if(products.Any())
            {
                return Ok(products);
            }
            return NotFound();
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Product> Get(int id)
        {
            if (id < 0)
                return NotFound();
            var product = _repoWrapper.ProductService.Find(s => s.Id == id).FirstOrDefault();
            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public void Post([FromBody] Product value)
        {
            if (value == null)
                return;
            _repoWrapper.ProductService.Add(value);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product value)
        {
            if (id == 0 || value == null)
                return;
            _repoWrapper.ProductService.Update(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (id <= 0)
                return;
            var product = _repoWrapper.ProductService.Get(id);
            if(product != null)
                _repoWrapper.ProductService.Remove(product);
        }
    }
}
