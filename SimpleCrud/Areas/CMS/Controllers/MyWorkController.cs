using CMS.Entities;
using CMS.Repo.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SimpleCrud.Areas.CMS.ViewModels;
using SimpleCrud.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace SimpleCrud.Areas.CMS.Controllers
{
    [Area("cms")]
    [Route("{cms}/mywork")]
    [Authorize]
    public class MyWorkController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly MyWorkRepository myWorkRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public MyWorkController(IUnitOfWork unitOfWork, MyWorkRepository myWorkRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.myWorkRepository = myWorkRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            var myWork = myWorkRepository.Queryable().Where(a => !a.IsDeleted).ToList();
            MyWorkViewModel viewModel = new MyWorkViewModel();
            foreach (var item in myWork)
            {
                WorkDetails work = new WorkDetails();
                work.Id = item.Id;
                work.Description = item.Description;
                work.Title = item.Title;
                work.Image = item.Image;
                work.CreatedDate = item.Created_date;
                viewModel.workDetails.Add(work);
            }
            return View(viewModel);
        }

        [HttpGet]
        [Route("create")]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(WorkDetails work)
        {
            if (ModelState.IsValid)
            {
                var imagePath = UploadedFile(work);
                MyWork wrk = new MyWork();
                wrk.Id = work.Id;
                wrk.Title = work.Title;
                wrk.Description = work.Description;
                wrk.Image = imagePath;
                wrk.Created_date = work.CreatedDate;
                unitOfWork.GetRepository<MyWork>().Insert(wrk);
                return RedirectToAction(nameof(Index));
            }
            return View(work);
        }

        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var work = myWorkRepository.GetById(id);
            WorkDetails wrk = new WorkDetails();
            wrk.Id = work.Id;
            wrk.Title = work.Title;
            wrk.Description = work.Description;
            wrk.Image = work.Image;
            wrk.CreatedDate = work.Created_date;
            if (work == null)
            {
                return NotFound();
            }
            return View(wrk);
        }

        [HttpPost]
        [Route("editpost")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(WorkDetails work)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    MyWork wrk = myWorkRepository.GetById(work.Id);
                    if (work.ProfileImage != null)
                    {
                        DeleteImage(wrk.Image);
                        var imagePath = UploadedFile(work);
                        wrk.Image = imagePath;
                    }

                    wrk.Id = work.Id;
                    wrk.Title = work.Title;
                    wrk.Description = work.Description;
                    wrk.Created_date = work.CreatedDate;

                    unitOfWork.GetRepository<MyWork>().Update(wrk);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View(work);
        }

        [HttpGet]
        [Route("delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            try
            {
                MyWork wrk = myWorkRepository.GetById(Id);
                wrk.IsDeleted = true;
                unitOfWork.GetRepository<MyWork>().Update(wrk);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string UploadedFile(WorkDetails model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images/Custom");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        void DeleteImage(string path)
        {
            if (path != null)
            {
                string folder= Path.Combine(webHostEnvironment.WebRootPath, "Images/Custom");
                string filepath = Path.Combine(folder, path);
                System.IO.File.Delete(filepath);
            }
        }
    }
}