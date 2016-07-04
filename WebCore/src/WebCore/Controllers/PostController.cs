using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebCore.Infra;
using WebCore.Model;

namespace EFCore1.Controllers
{
    public class PostController : Controller
    {
        BlogContext context;

        public PostController(BlogContext context)
        {
            this.context = context;
        }

        public ActionResult Index(string message = "")
        {
            var models = this.context.Posts.ToList();

            if (!String.IsNullOrEmpty(message))
                ViewBag.Success = message;

            return View(models);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post model)
        {
            if (!ModelState.IsValid)
                return View(model);

            context.Posts.Add(model);
            context.SaveChanges();

            return RedirectToAction("Index", new { message = "Post criado com sucesso!" });

        }

        public ActionResult Edit(int id)
        {
            var post = this.context.Posts.Where(x => x.Id == id).FirstOrDefault();
            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(Post model)
        {
            if (!ModelState.IsValid)
                return View(model);

            context.Posts.Add(model);
            context.Entry<Post>(model).State = EntityState.Modified;
            context.SaveChanges();

            return RedirectToAction("Index", new { message = "Post editado com sucesso!" });

        }

        public ActionResult Delete(int id)
        {
            var post = this.context.Posts.Where(x => x.Id == id).FirstOrDefault();

            this.context.Posts.Remove(post);
            this.context.SaveChanges();

            return RedirectToAction("Index", new { message = "Post excluído com sucesso" });
        }

    }
}