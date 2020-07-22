using System;
using System.Collections.Generic;
using GOSDataModel;
using GOSDataModel.Models;
using System.Linq;
using BusinessService.Interface;
using System.Net.WebSockets;

namespace BusinessService
{
	public class ProductService : Repository<Product>, IProductService
	{
		public GOSContext Context;
		public ProductService(GOSContext _context) : base(_context)
		{
			Context = _context;
		}

		public IEnumerable<Product> GetByCategoryId(int categoryId, int skip, int take)
		{
			return Context.Product.Where(s => s.CategoryId == categoryId).Skip(skip).Take(take).ToList();
		}

		public Product GetById(int Id)
		{
			return Context.Product.FirstOrDefault(s => s.Id == Id);
		}

		public IEnumerable<Product> GetHomePage()
		{
			IEnumerable<Product> allProducts = Context.Product.OrderBy(s => s.CategoryId).ToList();
			int PageSize = 8;
			if (allProducts != null && allProducts.Any())
			{
				List<int> distinctCatrgory = allProducts.Select(s => s.CategoryId).Distinct().ToList();
				List<Product> products = new List<Product>();
				if (distinctCatrgory.Any())
				{
					distinctCatrgory.ForEach(s =>
					{
						var top = allProducts.Where(p => p.CategoryId == s).Take(PageSize);
						products.AddRange(top);
					});
				}
				return products;
			}
			return allProducts;
		}
	}
}
