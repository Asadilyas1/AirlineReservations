using AirlineReservations.Areas.Identity.Data;
using AirlineReservations.Models;
using AirlineReservations.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace AirlineReservations.Controllers
{

    [Authorize(Roles ="Customer")]
    public class PassengerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public PassengerController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context=context;
            _userManager=userManager;
        }
        public IActionResult Airline()
        {
            var ShowAirline = _context.airlines.ToList();
            return View(ShowAirline);
        }

        public IActionResult CityAirlineRoute(int id)
        {
            var GetRouteDetils = _context.routes.Where(x => x.AirlineId == id).ToList();
            
            return View(GetRouteDetils);
        }
        
       
        public IActionResult BookingTicket(int?id,int Rid)
        {
            TempData["ID"] =  Rid;

            var MatchAirline= (from ticket in _context.Tickets 
                               where ticket.AirlineID == id select new TicketViewModel
                               {
                                   TicketFrom=ticket.TicketFrom,
                                   TicketTo=ticket.TicletTo,
                                   TimeFrom=ticket.TimeFrom,
                                   TimeTo=ticket.TimeTo,
                                   TotalTicket=ticket.TotalSeata,
                                   SingleWayPrice=ticket.SinglePrice,
                                   TwoWayPrice=ticket.TwoWaysPrice,
                                   TicketId=ticket.Id,
                                   airlineName=ticket.Airline.AirlineName,
                                   AirlineCode=ticket.Airline.Airlinecode,
                                   PassangerClass=ticket.bookingClass.ClassTpye
                                }).ToList();
            return View(MatchAirline);
        }

        public IActionResult ConfirmTicket(int id)
        {
            var getTicketInfo = (from ticket in _context.Tickets
                            join city in _context.routes on ticket.AirlineID equals city.AirlineId
                            where ticket.Id == id
                            select new ConfirmViewModel
                            {
                                TicketFrom=ticket.TicketFrom,
                                TicketTo=ticket.TicletTo,
                                singlePrice=ticket.SinglePrice,
                                TwoWayPrice=ticket.TwoWaysPrice,
                                TimeFrom=ticket.TimeFrom,
                                TimeTo=ticket.TimeTo,
                                PassangerClass=ticket.bookingClass.ClassTpye,
                                ticketid=ticket.Id,
                                RouteFromCity=city.RouteFromCity,
                                RoutetoCity=city.RouteToCity

                            }).FirstOrDefault();
            return View(getTicketInfo);
        }

        [HttpPost]
        public async Task<IActionResult> UserConfirmTicket(int Price, int Seat,int id, string PhoneNumber,string Name,string Adress, string City)
        {
           
            BookingDetails booking = new BookingDetails();

           

            string userId = _userManager.GetUserId(User);

            var getdata = _context.Tickets.Where(x => x.Id == id).FirstOrDefault();

            bool check = _context.Tickets.Select(x=>x.SinglePrice==Price).FirstOrDefault();

            if (check == true)
            {
                booking.From = getdata.TicketFrom;
                booking.To = getdata.TicletTo;
                booking.ReturnTime = getdata.TimeTo;
                booking.DepartureTime = getdata.TimeFrom;
                booking.TicketsId = id;
                booking.Seats = Seat;
                booking.TicketStatus = "Pending";
                booking.userId = userId;
                booking.TicketPrice = Price;
               
                booking.RouteId = Convert.ToInt32(TempData["ID"]);

                _context.Add(booking);
               
               int SaveCheck=  _context.SaveChanges();

                if(SaveCheck==1)
                {
                    TempData["ID"] = null;
                }
                    var user =  await _userManager.FindByIdAsync(userId);
                    await _userManager.SetPhoneNumberAsync(user, PhoneNumber);
                    user.Adress = Adress;
                    user.City = City;
                    user.Name = Name;
                    await _userManager.UpdateAsync(user);
                    var getTicketSeats = _context.Tickets.Find(id);

                    getTicketSeats.TotalSeata = getTicketSeats.TotalSeata - Seat;
                    
                    _context.Update(getTicketSeats);
                    _context.SaveChanges();

                    return RedirectToAction("TicketConfirmation");
            }
           
            else
            {
                booking.From = getdata.TicketFrom;
                booking.To = getdata.TicletTo;
                booking.ReturnTime = getdata.TimeTo;
                booking.DepartureTime = getdata.TimeFrom;
                booking.TicketsId = id;
                booking.Seats = Seat;
                booking.TicketStatus = "Pending";
                booking.userId = userId;
                booking.TicketPrice = Price;
                booking.RouteId = Convert.ToInt32(TempData["ID"]);
                _context.Add(booking);
               int SavedCheck=  _context.SaveChanges();
                if(SavedCheck==1)
                {
                    TempData["ID"] = null;
                }
                var user = await _userManager.FindByIdAsync(userId);
                
                    await _userManager.SetPhoneNumberAsync(user, PhoneNumber);
                    user.Adress = Adress;
                    user.City = City;
                    user.Name = Name;
                    await _userManager.UpdateAsync(user);
                    var getTicketSeats = _context.Tickets.Find(id);
                    getTicketSeats.TotalSeata = getTicketSeats.TotalSeata - Seat;
                    _context.Update(getTicketSeats);
                    _context.SaveChanges();

                    return RedirectToAction("TicketConfirmation");
            }

           
        }

        public IActionResult TicketConfirmation()
        {
            return View();
        }

        public IActionResult UserHistory()
        {
            string userId = _userManager.GetUserId(User);


            var getOrder = (from user in _context.Users
                            join bokingDetail in _context.bookingDetails on user.Id equals bokingDetail.userId
                            join tick in _context.Tickets on bokingDetail.TicketsId equals tick.Id
                            join airline in _context.airlines on tick.AirlineID equals airline.Id
                            where bokingDetail.userId == userId
                            select new PassangerViewModel
                            {
                                PassengerName=user.Name,
                                AirlineName=airline.AirlineName,
                                TicketFrom=tick.TicketFrom,
                                TicketPrice=bokingDetail.TicketPrice,
                                TicketTimeTo=bokingDetail.ReturnTime,
                                TicketTo=tick.TicletTo,
                                AirlineClassType=tick.bookingClass.ClassTpye,
                                BookingDetialsId = bokingDetail.BookingID,
                                Status = bokingDetail.TicketStatus
                            }).ToList();


           
            return View(getOrder);
        }

        public IActionResult CancelOrder(int id)
        {
            var getOrderDetails=_context.bookingDetails.Find(id);
            if (getOrderDetails == null)
            {
                return NotFound();
            }
            else
            {
                getOrderDetails.TicketStatus = "Cancel";
                _context.bookingDetails.Update(getOrderDetails);
                _context.SaveChanges();
                return RedirectToAction("UserHistory");
            }

           
        }
    }
}
