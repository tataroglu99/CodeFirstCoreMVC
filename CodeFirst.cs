// İlişkili iki tablo (ör: Category ve Product) ile ASP.NET Core MVC Code First örneği:

// 1. Model Sınıfları
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    // Navigation property
    public ICollection<Product> Products { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    // Foreign key
    public int CategoryId { get; set; }
    // Navigation property
    public Category Category { get; set; }
}

// 2. DbContext Sınıfı
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}

public class UnitsConfiguration : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.HasMany(e => e.Products)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId)
            .HasPrincipalKey(e => e.Id);            
        }
    }

// 3. Controller (Product için örnek)
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class ProductsController : Controller
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Kategori ile birlikte ürünleri getir
        var products = _context.Products.Include(p => p.Category).ToList();
        return View(products);
    }

    [HttpGet]
    public IActionResult Create()
    {
        // Kategorileri ViewBag ile View'a gönder
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        ViewBag.Categories = _context.Categories.ToList();
        return View(product);
    }
}

// 4. appsettings.json dosyanıza bağlantı ekleyin:
// "ConnectionStrings": {
//   "DefaultConnection": "Server=.;Database=MyDb;Trusted_Connection=True;"
// }

// 5. Program.cs veya Startup.cs'de DbContext'i ekleyin:
// services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

// 6. Migration ve veritabanı oluşturmak için Package Manager Console'da:
// Add-Migration InitialCreate
// Update-Database

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=KitaplikDb;Trusted_Connection=True;TrustServerCertificate=True;"
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    dotnet ef migrations add InitialCreate
dotnet ef database update
Eğer dotnet ef komutu yoksa dotnet tool install --global dotnet-ef komutu ile yükleyebilirsin.


using System.Linq.Expressions;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _baseContext;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(DbContext baseContext)
        {
            _baseContext = baseContext;
            _dbSet = _baseContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _baseContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            var objects = _dbSet.Where(where).AsEnumerable();
            foreach (var obj in objects)
                _dbSet.Remove(obj);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.FirstOrDefaultAsync(where);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.Where(where).ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _baseContext.SaveChangesAsync() > 0;
        }
    }
}

public interface IRepository<T>
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        Task<T> GetByIdAsync(int id);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where);
        Task<bool> SaveChangesAsync();
    }

public class UnitRepository : BaseRepository<Unit>
    {
        public UnitRepository(DbContext baseContext) : base(baseContext)
        {
        }
    }