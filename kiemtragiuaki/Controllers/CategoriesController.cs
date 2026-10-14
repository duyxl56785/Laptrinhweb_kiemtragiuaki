using kiemtragiuaki.Data;
using kiemtragiuaki.Localization;
using kiemtragiuaki.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kiemtragiuaki.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _db;

        public CategoriesController(AppDbContext db) => _db = db;

        public async Task<IActionResult> Index()
        {
            var categories = await _db.DishCategories.AsNoTracking()
                .Include(c => c.Dishes)
                .OrderBy(c => c.Name)
                .ToListAsync();
            return View(categories);
        }

        public IActionResult Create() => View(new DishCategory_BIT240081());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] DishCategory_BIT240081 category)
        {
            category.Name = category.Name?.Trim() ?? string.Empty;
            await ValidateCategory(category);
            if (!ModelState.IsValid) return View(category);

            _db.DishCategories.Add(category);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Msg_CategoryCreated";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFoundView(Translations.Get("Err_IdMissing"));

            var category = await _db.DishCategories.FindAsync(id.Value);
            if (category == null) return NotFoundView(Translations.Get("Err_CategoryNotFound", id));

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] DishCategory_BIT240081 input)
        {
            var category = await _db.DishCategories.FindAsync(id);
            if (category == null) return NotFoundView(Translations.Get("Err_CategoryNotFound", id));

            input.Id = id;
            input.Name = input.Name?.Trim() ?? string.Empty;
            await ValidateCategory(input);
            if (!ModelState.IsValid) return View(input);

            category.Name = input.Name;
            category.Description = input.Description;
            await _db.SaveChangesAsync();
            TempData["Success"] = "Msg_CategoryUpdated";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFoundView(Translations.Get("Err_IdMissing"));

            var category = await _db.DishCategories.AsNoTracking()
                .Include(c => c.Dishes)
                .FirstOrDefaultAsync(c => c.Id == id.Value);
            if (category == null) return NotFoundView(Translations.Get("Err_CategoryNotFound", id));

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _db.DishCategories.FindAsync(id);
            if (category == null) return NotFoundView(Translations.Get("Err_CategoryNotFound", id));

            // Business rule: a category that still has dishes cannot be deleted.
            var dishCount = await _db.Dishes.CountAsync(d => d.DishCategoryId == id);
            if (dishCount > 0)
            {
                TempData["Error"] = Translations.Get("Msg_CategoryInUse", category.Name, dishCount);
                return RedirectToAction(nameof(Index));
            }

            _db.DishCategories.Remove(category);
            await _db.SaveChangesAsync();
            TempData["Success"] = "Msg_CategoryDeleted";
            return RedirectToAction(nameof(Index));
        }

        private async Task ValidateCategory(DishCategory_BIT240081 category)
        {
            if (string.IsNullOrEmpty(category.Name)) return;
            var lower = category.Name.ToLower();
            if (await _db.DishCategories.AnyAsync(c => c.Id != category.Id && c.Name.ToLower() == lower))
                ModelState.AddModelError(nameof(category.Name), Translations.Get("Err_DuplicateCategoryName", category.Name));
        }

        private IActionResult NotFoundView(string message)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            ViewData["Message"] = message;
            return View("NotFound");
        }
    }
}
