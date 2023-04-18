using CodeFirstApproach_LNE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList.Mvc;
using PagedList;
using System.Web.UI.WebControls.Expressions;

namespace CodeFirstApproach_LNE.Controllers
{
    public class HomeController : Controller
    {
        StudentContext db = new StudentContext();

        // GET: Home
        public ActionResult Index(string search, int? i )
        {
            //List<Student> stu = db.Students.ToList();
            // var str = db.Students.Where(x => x.Name.StartsWith(search) || x.Gender.StartsWith(search)  || search == null ).ToList().ToPagedList(i ?? 1, 3);

            var str = db.Students
           .Where(x => x.Name.StartsWith(search) || x.Gender.StartsWith(search) || search == null)
           .OrderBy(x => x.Name) 
           .ToList()
           .ToPagedList(i ?? 1, 3);




            return View(str);

        }


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student s)
        {
            if (ModelState.IsValid == true)
            {



                db.Students.Add(s);
                int a = db.SaveChanges();

                if (a > 0)
                {
                    /*   ViewBag.InsertMessage = "<script>alert ('Data Inserted !!')</script>";*/
                    /*
                                        TempData["InsertMessage"] = "<script>alert ('Data Inserted !!')</script>";*/


                    TempData["InsertMessage"] = "Data Inserted !!";

                    return RedirectToAction("Index");
                    /*           ModelState.Clear();*/

                }
                else
                {
                    ViewBag.InsertMessage = "<script>alert ('Data not Inserted !!')</script>";

                }
            }
            return View();
        }

        public ActionResult Edit(int Id)
        {
            var row = db.Students.Where(x => x.Id == Id).FirstOrDefault();
            return View(row);
        }

        [HttpPost]

        public ActionResult Edit(Student student)
        {

            if (ModelState.IsValid == true)
            {



                db.Entry(student).State = EntityState.Modified;

                int a = db.SaveChanges();
                if (a > 0)
                {
                    /* ViewBag.UpdateMessage = "<script>alert ('Data updated !!')</script>";*/

                    /*  TempData["UpdateMessage"] = "<script>alert ('Data updated !!')</script>";*/

                    TempData["UpdateMessage"] = "Data Updated !!";

                    /*  ModelState.Clear();*/
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.UpdateMessage = "<script>alert ('Data not updated !!')</script>";
                }
            }
            return View();
        }

        public ActionResult Delete(int Id)
        {

            var studentrow = db.Students.Where(x => x.Id == Id).FirstOrDefault();

            return View(studentrow);
        }
        [HttpPost]
        public ActionResult Delete(Student student)
        {
            db.Entry(student).State = EntityState.Deleted;
            int a = db.SaveChanges();
            if (a > 0)
            {
                TempData["DeleteMessage"] = "<script>alert ('Data Deleted !!')</script>";
            }
            else
            {
                ViewBag.DeleteMessage = "<script>alert ('Data  not Deleted !!')</script>";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Details(int Id)
        {
            var DetailsById = db.Students.Where(x => x.Id == Id).FirstOrDefault();

            return View(DetailsById);
        }


    }
}