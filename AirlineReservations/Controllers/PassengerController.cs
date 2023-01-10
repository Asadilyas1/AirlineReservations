using AirlineReservations.Areas.Identity.Data;
using AirlineReservations.Models;
using AirlineReservations.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;

namespace AirlineReservations.Controllers
{

   
    public class PassengerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public PassengerController(ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _context=context;
            _userManager=userManager;
        }
        public IActionResult Index()
        {
            var getDate = (from airline in _context.airlines
                           join tick in _context.Tickets on airline.Id equals tick.AirlineID
                           join booking in _context.bookingClass on tick.ClassTypeID equals booking.Id
                           join route in _context.routes on airline.Id equals route.AirlineId
                           select new PassangerViewModel
                           {
                               AirlineName=airline.AirlineName,
                               AirlineClassType=booking.ClassTpye,
                               AirlineCityRouteFrom=route.RouteFromCity,
                               AirlineCityRouteTo=route.RouteToCity,
                               TotalSeat=tick.TotalSeata,
                               AirlineCode=airline.Airlinecode,
                               TicketPrice=tick.SinglePrice,
                               TicketFrom=tick.TicketFrom,
                               TicketTo=tick.TicletTo,
                               TicketTimeFrom=tick.TimeFrom,
                               TicketTimeTo=tick.TimeTo,
                               TicketID=tick.Id
                           }).ToList();


            return View(getDate);
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

        public IActionResult BookingTicket(int?id)
        {
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
                booking.userId = userId;
                booking.TicketPrice = Price;
                _context.Add(booking);

                int checkConfirmation=  _context.SaveChanges();

                var user =  await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await _userManager.SetPhoneNumberAsync(user, PhoneNumber);

                    
                    user.Adress = Adress;
                    user.City = City;
                    user.Name = Name;
              
                    await _userManager.UpdateAsync(user);
                }


                if (checkConfirmation == 1)
                {
                    var getTicketSeats = _context.Tickets.Find(id);

                    getTicketSeats.TotalSeata = getTicketSeats.TotalSeata - Seat;
                    
                    _context.Update(getTicketSeats);
                    _context.SaveChanges();
                }
                else
                {
                    return View();
                }

            }
            else
            {
                booking.From = getdata.TicketFrom;
                booking.To = getdata.TicletTo;
                booking.ReturnTime = getdata.TimeTo;
                booking.DepartureTime = getdata.TimeFrom;
                booking.TicketsId = id;
                booking.Seats = Seat;
                booking.userId = userId;
                booking.TicketPrice = Price;
                _context.Add(booking);
                int chek= _context.SaveChanges();


                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    await _userManager.SetPhoneNumberAsync(user, PhoneNumber);


                    user.Adress = Adress;
                    user.City = City;
                    user.Name = Name;

                    await _userManager.UpdateAsync(user);
                }

                if (chek == 1)
                {
                    var getTicketSeats = _context.Tickets.Find(id);

                    getTicketSeats.TotalSeata = getTicketSeats.TotalSeata - Seat;

                    _context.Update(getTicketSeats);
                    _context.SaveChanges();

                }
                else
                {
                    return View();
                }


            }

            return View();
        }
    }
}
