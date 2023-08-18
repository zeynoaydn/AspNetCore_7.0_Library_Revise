using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc_Project.Models;
using Mvc_Project.Utility;

namespace Mvc_Project.Controllers
{
    //[Authorize(Roles = UserRoles.Role_Admin)]
    //kitap işlemlerine sadece admin girebilirrrr
    public class KitapController : Controller
    {
        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;

        public readonly IWebHostEnvironment _webHostEnvironment;

        public KitapController(IKitapRepository kitapRepository,IKitapTuruRepository kitapTuruRepository,IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Admin,Ogrenci")]
        public IActionResult Index()
        {
            //List<Kitap> objKitapList = _kitapRepository.GetAll().ToList();
            List<Kitap> objKitapList = _kitapRepository.GetAll(includeProps:"KitapTuru").ToList();
            return View(objKitapList);
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult AddUpdate(int? id)
        {
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll()
                .Select(p => new SelectListItem
                {
                    Text = p.Ad,
                    Value = p.Id.ToString(),
                });
            ViewBag.KitapTuruList = KitapTuruList;

            if(id==null || id == 0)
            {
                //ekleme
                return View();
            }
            else
            {
                //güncelleme
                Kitap? kitapVt = _kitapRepository.Get(p => p.Id == id);
                if (kitapVt == null)
                {
                    return NotFound();
                }
                return View(kitapVt);

            }
            //return View();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult AddUpdate(Kitap kitap,IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"image");

                using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                kitap.ResimUrl = @"\image" + file.FileName;

                if (kitap.Id == 0)
                {
                    _kitapRepository.Add(kitap);
                    TempData["success"] = "Yeni Kitap Başarıyla Oluşturuldu";
                }
                else
                {
                    _kitapRepository.Update(kitap);
                    TempData["success"] = "Kitap Başarıyla Güncellendi";
                }

                _kitapRepository.Add(kitap);
                _kitapRepository.Save();
                TempData["success"] = "Yeni Kitap Başarıyla Oluşturuldu";
                return RedirectToAction("Index");
            }
            return View();
        }

        //public IActionResult Update(int? id)
        //{
        //    IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll()
        //        .Select(p => new SelectListItem
        //        {
        //            Text = p.Ad,
        //            Value = p.Id.ToString(),
        //        });
        //    ViewBag.KitapTuruList = KitapTuruList;
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    //Kitap? kitapVt = _kitapRepository.Get(p => p.Id == id);
        //    //if (kitapVt == null)
        //    //{
        //    //    return NotFound();
        //    //}
        //    //return View(kitapVt);
        //}
        //[HttpPost]
        //public IActionResult Update(Kitap kitap)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _kitapRepository.Update(kitap);
        //        _kitapRepository.Save();
        //        TempData["success"] = "Kitap Başarıyla Güncellendi";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kitap? kitapVt = _kitapRepository.Get(p => p.Id == id);
            if (kitapVt == null)
            {
                return NotFound();
            }
            return View(kitapVt);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult DeletePost(int? id)
        {
            Kitap? kitapVt = _kitapRepository.Get(p => p.Id == id);
            if (kitapVt == null)
            {
                return NotFound();
            }
            _kitapRepository.Delete(kitapVt);
            _kitapRepository.Save();
            TempData["success"] = "Kitap Başarıyla Silindi";
            return RedirectToAction("Index");
        }
    }
}
