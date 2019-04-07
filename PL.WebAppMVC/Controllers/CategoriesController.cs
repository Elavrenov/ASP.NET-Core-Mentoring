using System.Threading.Tasks;
using BLL.CoreEntities.Entities.UpdateEntities;
using BLL.Interfaces.Interfaces;
using DAL.EF.Mapper;
using Microsoft.AspNetCore.Mvc;
using PL.WebAppMVC.Filters;

namespace PL.WebAppMVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        [TypeFilter(typeof(ActionFilter),
            Arguments = new object[] { "Method 'Index' controller 'Categories'" })]
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAllCategoriesAsync());
        }

        // GET: Categories/Details/5
        [TypeFilter(typeof(ActionFilter),
            Arguments = new object[] { "Method 'Details' controller 'Categories'" })]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetCategoryByIdAsync(id);

            if (categories == null)
            {
                return NotFound();
            }

            return View(categories);
        }

        // GET: Categories/Create
        [TypeFilter(typeof(ActionFilter),
            Arguments = new object[] { "Method 'Create' controller 'Categories'" })]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryName,Description,Picture")] UpdateCategory categories)
        {
            if (!ModelState.IsValid)
            {
                return View(categories);
            }

            await _categoryService.CreateCategoryAsync(categories);

            return RedirectToAction(nameof(Index));
        }

        //// GET: Categories/Edit/5
        [TypeFilter(typeof(ActionFilter),
            Arguments = new object[] { "Method 'Edit' controller 'Categories'" })]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(Mapper.ToUpdateCategoryModel(category));
        }

        //// POST: Categories/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryName,Description,Picture")]
            UpdateCategory category)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateCategoryAsync(id, category);
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }

        [Route("[controller]/[action]/{id}")]
        [Route("/[action]/{id}")]
        [TypeFilter(typeof(ActionFilter),
            Arguments = new object[] { "Method 'Image' controller 'Categories'" })]
        public async Task<IActionResult> Image(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dbImage = await _categoryService.GetPictureByCategoryId(id);

            return View(dbImage);
        }
    }
}