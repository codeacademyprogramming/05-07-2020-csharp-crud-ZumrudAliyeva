using AspNet_MVC_Crud.Contexts;
using AspNet_MVC_Crud.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace AspNet_MVC_Crud.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        ShopContext db = new ShopContext();
        //public ActionResult Index()
        //{
        //    var shopModel = new ShopModel()
        //    {
        //        Products = db.Products.ToList()
        //    };
        //    return View(shopModel.Products);
        //}

        //SEARCH PRODUCT BY NAME
        public ActionResult Index(string searching)
        {
            var shopModel = new ShopModel()
            {
                Products = db.Products.ToList()
            };
            var products = db.Products.ToList();
            
            if (!string.IsNullOrEmpty(searching))
            {
               var filteredProducts = products.Where(p => p.Name.ToLower().Contains(searching.ToLower())).ToList();
                products = filteredProducts;
            }

            return View(products);
        }


        // CREATE NEW PRODUCT
        public ActionResult Create()
        {
            return View();
        }



    [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //ACTION TO CANCEL
        public ActionResult Cancel()
        {
            return RedirectToAction("Index");
        }

        public ActionResult GetProduct(int id)
        {
            var product = db.Products.FirstOrDefault(p => p.Id == id);

            return View(product);
        }

        //EDIT SELECTED PRODUCT
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(404);
            }
            var product = db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return new HttpStatusCodeResult(400);
            }
            else
            {
                db.SaveChanges();
            }

            return View(product);
        }


        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {

                var product_ = db.Products.FirstOrDefault(p => p.Id == product.Id);

                if (product_ == null)
                {
                    return new HttpStatusCodeResult(404);

                }
                product_ = product;

                db.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            return new HttpStatusCodeResult(400);
        }


        //DELETE SELECTED PRODUCT
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(400);
            }
            var product = db.Products.FirstOrDefault(p => p.Id == id);

            return View(product);
        }


        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Product product = db.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return new HttpStatusCodeResult(404);
            }
            else
            {
                db.Products.Remove(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }

}