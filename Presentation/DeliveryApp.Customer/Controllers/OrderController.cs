using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Application.DTOs;
using DeliveryApp.Customer.Extentions;
using DeliveryApp.Customer.Models;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistence.Context;
using DeliveryApp.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DeliveryApp.Customer.Controllers
{
   
    public class OrderController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICompanyService _companyService;
        private readonly ICustomerService _customerService;
        private readonly AppDbContext _context;
        private readonly IOrderService _orderService;
        public OrderController(UserManager<AppUser> userManager, ICompanyService companyService,
                            ICustomerService customerService, AppDbContext context, IOrderService orderService)
        {
            _userManager = userManager;
            _companyService = companyService;
            _customerService = customerService;
            _context = context;
            _orderService = orderService;
        }   

        public IActionResult Index()
        {
            return RedirectToAction("checkout");
        }

        public IActionResult Checkout()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("register", "error");

            var isBasket = Request.Cookies["basket"];
            if(isBasket==null) return RedirectToAction("empty", "error");
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(isBasket);

            if (basket.Count == 0) return RedirectToAction("empty", "error");

            return View();
        }


        public async Task<IActionResult> Confirm(string phoneNumber,string latitude,string longitude)
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var customer = _customerService.GetCustomer(user.Id);

            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);

            Order order = new()
            {
                UserId = user.Id,
                FirstName = customer.Name,
                SurName = customer.SurName,
                PhoneNumber = phoneNumber,
                InvoiceNo = RandomNumber.RandomString(7),
                TrackingNo = RandomNumber.RandomStringAll(11),
                LatCoord=latitude,
                LngCoord=longitude,
                Email=user.Email,
                OrderStatus = OrderStatus.Unpaid
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
            List<OrderItem> orderItems = new();

            foreach (BasketVM pr in basket)
            {
                var dbProduct = await _context.Products.FindAsync(pr.Id);
                OrderItem orderItem = new()
                {
                    ProductId=pr.Id,
                    ProductCount=pr.BasketCount,
                    Price =dbProduct.Price,
                    Total = pr.BasketCount * dbProduct.Price,
                    OrderId = order.Id

                };
                order.TotalPrice += pr.Price * pr.BasketCount;
                orderItems.Add(orderItem);
            }
            _context.OrderItems.AddRange(orderItems);
            _context.SaveChanges();
            //bool isOk= _orderService.CreateOrder(order, orderItems);

            Response.Cookies.Append("oi", JsonConvert.SerializeObject(order.Id));

            return Ok();
        }

       

        
        public async Task<IActionResult> Cash(bool isOrder,int orderId)
        {
            if (isOrder == false) return RedirectToAction("invalid", "error");

            var order = _context.Orders.FirstOrDefault(x=>x.Id==orderId);
            if (order == null) return BadRequest("Order not found");

            order.OrderStatus = OrderStatus.Processing;

            Response.Cookies.Delete("basket");

            return RedirectToAction("index","home");
        }



        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Pay()
        {
            int orderId = JsonConvert.DeserializeObject<int>(Request.Cookies["oi"]);

            Response.Cookies.Delete("oi");

            return RedirectToAction("Cash", new {isOrder=true,orderId=orderId});

        }

    }
}
