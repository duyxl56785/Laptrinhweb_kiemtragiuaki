using System.Globalization;
using kiemtragiuaki.Data;
using kiemtragiuaki.Localization;
using kiemtragiuaki.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace kiemtragiuaki.Controllers
{
    public class DishesController : Controller
    {
        private readonly AppDbContext _db;

        public DishesController(AppDbContext db) => _db = db;

        // GET: /Dishes?search=&categoryId=&status=&minPrice=&maxPrice=&sort=
        public async Task<IActionResult> Index(string? search, int? categoryId, string? status,
            string? minPrice, string? maxPrice, string? sort)
        {
            var vm = new DishFilterViewModel
            {
                Search = search?.Trim(),
                CategoryId = categoryId,
                Status = status,
                Sort = sort,
                Categories = await _db.DishCategories.AsNoTracking().OrderBy(c => c.Name).ToListAsync()
            };

            // Build the query; every filter is translated to SQL and executed by PostgreSQL.
            IQueryable<Dish_BIT240081> query = _db.Dishes.AsNoTracking()
                .Include(d => d.DishCategory)
                .Include(d => d.Images.Where(i => i.IsThumbnail));

            if (!string.IsNullOrEmpty(vm.Search))
            {
                var pattern = "%" + vm.Search.Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_") + "%";
                query = query.Where(d => EF.Functions.ILike(d.Name, pattern, "\\"));
            }

            if (categoryId.HasValue)
                query = query.Where(d => d.DishCategoryId == categoryId.Value);

            if (status == "available")
                query = query.Where(d => d.IsAvailable);
            else if (status == "unavailable")
                query = query.Where(d => !d.IsAvailable);

            // Price range: validate before applying.
            var minOk = TryParsePrice(minPrice, out var min);
            var maxOk = TryParsePrice(maxPrice, out var max);
            vm.MinPrice = min;
            vm.MaxPrice = max;
            ViewData["MinPriceRaw"] = minPrice;
            ViewData["MaxPriceRaw"] = maxPrice;

            if (!minOk || !maxOk)
                vm.Errors.Add(Translations.Get("Err_InvalidPriceInput"));
            else if (min < 0 || max < 0)
                vm.Errors.Add(Translations.Get("Err_PriceNegative"));
            else if (min.HasValue && max.HasValue && min > max)
                vm.Errors.Add(Translations.Get("Err_PriceRange"));
            else
            {
                if (min.HasValue) query = query.Where(d => d.Price >= min.Value);
                if (max.HasValue) query = query.Where(d => d.Price <= max.Value);
            }

            query = sort switch
            {
                "price_asc" => query.OrderBy(d => d.Price).ThenBy(d => d.Name),
                "price_desc" => query.OrderByDescending(d => d.Price).ThenBy(d => d.Name),
                "prep_asc" => query.OrderBy(d => d.PreparationTime).ThenBy(d => d.Name),
                _ => query.OrderBy(d => d.Name)
            };

            vm.Dishes = await query.ToListAsync();
            vm.TotalDishes = await _db.Dishes.CountAsync();
            vm.AvailableDishes = await _db.Dishes.CountAsync(d => d.IsAvailable);
            return View(vm);
        }

        private static bool TryParsePrice(string? raw, out decimal? value)
        {
            value = null;
            if (string.IsNullOrWhiteSpace(raw)) return true;
            if (decimal.TryParse(raw.Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out var v))
            {
                value = v;
                return true;
            }
            return false;
        }

        // GET: /Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFoundView(Translations.Get("Err_IdMissing"));

            var dish = await LoadDishWithImages(id.Value);
            if (dish == null) return NotFoundView(Translations.Get("Err_DishNotFound", id));

            return View(new DishDetailsViewModel { Dish = dish });
        }

        // GET: /Dishes/Create
        public async Task<IActionResult> Create()
        {
            await LoadCategories(null);
            return View(new Dish_BIT240081 { IsAvailable = true });
        }

        // POST: /Dishes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name,Price,PreparationTime,IsAvailable,Description,DishCategoryId")] Dish_BIT240081 dish)
        {
            dish.Name = dish.Name?.Trim() ?? string.Empty;
            await ValidateDish(dish);

            if (ModelState.IsValid)
            {
                _db.Dishes.Add(dish);
                if (await TrySave())
                {
                    TempData["Success"] = "Msg_DishCreated";
                    return RedirectToAction(nameof(Details), new { id = dish.Id });
                }
            }

            await LoadCategories(dish.DishCategoryId);
            return View(dish);
        }

        // GET: /Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFoundView(Translations.Get("Err_IdMissing"));

            var dish = await _db.Dishes.FindAsync(id.Value);
            if (dish == null) return NotFoundView(Translations.Get("Err_DishNotFound", id));

            await LoadCategories(dish.DishCategoryId);
            return View(dish);
        }

        // POST: /Dishes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Name,Price,PreparationTime,IsAvailable,Description,DishCategoryId")] Dish_BIT240081 input)
        {
            var dish = await _db.Dishes.FindAsync(id);
            if (dish == null) return NotFoundView(Translations.Get("Err_DishNotFound", id));

            input.Id = id;
            input.Name = input.Name?.Trim() ?? string.Empty;
            await ValidateDish(input);

            if (ModelState.IsValid)
            {
                dish.Name = input.Name;
                dish.Price = input.Price;
                dish.PreparationTime = input.PreparationTime;
                dish.IsAvailable = input.IsAvailable;
                dish.Description = input.Description;
                dish.DishCategoryId = input.DishCategoryId;

                if (await TrySave())
                {
                    TempData["Success"] = "Msg_DishUpdated";
                    return RedirectToAction(nameof(Details), new { id });
                }
            }

            await LoadCategories(input.DishCategoryId);
            return View(input);
        }

        // GET: /Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFoundView(Translations.Get("Err_IdMissing"));

            var dish = await LoadDishWithImages(id.Value);
            if (dish == null) return NotFoundView(Translations.Get("Err_DishNotFound", id));

            return View(dish);
        }

        // POST: /Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _db.Dishes.FindAsync(id);
            if (dish == null) return NotFoundView(Translations.Get("Err_DishNotFound", id));

            _db.Dishes.Remove(dish); // images are removed by cascade
            await _db.SaveChangesAsync();
            TempData["Success"] = "Msg_DishDeleted";
            return RedirectToAction(nameof(Index));
        }

        // POST: /Dishes/AddImage/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(int id,
            [Bind("ImageUrl,IsThumbnail", Prefix = "NewImage")] DishImage_BIT240081 newImage)
        {
            var dish = await LoadDishWithImages(id);
            if (dish == null) return NotFoundView(Translations.Get("Err_DishNotFound", id));

            newImage.ImageUrl = newImage.ImageUrl?.Trim() ?? string.Empty;
            if (!string.IsNullOrEmpty(newImage.ImageUrl) &&
                !(Uri.TryCreate(newImage.ImageUrl, UriKind.Absolute, out var uri) &&
                  (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)))
            {
                ModelState.AddModelError("NewImage.ImageUrl",
                    Translations.Get("Val_Url", Translations.Get("Field_ImageUrl")));
            }

            if (!ModelState.IsValid)
                return View(nameof(Details), new DishDetailsViewModel { Dish = dish, NewImage = newImage });

            await using var tx = await _db.Database.BeginTransactionAsync();

            // The first image of a dish automatically becomes its thumbnail.
            var makeThumbnail = newImage.IsThumbnail || !dish.Images.Any();
            if (makeThumbnail)
                await ClearThumbnail(id);

            _db.DishImages.Add(new DishImage_BIT240081
            {
                DishId = id,
                ImageUrl = newImage.ImageUrl,
                IsThumbnail = makeThumbnail
            });
            await _db.SaveChangesAsync();
            await tx.CommitAsync();

            TempData["Success"] = "Msg_ImageAdded";
            return RedirectToAction(nameof(Details), new { id });
        }

        // POST: /Dishes/SetThumbnail/3  (id = image id)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetThumbnail(int id)
        {
            var image = await _db.DishImages.FindAsync(id);
            if (image == null) return NotFoundView(Translations.Get("Err_ImageNotFound", id));

            await using var tx = await _db.Database.BeginTransactionAsync();
            await ClearThumbnail(image.DishId);
            image.IsThumbnail = true;
            await _db.SaveChangesAsync();
            await tx.CommitAsync();

            TempData["Success"] = "Msg_ThumbnailChanged";
            return RedirectToAction(nameof(Details), new { id = image.DishId });
        }

        // POST: /Dishes/DeleteImage/3  (id = image id)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _db.DishImages.FindAsync(id);
            if (image == null) return NotFoundView(Translations.Get("Err_ImageNotFound", id));

            var dishId = image.DishId;
            _db.DishImages.Remove(image);
            await _db.SaveChangesAsync();

            // If the thumbnail was removed, promote the oldest remaining image.
            if (image.IsThumbnail)
            {
                var next = await _db.DishImages.Where(i => i.DishId == dishId).OrderBy(i => i.Id).FirstOrDefaultAsync();
                if (next != null)
                {
                    next.IsThumbnail = true;
                    await _db.SaveChangesAsync();
                }
            }

            TempData["Success"] = "Msg_ImageDeleted";
            return RedirectToAction(nameof(Details), new { id = dishId });
        }

        // ---- helpers ----

        private Task<Dish_BIT240081?> LoadDishWithImages(int id) =>
            _db.Dishes
                .Include(d => d.DishCategory)
                .Include(d => d.Images.OrderByDescending(i => i.IsThumbnail).ThenBy(i => i.Id))
                .FirstOrDefaultAsync(d => d.Id == id);

        private Task<int> ClearThumbnail(int dishId) =>
            _db.DishImages
                .Where(i => i.DishId == dishId && i.IsThumbnail)
                .ExecuteUpdateAsync(s => s.SetProperty(i => i.IsThumbnail, false));

        private async Task ValidateDish(Dish_BIT240081 dish)
        {
            if (dish.DishCategoryId > 0 && !await _db.DishCategories.AnyAsync(c => c.Id == dish.DishCategoryId))
                ModelState.AddModelError(nameof(dish.DishCategoryId), Translations.Get("Err_CategoryInvalid"));

            if (!string.IsNullOrEmpty(dish.Name))
            {
                var lower = dish.Name.ToLower();
                var duplicate = await _db.Dishes.AnyAsync(d =>
                    d.DishCategoryId == dish.DishCategoryId && d.Id != dish.Id && d.Name.ToLower() == lower);
                if (duplicate)
                    ModelState.AddModelError(nameof(dish.Name), Translations.Get("Err_DuplicateDishName", dish.Name));
            }
        }

        private async Task<bool> TrySave()
        {
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                // e.g. unique (category, name) violated by a concurrent insert
                ModelState.AddModelError(string.Empty, Translations.Get("Err_Generic"));
                return false;
            }
        }

        private async Task LoadCategories(int? selected)
        {
            var categories = await _db.DishCategories.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", selected);
        }

        private IActionResult NotFoundView(string message)
        {
            Response.StatusCode = StatusCodes.Status404NotFound;
            ViewData["Message"] = message;
            return View("NotFound");
        }
    }
}
