using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc_Project.Models;
using Mvc_Project.Utility;
using System.Data;

namespace Mvc_Project.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]

    public class KitapTuruController : Controller
    {
        private readonly IKitapTuruRepository _kitapTuruRepository;

        public KitapTuruController(IKitapTuruRepository kitapTuruRepository)
        {
            _kitapTuruRepository=kitapTuruRepository;
        }
        public IActionResult Index()
        {
            List<KitapTuru> objKitapTuruList= _kitapTuruRepository.GetAll().ToList();    
            return View(objKitapTuruList);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(KitapTuru kitapTuru)
        {
            //_context.KitapTurleri.Add(kitapTuru);
            //_context.SaveChanges();
            //return RedirectToAction("Index");

            //kontrolü section js ile sağladık buna gerek kalmadı
            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Add(kitapTuru);
                _kitapTuruRepository.Save();
                TempData["success"] ="Yeni Kitap Türü Başarıyla Oluşturuldu";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int? id)
        {
            if(id==null || id==0 )
            { 
                return NotFound();
            }
            KitapTuru? kitapTuruVt = _kitapTuruRepository.Get(p=>p.Id==id);
            if(kitapTuruVt==null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);
        }
        [HttpPost]
        public IActionResult Update(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                _kitapTuruRepository.Update(kitapTuru);
                _kitapTuruRepository.Save();
                TempData["success"] = "Kitap Türü Başarıyla Güncellendi";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            KitapTuru? kitapTuruVt = _kitapTuruRepository.Get(p=>p.Id==id);
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            return View(kitapTuruVt);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            KitapTuru? kitapTuruVt = _kitapTuruRepository.Get(p => p.Id == id);
            if (kitapTuruVt == null)
            {
                return NotFound();
            }
            _kitapTuruRepository.Delete(kitapTuruVt);
            _kitapTuruRepository.Save();
            TempData["success"] = "Kitap Türü Başarıyla Silindi";
            return RedirectToAction("Index");
        }
    }
}
