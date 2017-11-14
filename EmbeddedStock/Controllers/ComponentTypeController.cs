using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using EmbeddedStock.Data;
using EmbeddedStock.Models;
using EmbeddedStock.ViewModels;

namespace EmbeddedStock.Controllers
{
    public class ComponentTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComponentTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComponentType
        public IActionResult Index()
        {
            var componentTypeVMList = new List<ComponentTypeViewModel>();

            foreach(var componentType in _context.ComponentTypes.ToList())
            {
                var categoryIds = _context.CategoryComponentType.Where(x => x.ComponentTypeId == componentType.ComponentTypeId).Select(i => i.CategoryId).ToList();

                var categories = new List<string>();
                foreach (var categoryId in categoryIds)
                    categories.Add(_context.Categories.Where(x => x.CategoryId == categoryId).Select(x => x.Name).FirstOrDefault());

                var componentTypeVM = new ComponentTypeViewModel()
                {
                    ComponentType = componentType,
                    SelectedCategories = categories
                };

                componentTypeVMList.Add(componentTypeVM);
            }

            return View(componentTypeVMList);
        }

        // GET: ComponentType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .SingleOrDefaultAsync(m => m.ComponentTypeId == id);

            if (componentType == null)
            {
                return NotFound();
            }

            var categoryIds = _context.CategoryComponentType.Where(x => x.ComponentTypeId == componentType.ComponentTypeId).Select(i => i.CategoryId).ToList();

            var categories = new List<string>();
            foreach (var categoryId in categoryIds)
            {
                var category = _context.Categories.Where(x => x.CategoryId == id).Select(x => x.Name).FirstOrDefault();

                if(category != null)
                    categories.Add(category);
            }

            var componentTypeVM = new ComponentTypeViewModel()
            {
                ComponentType = componentType,
                SelectedCategories = categories
            };

            return View(componentTypeVM);
        }

        // GET: ComponentType/Create
        public IActionResult Create()
        {
            var componentTypeVM = new ComponentTypeViewModel();

            componentTypeVM.Categories = _context.Categories.ToList().Select(
            cat => new SelectListItem {
                Text = cat.Name,
                Value = cat.CategoryId.ToString()
            }).ToList();

            return View(componentTypeVM);
        }

        // POST: ComponentType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComponentTypeViewModel componentTypeVM)
        {
            if (!ModelState.IsValid)
                return View(componentTypeVM);
            
            if (componentTypeVM.SelectedCategories != null)
            {
                var selectedCategories = _context.Categories
                .Where(x => componentTypeVM.SelectedCategories
                       .Contains(x.CategoryId.ToString()));

                var cctList = new List<CategoryComponentType>();

                foreach (var selectedCategory in selectedCategories)
                {
                    var cct = new CategoryComponentType
                    {
                        CategoryId = selectedCategory.CategoryId,
                        ComponentType = componentTypeVM.ComponentType
                    };

                    cctList.Add(cct);
                }

                componentTypeVM.ComponentType.CategoryComponentTypes = cctList;
            }

            _context.Add(componentTypeVM.ComponentType);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: ComponentType/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .SingleOrDefaultAsync(m => m.ComponentTypeId == id);

            if (componentType == null)
            {
                return NotFound();
            }

            var categoryIds = _context.CategoryComponentType.Where(x => x.ComponentTypeId == componentType.ComponentTypeId).Select(i => i.CategoryId).ToList();

            var categories = new List<string>();
            foreach (var categoryId in categoryIds)
            {
                var category = _context.Categories.Where(x => x.CategoryId == id).Select(x => x.Name).FirstOrDefault();

                if (category != null)
                    categories.Add(category);
            }

            var componentTypeVM = new ComponentTypeViewModel()
            {
                ComponentType = componentType,
                SelectedCategories = categories,
                Categories = _context.Categories.ToList().Select(
                    cat => new SelectListItem
                    {
                        Text = cat.Name,
                        Value = cat.CategoryId.ToString()
                    }).ToList()
            };

            return View(componentTypeVM);
        }

        // POST: ComponentType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ComponentTypeViewModel componentTypeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(componentTypeVM);
            }

            var id = componentTypeVM.ComponentType.ComponentTypeId;

            try
            {
                _context.CategoryComponentType.RemoveRange(
                        _context.CategoryComponentType.Where(x => x.ComponentTypeId == id));

                if(componentTypeVM.SelectedCategories != null)
                {
                    var selectedCategories = _context.Categories
                    .Where(x => componentTypeVM.SelectedCategories
                           .Contains(x.CategoryId.ToString()));

                    var cctList = new List<CategoryComponentType>();

                    foreach (var selectedCategory in selectedCategories)
                    {
                        var cct = new CategoryComponentType
                        {
                            CategoryId = selectedCategory.CategoryId,
                            ComponentType = componentTypeVM.ComponentType
                        };

                        cctList.Add(cct);
                    }

                    componentTypeVM.ComponentType.CategoryComponentTypes = cctList;
                }

                _context.Update(componentTypeVM.ComponentType);

                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ComponentType/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var componentType = await _context.ComponentTypes
                .SingleOrDefaultAsync(m => m.ComponentTypeId == id);

            if (componentType == null)
            {
                return NotFound();
            }

            var categoryIds = _context.CategoryComponentType.Where(x => x.ComponentTypeId == componentType.ComponentTypeId).Select(i => i.CategoryId).ToList();

            var categories = new List<string>();
            foreach (var categoryId in categoryIds)
            {
                var category = _context.Categories.Where(x => x.CategoryId == id).Select(x => x.Name).FirstOrDefault();

                if (category != null)
                    categories.Add(category);
            }

            var componentTypeVM = new ComponentTypeViewModel()
            {
                ComponentType = componentType,
                SelectedCategories = categories
            };

            return View(componentTypeVM);
        }

        // POST: ComponentType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var componentType = await _context.ComponentTypes.SingleOrDefaultAsync(m => m.ComponentTypeId == id);
            _context.ComponentTypes.Remove(componentType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComponentTypeExists(long id)
        {
            return _context.ComponentTypes.Any(e => e.ComponentTypeId == id);
        }
    }
}