using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.CourierMVC.Controllers
{
    [Authorize(Roles ="Admin,Courier")]
    public class BaseController : Controller
    {
       
    }
}
