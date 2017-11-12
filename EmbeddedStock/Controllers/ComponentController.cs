using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using EmbeddedStock.Data;
using EmbeddedStock.Models;
using EmbeddedStock.Models.Enums;

namespace EmbeddedStock.Controllers
{
    public class ComponentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComponentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var components = _context.Components
                .Include(x => x.ComponentType)
                .ToListAsync();
            return View(await components);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null) return NotFound();

            var component = await _context.Components
            .SingleOrDefaultAsync(m => m.ComponentId == id);

            if (component == null) return NotFound();

            return View(component);
        }

        /*
        public IActionResult Create()
        {
            var model = new ComponentViewModel();

            var types = _context.Types.ToList();
            model.Types = types.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComponentViewModel componentViewModel)
        {
            if (!ModelState.IsValid) return View(componentViewModel);

            var type = _context.Types.FirstOrDefault(x => x.Id == int.Parse(componentViewModel.TypeId));
            componentViewModel.Component.Type = type;
            componentViewModel.Component.TypeId = (int)type.Id;

            _context.Add(componentViewModel.Component);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null) return NotFound();

            var component = await _context.Components.SingleOrDefaultAsync(m => m.Id == id);
            var model = new ComponentViewModel();
            var types = _context.Types.ToList();

            model.TypeId = component.TypeId.ToString();
            model.Types = types.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            model.Component = component;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id)
        {
            var editComponent = CreateComponentFromRequestBody(Request.Form, id);

            if (!ModelState.IsValid)
            {
                return View(new ComponentViewModel
                {
                    Component = editComponent,
                    TypeId = editComponent.TypeId.ToString(),
                    Types = _context.Types.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList()
                });
            }
            try
            {
                var type = _context.Types.FirstOrDefault(x => x.Id == editComponent.TypeId);
                editComponent.Type = type;
                _context.Update(editComponent);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComponentExists(editComponent.Id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        private static Component CreateComponentFromRequestBody(IFormCollection requestForm, long id)
        {
            var component = new Component { Id = id };

            if (requestForm.TryGetValue("Component.Number", out var number)) component.Number = int.Parse(number);
            if (requestForm.TryGetValue("Component.SerialNo", out var serialNumber)) component.SerialNo = serialNumber;
            if (requestForm.TryGetValue("Component.AdminComment", out var adminComment)) component.AdminComment = adminComment;
            if (requestForm.TryGetValue("Component.UserComment", out var userComment)) component.UserComment = userComment;
            if (requestForm.TryGetValue("TypeId", out var typeId)) component.TypeId = int.Parse(typeId);
            if (requestForm.TryGetValue("Component.Status", out var status))
            {
                if (Enum.TryParse(status, out ComponentStatus statusEnum)) component.Status = statusEnum;
            }
            if (requestForm.TryGetValue("Component.CurrentLoanInformationId", out var currentLoanInformationId))
            {
                if (StringValues.IsNullOrEmpty(currentLoanInformationId)) component.CurrentLoanInformationId = null;
                else component.CurrentLoanInformationId = long.Parse(currentLoanInformationId);
            }

            return component;
        }*/

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null) return NotFound();

            var component = await _context.Components
                .SingleOrDefaultAsync(m => m.ComponentId == id);

            if (component == null) return NotFound();
            return View(component);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var component = await _context.Components.SingleOrDefaultAsync(m => m.ComponentId == id);

            _context.Components.Remove(component);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ComponentExists(long id)
        {
            return _context.Components.Any(e => e.ComponentId == id);
        }
    }
}