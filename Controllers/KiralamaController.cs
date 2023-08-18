using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc_Project.Models;
using Mvc_Project.Utility;
using System.Data;

namespace Mvc_Project.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]

    public class KiralamaController : Controller
    {
        private readonly IKiralamaRepository _kiralamaRepository;
        private readonly IKitapRepository _kitapRepository;

        public readonly IWebHostEnvironment _webHostEnvironment;

        public KiralamaController(IKiralamaRepository kiralamaRepository,IKitapRepository kitapRepository,IWebHostEnvironment webHostEnvironment)
        {
            _kiralamaRepository = kiralamaRepository;
            _kitapRepository = kitapRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(includeProps:"Kitap").ToList();
            return View(objKiralamaList);
        }
        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
                .Select(p => new SelectListItem
                {
                    Text = p.KitapAdi,
                    Value = p.Id.ToString(),
                });
            ViewBag.KitapList = KitapList;

            if(id==null || id == 0)
            {
                //ekleme
                return View();
            }
            else
            {
                //güncelleme
                Kiralama? kiralamaVt = _kiralamaRepository.Get(p => p.Id == id);
                if (kiralamaVt == null)
                {
                    return NotFound();
                }
                return View(kiralamaVt);

            }
            //return View();
        }
        [HttpPost]

        public IActionResult AddUpdate(Kiralama kiralama)
        {
            if (ModelState.IsValid)
            {
                if (kiralama.Id == 0)
                {
                    _kiralamaRepository.Add(kiralama);
                    TempData["success"] = "Yeni Kiralama İşlemi Başarıyla Oluşturuldu";
                }
                else
                {
                    _kiralamaRepository.Update(kiralama);
                    TempData["success"] = "Kiralama Kayıt Başarıyla Güncellendi";
                }

                _kiralamaRepository.Save();
                return RedirectToAction("Index","Kiralama");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
                .Select(p => new SelectListItem
                {
                    Text = p.KitapAdi,
                    Value = p.Id.ToString(),
                });
            ViewBag.KitapList = KitapList;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kiralama? kiralamaVt = _kiralamaRepository.Get(p => p.Id == id);
            if (kiralamaVt == null)
            {
                return NotFound();
            }
            return View(kiralamaVt);
        }
        [HttpPost, ActionName("Delete")]

        public IActionResult DeletePost(int? id)
        {
            Kiralama? kiralamaVt = _kiralamaRepository.Get(p => p.Id == id);
            if (kiralamaVt == null)
            {
                return NotFound();
            }
            _kiralamaRepository.Delete(kiralamaVt);
            _kiralamaRepository.Save();
            TempData["success"] = "Kiralama Başarıyla Silindi";
            return RedirectToAction("Index","Kiralama");
        }
    }
}
