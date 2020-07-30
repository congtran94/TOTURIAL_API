using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessService.Interface;
using GOSDataModel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using BusinessService.Models;

namespace TOTURIAL_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigins")]
    public class ProductsController : ControllerBase
    {
        private IRepositoryWrapper _repoWrapper;
        private IMapper _mapper;
        public ProductsController(IRepositoryWrapper repoWrapper, IMapper mapper)
        {
            _repoWrapper = repoWrapper;
            _mapper = mapper;
        }
        [HttpGet("gethome")]
        public ActionResult<IEnumerable<ProductModel>> Get()
        {
            var products = _repoWrapper.ProductService.GetHomePage();
            if(products.Any())
            {
                return Ok(products);
            }
            return NoContent();
        }
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<ProductDetail> Get(int id)
        {
            if (id < 0)
                return NotFound();
            var product = _repoWrapper.ProductService.GetById(id);
            return Ok(product);
        }
        [HttpGet("getcategory")]
        public ActionResult<IEnumerable<ProductModel>> GetByCategory(PagingModel paging)
        {
            if (paging.CategoryId < 0)
                return NotFound();
            var products = _repoWrapper.ProductService.GetByCategoryId(paging.CategoryId, paging.PageIndex * paging.PageSize, paging.PageSize);
            if(products != null && products.Any())
                return Ok(products);
            return NoContent();
        }
        // POST: api/Products
        [HttpPost]
        public void Post([FromBody] ProductModel value)
        {
            if (value == null)
                return;
            var model = _mapper.Map<GOSDataModel.Models.Product>(value);
            _repoWrapper.ProductService.Add(model);
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
