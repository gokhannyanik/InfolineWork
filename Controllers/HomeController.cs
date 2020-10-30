using InfolineWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;

namespace InfolineWork.Controllers
{
    
    public class HomeController : Controller
    {
        public InfolineWorkEntities db = new InfolineWorkEntities();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PersonsSave(IEnumerable<PersonModel> result)
        {

            foreach (var item in result)
            {
                tblPerson newPerson = new tblPerson
                {
                    Name = item.AdSoyad,
                    Phone = item.Telefon,
                    Email = item.Email,
                    PersonPhoto = item.PersonPhoto
                };
                db.tblPerson.Add(newPerson);
            }
            try
            {
                db.SaveChanges();
                return Json("Kayıt İşlemi Gerçekleştirildi");
            }
            catch (Exception ex)
            {
                return Json("Inner Exception: " + ex.Message);
            }
        }

        public ActionResult KullaniciList()
        {
            List<tblPerson> KullaniciList = db.tblPerson.ToList();

            return View(KullaniciList);
        }

        [HttpPost]
        public JsonResult PersonsDelete(IEnumerable<PersonModel> result)
        {
            foreach (var item in result)
            {
                var DeletePerson = db.tblPerson.Where(w => w.PersonId == item.PersonId).FirstOrDefault();
                db.tblPerson.Remove(DeletePerson);

                var DeletePersonAnswer = db.tblAnswer.Where(w => w.PersonId == item.PersonId).FirstOrDefault();
                if (DeletePersonAnswer != null)
                {
                    db.tblAnswer.Remove(DeletePersonAnswer);
                }
            }
            try
            {
                db.SaveChanges();
                return Json("Kayıt İşlemi Gerçekleştirildi");
            }
            catch (Exception ex)
            {

                return Json("Inner Exception: " + ex.Message);
            }
        }

        public ActionResult SoruKullanici()
        {
            var randomQuestion = db.tblQuestion.OrderBy(u => Guid.NewGuid()).FirstOrDefault();
            var randomPerson = db.tblPerson.OrderBy(u => Guid.NewGuid()).FirstOrDefault();
            PersonQuestion newPerson = new PersonQuestion
            {
                PersonId = randomPerson.PersonId,
                Name = randomPerson.Name,
                PersonPhoto = randomPerson.PersonPhoto,
                QuestionId = randomQuestion.QuestionId,
                Question = randomQuestion.Question
            };

            return View(newPerson);
        }

        [HttpPost]
        public JsonResult SoruKullanici(tblAnswer result)
        {
            tblAnswer newAnswer = new tblAnswer
            {
                PersonId = result.PersonId,
                QuestionId = result.QuestionId,
                Answer = result.Answer,
                AnswerDate = DateTime.Now
            };
            db.tblAnswer.Add(newAnswer);
            db.SaveChanges();

            return Json("kaydedildi");
        }

        public ActionResult KullaniciCevapList()
        {

            var CevapList = (from ans in db.tblAnswer
                join ques in db.tblQuestion on ans.QuestionId equals ques.QuestionId
                join prs in db.tblPerson on ans.PersonId equals prs.PersonId
                select new PersonAnswer { Name = prs.Name, Photo = prs.PersonPhoto, Question = ques.Question, Answer = ans.Answer, AnswerDate = ans.AnswerDate }).ToList();

            return View(CevapList);
        }

    }
}