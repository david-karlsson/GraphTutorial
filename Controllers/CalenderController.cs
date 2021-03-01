// using GraphTutorial.Models;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using Microsoft.Identity.Web;
// using Microsoft.Graph;
// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using TimeZoneConverter;

// namespace GraphTutorial.Controllers
// {
//     public class CalendarController : Controller
//     {
//         private readonly GraphServiceClient _graphClient;
//         private readonly ILogger<HomeController> _logger;

//         public CalendarController(
//             GraphServiceClient graphClient,
//             ILogger<HomeController> logger)
//         {
//             _graphClient = graphClient;
//             _logger = logger;
//         }

//                 private async Task<IList<Event>> GetUserWeekCalendar(DateTime startOfWeekUtc)
//                 {
//                     // Configure a calendar view for the current week
//                     var endOfWeekUtc = startOfWeekUtc.AddDays(7);

//                     var viewOptions = new List<QueryOption>
//             {
//                 new QueryOption("startDateTime", startOfWeekUtc.ToString("o")),
//                 new QueryOption("endDateTime", endOfWeekUtc.ToString("o"))
//             };

//             var events = await _graphClient.Me
//                 .CalendarView
//                 .Request(viewOptions)
//                 // Send user time zone in request so date/time in
//                 // response will be in preferred time zone
//                 .Header("Prefer", $"outlook.timezone=\"{User.GetUserGraphTimeZone()}\"")
//                 // Get max 50 per request
//                 .Top(50)
//                 // Only return fields app will use
//                 .Select(e => new
//                 {
//                     e.Subject,
//                     e.Organizer,
//                     e.Start,
//                     e.End
//                 })
//                 // Order results chronologically
//                 .OrderBy("start/dateTime")
//                 .GetAsync();

//             IList<Event> allEvents;
//             // Handle case where there are more than 50
//             if (events.NextPageRequest != null)
//             {
//                 allEvents = new List<Event>();
//                 // Create a page iterator to iterate over subsequent pages
//                 // of results. Build a list from the results
//                 var pageIterator = PageIterator<Event>.CreatePageIterator(
//                     _graphClient, events,
//                     (e) =>
//                     {
//                         allEvents.Add(e);
//                         return true;
//                     }
//                 );
//                 await pageIterator.IterateAsync();
//             }
//             else
//             {
//                 // If only one page, just use the result
//                 allEvents = events.CurrentPage;
//             }

//             return allEvents;
//         }

//         private static DateTime GetUtcStartOfWeekInTimeZone(DateTime today, TimeZoneInfo timeZone)
//         {
//             // Assumes Sunday as first day of week
//             int diff = System.DayOfWeek.Sunday - today.DayOfWeek;

//             // create date as unspecified kind
//             var unspecifiedStart = DateTime.SpecifyKind(today.AddDays(diff), DateTimeKind.Unspecified);

//             // convert to UTC
//             return TimeZoneInfo.ConvertTimeToUtc(unspecifiedStart, timeZone);
//         }


//         // Minimum permission scope needed for this view
// [AuthorizeForScopes(Scopes = new[] { "Calendars.Read" })]
// public async Task<IActionResult> Index()
// {
//     try
//     {
//         var userTimeZone = TZConvert.GetTimeZoneInfo(
//             User.GetUserGraphTimeZone());
//         var startOfWeek = CalendarController.GetUtcStartOfWeekInTimeZone(
//             DateTime.Today, userTimeZone);

//         var events = await GetUserWeekCalendar(startOfWeek);

//         // Return a JSON dump of events
//         return new ContentResult {
//             Content = _graphClient.HttpProvider.Serializer.SerializeObject(events),
//             ContentType = "application/json"
//         };
//     }
//     catch (ServiceException ex)
//     {
//         if (ex.InnerException is MicrosoftIdentityWebChallengeUserException)
//         {
//             throw;
//         }

//         return new ContentResult {
//             Content = $"Error getting calendar view: {ex.Message}",
//             ContentType = "text/plain"
//         };
//     }
// }


//     }













    
// }