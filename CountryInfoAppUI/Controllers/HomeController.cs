using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CountryInfoAppUI.Controllers
{
    public class HomeController : Controller
    {
        private IUserBL _logic;
        private IRegionBL _regionLogic;

        public HomeController(IUserBL logic, IRegionBL regionLogic)
        {
            _logic = logic;
            _regionLogic = regionLogic;
        }
        //Возвращает представление стартовой страницы
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Возвращает представление с информацией о стране с названием, указанном в поле ввода.
        /// В случае отсутствия такой страны вернется соответствующие сообщение
        /// </summary>
        /// <param name="countryName">Название страны</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GetCountryInfo(string countryName)
        {
            try
            {
                var country = await _logic.GetCountryInfo(countryName);
                ControllerContext.HttpContext.Session["currentCountry"] = country;
                return View("CountryViewWithSaveFunc", country);
            }
            catch (HttpRequestException)
            {
                ViewBag.Request = countryName;
                return View("NotFoundView");
            }
            catch (Exception)
            {
                return View("ErrorView");
            }
        }

        /// <summary>
        /// Возвращает представление, которое содержит информацию о странах, сохраненных в бд
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetAllCountriesInfos()
        {
            var countries = await _logic.GetCountryInfosFromDb();
            return View("CountriesListView", countries);
        }
        /// <summary>
        /// Возвращает представление, содержащее диалоговое окно с подтверждением операции сохранения
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveCountyInfos()
        {
            return PartialView("SaveCountyInfosModalView");
        }
        /// <summary>
        /// Сохраняет информацию о стране
        /// </summary>
        /// <returns></returns>
        [HttpPost, ActionName("SaveCountyInfos")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveCountyInfosConfirmed()
        {
            var countryInfo = ControllerContext.HttpContext.Session["currentCountry"] as CountryInfoDTO;
            await _logic.SaveCountyInfo(countryInfo);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _regionLogic.Dispose();
                _logic.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}