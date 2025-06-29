using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Final_Project.ViewModels.Admin.Product;
using Final_Project.ViewModels.User.UIProduct;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<ProductVM>> GetAllAsync()
        {
            var products = await _context.Products
                .Include(p => p.ProductAuthors).ThenInclude(pa => pa.Author)
                .ToListAsync();

            return products.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Img = p.Img,
                Authors = p.ProductAuthors.Select(x => x.Author.FullName).ToList()
            }).ToList();
        }

        public async Task<ProductVM> GetByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductAuthors).ThenInclude(pa => pa.Author)
                .Include(p => p.ProductGenres).ThenInclude(pg => pg.Genre)  // buranı əlavə et
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) throw new Exception("Product not found");

            return new ProductVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Img = product.Img,
                Authors = product.ProductAuthors.Select(x => x.Author.FullName).ToList(),
                Genres = product.ProductGenres.Select(x => x.Genre.Name).ToList() // janrları da əlavə et
            };
        }


        public async Task CreateAsync(ProductCreateVM model)
        {
            string fileName = null;
            if (model.ImgFile != null)
            {
                fileName = SaveImage(model.ImgFile);
            }

            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Img = fileName,
                ProductAuthors = model.AuthorIds.Select(id => new ProductAuthor
                {
                    AuthorId = id
                }).ToList()
            };

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, ProductEditVM model)
        {
            var product = await _context.Products
                .Include(p => p.ProductAuthors)
                .Include(p => p.ProductGenres)  // janrları da yüklə
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) throw new Exception("Product not found");

            if (model.ImgFile != null)
            {
                string newFileName = SaveImage(model.ImgFile);

                if (!string.IsNullOrEmpty(product.Img))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "uploads", "products", product.Img);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                product.Img = newFileName;
            }

            product.Name = model.Name;
            product.Price = model.Price;

            // Müəllifləri yenilə
            product.ProductAuthors.Clear();
            foreach (var authorId in model.AuthorIds)
            {
                product.ProductAuthors.Add(new ProductAuthor
                {
                    AuthorId = authorId
                });
            }

            // Janrları yenilə
            product.ProductGenres.Clear();
            foreach (var genreId in model.GenreIds)
            {
                product.ProductGenres.Add(new ProductGenre
                {
                    GenreId = genreId
                });
            }

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) throw new Exception("Product not found");

            if (!string.IsNullOrEmpty(product.Img))
            {
                string path = Path.Combine(_env.WebRootPath, "uploads", "products", product.Img);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        private string SaveImage(IFormFile file)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string folderPath = Path.Combine(_env.WebRootPath, "uploads", "products");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string fullPath = Path.Combine(folderPath, fileName);
            using FileStream stream = new(fullPath, FileMode.Create);
            file.CopyTo(stream);

            return fileName;
        }
        public async Task<int> GetCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<List<ProductVM>> GetPagedAsync(int page, int pageSize)
        {
            var products = await _context.Products
                .Include(p => p.ProductAuthors).ThenInclude(pa => pa.Author)
                .Include(p => p.ProductGenres).ThenInclude(pg => pg.Genre)
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return products.Select(p => new ProductVM
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Img = p.Img,
                Authors = p.ProductAuthors.Select(x => x.Author.FullName).ToList(),
                Genres = p.ProductGenres.Select(x => x.Genre.Name).ToList()
            }).ToList();
        }
        //public async Task<List<ProductSearchVM>> LiveSearchAsync(string? query)
        //{
        //    IQueryable<Product> products = _context.Products
        //        .Include(p => p.ProductAuthors)
        //            .ThenInclude(pa => pa.Author);

        //    if (!string.IsNullOrWhiteSpace(query))
        //    {
        //        var loweredQuery = query.ToLower();
        //        products = products.Where(p =>
        //            p.Name.ToLower().Contains(loweredQuery) ||
        //            p.ProductAuthors.Any(pa => pa.Author.FullName.ToLower().Contains(loweredQuery)));
        //    }

        //    return await products.Select(p => new ProductSearchVM
        //    {
        //        Id = p.Id,
        //        Name = p.Name,
        //        Img = p.Img,
        //        Price = p.Price,
        //        Authors = p.ProductAuthors.Select(pa => pa.Author.FullName).ToList()
        //    }).ToListAsync();
        //}


        public async Task<List<ProductSearchVM>> LiveSearchAsync(string? query)
        {
            IQueryable<Product> products = _context.Products
                .Include(p => p.ProductAuthors)
                    .ThenInclude(pa => pa.Author);

            if (!string.IsNullOrWhiteSpace(query))
            {
                var loweredQuery = query.ToLower();
                products = products.Where(p =>
                    p.Name.ToLower().Contains(loweredQuery) ||
                    p.ProductAuthors.Any(pa => pa.Author.FullName.ToLower().Contains(loweredQuery)));
            }

            var result = await products
                .Select(p => new ProductSearchVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    Img = p.Img,
                    Price = p.Price,
                    Authors = p.ProductAuthors
                                .Select(pa => pa.Author.FullName)
                                .ToList()
                })
                .ToListAsync();

            return result;
        }

    }
}
