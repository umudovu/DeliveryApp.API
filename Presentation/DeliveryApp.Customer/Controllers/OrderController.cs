using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.Customer.Extentions;
using DeliveryApp.Customer.Models;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistence.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly ISmsService _smsService;
        public OrderController(UserManager<AppUser> userManager, ICompanyService companyService,
                            ICustomerService customerService, AppDbContext context, IOrderService orderService, ISmsService smsService)
        {
            _userManager = userManager;
            _companyService = companyService;
            _customerService = customerService;
            _context = context;
            _orderService = orderService;
            _smsService = smsService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("checkout");
        }

        public async Task<IActionResult> MyOrder()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "error");
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var customer = _customerService.GetCustomer(user.Id);
            var orders = await _orderService.GetAllAsync();

            orders = orders.Where(x => x.CustomerId == customer.Id).OrderByDescending(x=>x.CreatedDate);

            return View(orders.ToList());
        }

        public IActionResult Track()
        {
            return RedirectToAction("checkout");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return RedirectToAction("empty", "error");
            var order = await _orderService.GetOrderByIdAsync(id);
            return View(order);
        }

        public async Task<IActionResult> Checkout()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "error");

            var isBasket = Request.Cookies["basket"];
            if(isBasket==null) return RedirectToAction("empty", "error");
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(isBasket);

            if (basket.Count == 0) return RedirectToAction("empty", "error");


            return View();
        }


        public async Task<IActionResult> Confirm(string phoneNumber,string latitude,string longitude, string address)
       
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var customer = _customerService.GetCustomer(user.Id);

            List<Company> companies = new();
            List<Order> orders = new();
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);

            var dbCompanies = _companyService.GetAllCompany();

            foreach (var b in basket)
            {
                var product = _context.Products.Find(b.Id);

                var company = await _companyService.GetCompanyByIdAsync(product.CompanyId);

                if(companies.FirstOrDefault(x=>x.Id==company.Id)==null)
                {
                    companies.Add(company);
                    dbCompanies = dbCompanies.Where(x => x.Id == company.Id);
                }

            }
            foreach (var c in companies)
            {
                Order order = new()
                {
                    FirstName = customer.Name,
                    SurName = customer.SurName,
                    PhoneNumber = phoneNumber,
                    InvoiceNo = RandomNumber.RandomString(7),
                    TrackingNo = RandomNumber.RandomStringAll(11),
                    Address = address,
                    LatCoord = latitude,
                    LngCoord = longitude,
                    Email = user.Email,
                    OrderStatus = OrderStatus.Unpaid,
                    CreatedDate = DateTime.Now,
                    CustomerId = customer.Id,
                    CompanyId=c.Id
                };
                customer.Address = address;
                orders.Add(order);
            }

            

            _context.Orders.AddRange(orders);
            _context.SaveChanges();

            List<OrderItem> orderItems = new();

            List<int> orderIds = new();

            foreach (var o in orders)
            {
                foreach (BasketVM pr in basket)
                {
                    var dbProduct = await _context.Products.Include(x=>x.Company).FirstOrDefaultAsync(x=>x.Id== pr.Id);
                    if (dbProduct.CompanyId==o.CompanyId)
                    {
                        OrderItem orderItem = new()
                        {
                            ProductId = pr.Id,
                            ProductCount = pr.BasketCount,
                            Price = dbProduct.Price,
                            Total = pr.BasketCount * dbProduct.Price,
                            OrderId = o.Id,
                        };
                        dbProduct.SellerCount += pr.BasketCount;
                        o.TotalPrice += pr.Price * pr.BasketCount;
                        orderItems.Add(orderItem);
                    }
                    
                }
                orderIds.Add(o.Id);
            }
           

            
            _context.OrderItems.AddRange(orderItems);
            _context.SaveChanges();
            //bool isOk= _orderService.CreateOrder(order, orderItems);

            Response.Cookies.Append("oi", JsonConvert.SerializeObject(orderIds));

            return Ok();
        }

       

        
        public async Task<IActionResult> Cash(bool isOrder,List<int> orderIds)
        {
            string phoneNumber="";


            if (isOrder == false) return RedirectToAction("invalid", "error");

            List<Order> orders = new();
            foreach (var oi in orderIds)
            {
                var order = await _context.Orders.Include(x => x.Company).FirstOrDefaultAsync(x => x.Id == oi);
                order.Company.Balance += order.TotalPrice;

                phoneNumber = order.PhoneNumber;
                orders.Add(order);
            }

            foreach (var o in orders)
            {
                o.OrderStatus = OrderStatus.Processing;
               
            }

            await _smsService.Send(phoneNumber, "Your order has been successfully received");

            _context.SaveChanges();

            Response.Cookies.Delete("basket");

            

            return RedirectToAction("successful", "info");
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Pay()
        {
            List<int> orderId = JsonConvert.DeserializeObject<List<int>>(Request.Cookies["oi"]);

            Response.Cookies.Delete("oi");

            return RedirectToAction("Cash", new {isOrder=true,orderIds=orderId});

        }

    }
}
