using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //Index Method to get all CategoryName,SubCategoryName,ItemName
        //if null or get Names with searched itemName
        public ActionResult Index(string search,int? page)
        {

            using (ShoppingEntities db = new ShoppingEntities())
            {
                List<Category> category = db.Categories.ToList();
                List<SubCategory> subcategory = db.SubCategories.ToList();
                List<Item> item = db.Items.ToList();
                
                    var GetItems = from c in category
                                   join s in subcategory on c.CategoryId equals s.CategoryId into table1
                                   from s in table1.ToList()
                                   join i in item on s.SubCategoryID equals i.SubCategoryID into table2
                                   from i in table2.ToList()
                                   select new ViewModel
                                   {
                                       category = c,
                                       subcategory = s,
                                       item = i
                                   };
                if (search == "")
                {
                    return View(GetItems.ToPagedList(page ?? 1, 2));
                }
                else
                {
                    return View(GetItems.Where(i => i.item.Name == search).ToPagedList(page ?? 1, 2));
                }
                
               
            }
           
        }
    }
}