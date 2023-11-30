using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.App.Services;

namespace SalesWebMvc.App.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SimpleSearch(DateTime minDate, DateTime maxDate)
        {

            var list = _salesRecordService.FindByDate(minDate, maxDate);
            return View(list);
        }

        public IActionResult GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

            var list = _salesRecordService.GroupingSearch(minDate, maxDate);
            return View(list);
        }
    }
}
