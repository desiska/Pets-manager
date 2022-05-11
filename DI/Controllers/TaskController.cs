using DI.Data;
using DI.Models.Task;
using EntityFrameworkPaginate;
using Korzh.EasyQuery.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DI.Controllers
{
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext db;

        public TaskController()
        {
            db = new ApplicationDbContext();
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Data.Entities.Task> tasks = db.Tasks.ToList();
            List<TaskVM> items = new List<TaskVM>();

            tasks.ForEach(t => items.Add(new TaskVM()
            {
                ID = t.ID,
                Name = t.Name,
                Description = t.Description,
                LimitTime = t.TimeLimit,
                Budget = t.Budget,
                Status = db.Statuses.Find(t.StatusID).Name,
                Location = db.Locations.Find(t.LocationID).Name,
                Category = db.Categories.Find(t.CategoryID).Text,
                Statuses = db.Statuses.ToList()
            }));

            tasks = new List<Data.Entities.Task>();

            TaskIndexVM model = new TaskIndexVM()
            {
                Items = items,
                Text = "",
                SearchedItem = tasks.AsQueryable()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult Index(TaskIndexVM model)
        {
            if (!string.IsNullOrEmpty(model.Text))
            {
                model.SearchedItem = db.Tasks.FullTextSearchQuery(model.Text);
                /*foreach (var item in model.SearchedItem)
                {
                    string status = db.Statuses.Find(item.StatusID).Name;
                    string location = db.Locations.Find(item.LocationID).Name;
                    string category = db.Categories.Find(item.CategoryID).Text;
                    item.CategoryID = category;
                    item.LocationID = location;
                    item.StatusID = status;
                }*/
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            TaskCreateVM model = new TaskCreateVM()
            {
                Locations = db.Locations.ToList(),
                Categories = db.Categories.ToList(),
                Location = db.Locations.FirstOrDefault().Name,
                Category = db.Categories.FirstOrDefault().Text
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskCreateVM model)
        {
            if (ModelState.IsValid)
            {
                Data.Entities.Task task = new Data.Entities.Task()
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = model.Name,
                    Description = model.Description,
                    TimeLimit = model.LimitTime,
                    Budget = model.Budget,
                    StatusID = db.Statuses.Where(s => s.Name == "Waiting").FirstOrDefault().ID,
                    LocationID = model.Location,
                    CategoryID = model.Category,
                    Photo = ""
                };

                db.Tasks.Add(task);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Data.Entities.Task task = db.Tasks.Find(id);

            if(task == null)
            {
                return NotFound();
            }

            TaskEditVM model = new TaskEditVM()
            {
                ID = task.ID,
                Name = task.Name,
                Description = task.Description,
                LimitTime = task.TimeLimit,
                Budget = task.Budget,
                Status = db.Statuses.Find(task.StatusID).Name,
                Statuses = db.Statuses.ToList(),
                Location = task.LocationID,
                Locations = db.Locations.ToList(),
                Category = task.CategoryID,
                Categories = db.Categories.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TaskEditVM model)
        {
            if (ModelState.IsValid)
            {
                Data.Entities.Task task = db.Tasks.Find(model.ID);
                task.Name = model.Name;
                task.Description = model.Description;
                task.TimeLimit = model.LimitTime;
                task.Budget = model.Budget;
                task.StatusID = model.Status;
                task.LocationID = model.Location;
                task.CategoryID = model.Category;

                db.Tasks.Update(task);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Data.Entities.Task task = db.Tasks.Find(id);

            if(task == null)
            {
                return NotFound();
            }

            db.Tasks.Remove(task);
            db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ChangeStatus(string? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Data.Entities.Task task = db.Tasks.Find(id);

            ChangeStatusTaskVM model = new ChangeStatusTaskVM()
            {
                ID = task.ID,
                Status = db.Statuses.Find(task.StatusID).Name,
                Statuses = db.Statuses.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(ChangeStatusTaskVM model)
        {
            Data.Entities.Task task = db.Tasks.Find(model.ID);

            task.StatusID = model.Status;
            task.Photo = SaveFile(model.Photo);

            db.Tasks.Update(task);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private static string SaveFile(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var extension = fileName.Split('.').Last();
            var fileNameWithoutExtension = string.Join("", fileName.Split('.').Take(fileName.Length - 1));

            var newfileName = "wwwroot/images/" + String.Format("{0}-{1:ddMMYYYYHHmmss}.{2}",
                fileNameWithoutExtension,
                DateTime.Now,
                extension
            );

            if (!Directory.Exists(Path.GetDirectoryName(newfileName)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(newfileName));
            }

            using (var localFile = System.IO.File.OpenWrite(newfileName))
            {
                using (var uploadedFile = file.OpenReadStream())
                {
                    uploadedFile.CopyTo(localFile);
                }
            }

            return newfileName;
        }

        /*Only Code Here. There is not code for this in Models and Views!!!*/
        public Page<Data.Entities.Task> GetFilteredTasks(int pageSize, int currentPage, string searchText, int sortBy)
        {
            Page<Data.Entities.Task> tasks;

            var filters = new Filters<Data.Entities.Task>();
            filters.Add(!string.IsNullOrEmpty(searchText), x => x.Name.Contains(searchText));

            var sorts = new Sorts<Data.Entities.Task>();
            sorts.Add(sortBy == 1, x => x.LocationID);
            sorts.Add(sortBy == 2, x => x.Name);
            sorts.Add(sortBy == 3, x => x.TimeLimit);

            tasks = db.Tasks.Paginate(currentPage, pageSize, sorts, filters);

            return tasks;
        }
    }
}