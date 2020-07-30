using System;
using System.Collections.Generic;
using GOSDataModel;
using GOSDataModel.Models;
using System.Linq;
using BusinessService.Interface;
using System.Net.WebSockets;
using BusinessService.Models;

namespace BusinessService
{
	public class ProductService : Repository<Product>, IProductService
	{
		public GOSContext Context;
		public ProductService(GOSContext _context) : base(_context)
		{
			Context = _context;
		}

		public IEnumerable<ProductModel> GetByCategoryId(int categoryId, int skip, int take)
		{
			return Context.Product.Where(s => s.CategoryId == categoryId).Skip(skip).Take(take).Select(s => new ProductModel()
			{
				Id = s.Id,
				Name = s.Name,
				CategoryId = s.CategoryId,
				CategoryName = s.Category.Name,
				Description = s.Description,
				Price = s.Price,
				Status = s.Status,
				ImageUrl = s.UrlImage,
				TopHot = s.TopHot == 1
			}).ToList();
		}

		public ProductDetail GetById(int Id)
		{
			ProductDetail productDetail = Context.Product.Select(s => new ProductDetail()
			{
				Id = s.Id,
				Name = s.Name,
				CategoryId = s.CategoryId,
				CategoryName = s.Category.Name,
				Description = s.Description,
				Price = s.Price,
				Status = s.Status,
				ImageUrl = s.UrlImage,
				TopHot = s.TopHot == 1
			}).FirstOrDefault(s => s.Id == Id);
			if(productDetail!= null)
			{
				productDetail.ProductColors = Context.ProductColor.Where(s => s.ProductId == Id).ToList();
				productDetail.RelateProducts = Context.Product.Where(s => s.CategoryId == productDetail.CategoryId && s.Id != Id).Select(s=>  new ProductModel {
					Id = s.Id,
					Name = s.Name,
					CategoryId = s.CategoryId,
					Price = s.Price,
					ImageUrl = s.UrlImage,
				}) .Take(4).ToList();
			}	
			return productDetail;
		}

		public IEnumerable<ProductModel> GetHomePage()
		{
			IEnumerable<ProductModel> allProducts = Context.Product.Select(s=> new ProductModel()
			{
				Id = s.Id,
				Name = s.Name,
				CategoryId =s.CategoryId,
				CategoryName =s.Category.Name,
				Description =s.Description,
				Price = s.Price,
				Status =s.Status,
				ImageUrl =s.UrlImage,
				TopHot = s.TopHot == 1
			}).Where(p => p.TopHot == true).Take(8).ToList();
			return allProducts;
		}
	}
}
