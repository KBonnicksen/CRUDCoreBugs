﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRUDWithIssuesCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWithIssuesCore.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Student> students = StudentDb.GetStudents(context);
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student p)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Add(p, context);
                ViewData["Message"] = $"{p.Name} was added!";
                return RedirectToAction("Index");
            }

            //Show web page with errors
            return View(p);
        }

        public IActionResult Edit(int id)
        {
            //get the product by id
            Student p = StudentDb.GetStudent(context, id); 

            //show it on web page
            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(context, p);
                ViewData["Message"] = "Product Updated!";
                return View(p);
            }
            //return view with errors
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Student p = StudentDb.GetStudent(context, id);
            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            //Get Product from database
            Student p = StudentDb.GetStudent(context, id);

            StudentDb.Delete(context, p);

            return RedirectToAction("Index");
        }
    }
}