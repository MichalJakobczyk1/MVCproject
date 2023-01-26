using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MVCproject.Data;
using MVCproject.Data.Enum;
using MVCproject.Interfaces;
using MVCproject.Models;
using MVCproject.Repositories;
using MVCproject.ViewModels;
using System.Net;

namespace MVCproject.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IPhotoService _photoService;
        private readonly AppDbContext _context;
        public ProductController(IProductRepository productRepository, IPhotoService photoService,AppDbContext context)
        {
            _productRepository = productRepository;
            _photoService = photoService;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _productRepository.GetAll();
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(productViewModel.Image);

                var product = new Product
                {
                    Image = result.Url.ToString(),
                    Name = productViewModel.Name,
                    Description = productViewModel.Description,
                    Price= productViewModel.Price,
                    Volume= productViewModel.Volume,
                    AlcVolume= productViewModel.AlcVolume,
                };
                _productRepository.Add(product);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(productViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var productDetails = await _productRepository.GetByIdAsync(id);
            if (productDetails == null) return View("Error");
            return View(productDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productDetails = await _productRepository.GetByIdAsync(id);
            if (productDetails == null) return View("Error");

            _productRepository.Delete(productDetails);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null) return View("Error");
            var editProductViewModel = new EditProductViewModel
            {
                URL = product.Image,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Volume = product.Volume,
                AlcVolume = product.AlcVolume,
            };
            return View(editProductViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductViewModel editProductViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit product");
                return View("Edit", editProductViewModel);
            }

            var productPhoto = await _productRepository.GetByIdAsyncNoTracking(id);

            if (productPhoto != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(productPhoto.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editProductViewModel);
                }


                var photoResult = await _photoService.AddPhotoAsync(editProductViewModel.Image);

                var product = new Product
                {
                    Id = id,
                    Image = photoResult.Url.ToString(),
                    Name = editProductViewModel.Name,
                    Description = editProductViewModel.Description,
                    Price = editProductViewModel.Price,
                    Volume = editProductViewModel.Volume,
                    AlcVolume = editProductViewModel.AlcVolume,
                };

                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editProductViewModel);
            }
        }
        public async Task<IActionResult> Detail(int id)
        {
            Product product = await _productRepository.GetByIdAsync(id);
            return View(product);
        }
        public async Task<IActionResult> Search(string name, Products category)
        {
            var product = from m in _context.Products
                           select m;

            if (!string.IsNullOrEmpty(name))
            {
                product = product.Where(x => x.Name!.Contains(name));
            }

            return View(product.ToList());
        }
        public async Task<IActionResult> OrderByName()
        {
            return View(_context.Products.OrderByDescending(p => p.Name).ToList());
        }

        public async Task<IActionResult> OrderByCategory()
        {
            return View(_context.Products.OrderByDescending(p => p.Category).ToList());
        }
    }

}
