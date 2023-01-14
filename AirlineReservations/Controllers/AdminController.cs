using AirlineReservations.Areas.Identity.Data;
using AirlineReservations.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace AirlineReservations.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManger;

        public AdminController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context=context;
            _userManger=userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AllOrder()
        {

            var GetOrder = (from bookingdetails in _context.bookingDetails
                       join user in _context.Users on bookingdetails.userId equals user.Id
                       join tick in _context.Tickets on bookingdetails.TicketsId equals tick.Id
                       join airline in _context.airlines on tick.AirlineID equals airline.Id
                       join route in _context.routes on bookingdetails.RouteId equals route.RouteId

                       select new UserOrderViewModel
                        {
                           AirlineName=airline.AirlineName,
                           CityFrom=route.RouteFromCity,
                           cityTo=route.RouteToCity,
                           phoneNumber=user.PhoneNumber,
                           UserName=user.UserName,
                           Status=bookingdetails.TicketStatus,
                           TicketPrice=bookingdetails.TicketPrice,
                           TicketFrom=tick.TicketFrom,
                           TicketTo=tick.TicletTo,
                           OrderId=bookingdetails.BookingID,
                        }).ToList();



            return View(GetOrder);
        }

        public IActionResult CancelOrder()
        {
            var GetOrder = (from bookingdetails in _context.bookingDetails
                            join user in _context.Users on bookingdetails.userId equals user.Id
                            join tick in _context.Tickets on bookingdetails.TicketsId equals tick.Id
                            join airline in _context.airlines on tick.AirlineID equals airline.Id
                            join route in _context.routes on bookingdetails.RouteId equals route.RouteId
                            where bookingdetails.TicketStatus == "Cancel"
                            select new UserOrderViewModel
                            {
                                AirlineName = airline.AirlineName,
                                CityFrom = route.RouteFromCity,
                                cityTo = route.RouteToCity,
                                phoneNumber = user.PhoneNumber,
                                UserName = user.UserName,
                                Status = bookingdetails.TicketStatus,
                                TicketPrice = bookingdetails.TicketPrice,
                                TicketFrom = tick.TicketFrom,
                                TicketTo = tick.TicletTo,
                                OrderId = bookingdetails.BookingID,
                            }).ToList();

            return View(GetOrder);
        }


        public IActionResult PendingOrder()
        {

            var GetOrder = (from bookingdetails in _context.bookingDetails
                            join user in _context.Users on bookingdetails.userId equals user.Id
                            join tick in _context.Tickets on bookingdetails.TicketsId equals tick.Id
                            join airline in _context.airlines on tick.AirlineID equals airline.Id
                            join route in _context.routes on bookingdetails.RouteId equals route.RouteId
                            where bookingdetails.TicketStatus == "Pending"
                            select new UserOrderViewModel
                            {
                                AirlineName = airline.AirlineName,
                                CityFrom = route.RouteFromCity,
                                cityTo = route.RouteToCity,
                                phoneNumber = user.PhoneNumber,
                                UserName = user.UserName,
                                Status = bookingdetails.TicketStatus,
                                TicketPrice = bookingdetails.TicketPrice,
                                TicketFrom = tick.TicketFrom,
                                TicketTo = tick.TicletTo,
                                OrderId = bookingdetails.BookingID,
                            }).ToList();

            return View(GetOrder);
        }

        public IActionResult ConfirmOrder()
        {
            var GetOrder = (from bookingdetails in _context.bookingDetails
                            join user in _context.Users on bookingdetails.userId equals user.Id
                            join tick in _context.Tickets on bookingdetails.TicketsId equals tick.Id
                            join airline in _context.airlines on tick.AirlineID equals airline.Id
                            join route in _context.routes on bookingdetails.RouteId equals route.RouteId
                            where bookingdetails.TicketStatus == "Confirm"
                            select new UserOrderViewModel
                            {
                                AirlineName = airline.AirlineName,
                                CityFrom = route.RouteFromCity,
                                cityTo = route.RouteToCity,
                                phoneNumber = user.PhoneNumber,
                                UserName = user.UserName,
                                Status = bookingdetails.TicketStatus,
                                TicketPrice = bookingdetails.TicketPrice,
                                TicketFrom = tick.TicketFrom,
                                TicketTo = tick.TicletTo,
                                OrderId = bookingdetails.BookingID,
                            }).ToList();

            return View(GetOrder);
        }
        public IActionResult ConfirmTicket(int id)
        {
            var getOrderDetails = _context.bookingDetails.Find(id);
            if (getOrderDetails == null)
            {
                return NotFound();
            }
            else
            {
                getOrderDetails.TicketStatus = "Confirm";
                _context.bookingDetails.Update(getOrderDetails);
                _context.SaveChanges();
                return RedirectToAction("AllOrder");
            }
        }
       
    }
}
