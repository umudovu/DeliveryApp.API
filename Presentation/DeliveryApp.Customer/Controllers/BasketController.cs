using DeliveryApp.Customer.Models;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeliveryApp.Customer.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddItem(int? id)
        {
            if (id == null) return NoContent();

            string basket = Request.Cookies["basket"];

            Product product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) return NoContent();



            List<BasketVM> products;

            if (basket == null)
            {
                products = new List<BasketVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
            }

            BasketVM isexsist = products.Find(p => p.Id == id);

            if (isexsist == null)
            {
                BasketVM basketVM = new BasketVM
                {
                    Id = product.Id,
                    BasketCount = 1,
                    Price = product.Price,
                    Name = product.Name,
                };
                products.Add(basketVM);
            }
            else
            {
                isexsist.BasketCount++;
            }


            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));

            double subtotal = 0;
            int basketCount = 0;

            if (products.Count > 0)
            {
                foreach (BasketVM pr in products)
                {
                    subtotal += pr.Price * pr.BasketCount;
                    basketCount += pr.BasketCount;
                }

                SumVM sumVM = new()
                {
                    SubTotal = subtotal,
                    BasketCount = basketCount
                };

                Response.Cookies.Append("sum", JsonConvert.SerializeObject(sumVM));
            }

            return Ok();
        }


        [HttpGet]
        public IActionResult ItemMinus(int? id)
        {
            string basket = Request.Cookies["basket"];

            double subtotal = 0;
            int basketCount = 0;

            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            BasketVM product = products.FirstOrDefault(p => p.Id == id);



            if (product == null) return NotFound();

            if (product.BasketCount > 1)
            {
                product.BasketCount--;

                Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
            }
            else
            {

                products.Remove(product);

                
                products = products.FindAll(p => p.Id != id);

                Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));
                SumVM sumVM = new();
                foreach (BasketVM pr in products)
                {
                    subtotal += pr.Price * product.BasketCount;
                    basketCount += pr.BasketCount;


                    sumVM.SubTotal = subtotal;
                    sumVM.BasketCount = basketCount;
                    
                    
                }
                Response.Cookies.Append("sum", JsonConvert.SerializeObject(sumVM));

                return Ok();
            }


            foreach (BasketVM pr in products)
            {
                subtotal += pr.Price * product.BasketCount;
                basketCount += pr.BasketCount;

                SumVM sumVM = new()
                {
                    SubTotal = subtotal,
                    BasketCount = basketCount
                };
                Response.Cookies.Append("sum", JsonConvert.SerializeObject(sumVM));
            }
           
            return Ok();
        }


        [HttpGet]
        public IActionResult ItemRemove(int? id)
        {
            string basket = Request.Cookies["basket"];

            List<BasketVM> products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);

            if (products == null) return NotFound();

           products = products.FindAll(p => p.Id != id);


            Response.Cookies.Append("basket", JsonConvert.SerializeObject(products));

            double subtotal = 0;
            int basketCount = 0;

            SumVM sumVM = new()
            {
                SubTotal = subtotal,
                BasketCount = basketCount
            };
            Response.Cookies.Append("sum", JsonConvert.SerializeObject(sumVM));

            if (products.Count > 0)
            {
                foreach (BasketVM pr in products)
                {
                    subtotal += pr.Price * pr.BasketCount;
                    basketCount += pr.BasketCount;


                    sumVM.SubTotal = subtotal;
                    sumVM.BasketCount = basketCount;
                    
                    Response.Cookies.Append("sum", JsonConvert.SerializeObject(sumVM));
                }
            }

            var obj = new
            {
                SubTotal = subtotal,
                BasketCount = basketCount,
                Products=products,
            };
            return Ok(obj);
        }


    }
}
