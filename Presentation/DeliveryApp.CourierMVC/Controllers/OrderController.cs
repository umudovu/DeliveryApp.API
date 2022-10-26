using DeliveryApp.Application.Abstractions.Services;
using DeliveryApp.CourierMVC.Models;
using DeliveryApp.Domain.Entities;
using DeliveryApp.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.CourierMVC.Controllers
{
    public class OrderController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly IOrderService _orderService;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(AppDbContext context, IOrderService orderService, UserManager<AppUser> userManager)
        {
            _context = context;
            _orderService = orderService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllAsync();
            orders = orders.Where(x => x.CourierId == null);
            orders = orders.Where(x => x.OrderStatus == OrderStatus.Processing);


            return View(orders.ToList());
        }

        public async Task<IActionResult> Detail(int id)
        {
            var order =await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound("Order not found");

            return View(order);
        }

        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null) return NotFound("not found");
            try
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var courier = _context.Couriers.FirstOrDefault(x => x.AppUserId == user.Id);
                if (courier.ActiveOrderId != null) return BadRequest("you have an active order");
                courier.IsActive = true;
                courier.ActiveOrderId = id;

                var order = await _orderService.GetOrderByIdAsync(id);
                order.ShippedStatus = ShippedStatus.Curyer_appointed;
                _context.SaveChanges();

                var result = await _orderService.AddCourierAsync(id,courier.Id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return RedirectToAction("active","order");
        }

        public async Task<IActionResult> Active()
        {
            OrderReturnVM returnVM = new();
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var courier = _context.Couriers.FirstOrDefault(x => x.AppUserId == user.Id);

            if (courier.ActiveOrderId == null) return RedirectToAction("index", "order");

            returnVM.Order = await _orderService.GetOrderByIdAsync(courier.ActiveOrderId);
            if (returnVM.Order == null) return NotFound("Order not found");
            returnVM.DestLat = returnVM.Order.Company.LatCoord;
            returnVM.DestLng = returnVM.Order.Company.LngCoord;

            if (returnVM.Order.ShippedStatus == ShippedStatus.The_order_is_in_the_courier)
            {
                returnVM.DestLat = returnVM.Order.LatCoord;
                returnVM.DestLng = returnVM.Order.LngCoord;
            }

            return View(returnVM);
        }

        public async Task<IActionResult> StatusChange(ShippedStatus shippedStatus)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var courier = _context.Couriers.FirstOrDefault(x => x.AppUserId == user.Id);

            if (courier.ActiveOrderId == null) return RedirectToAction("index", "order");

            var order = await _orderService.GetOrderByIdAsync(courier.ActiveOrderId);

            order.ShippedStatus = shippedStatus;
            if (shippedStatus == ShippedStatus.The_order_has_been_delivered)
            {
                await _orderService.CompleteOrder(order.Id, courier.Id);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("active","order");
        }
    }
}
