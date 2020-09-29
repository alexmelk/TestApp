using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Newtonsoft.Json;
using TestApp.Context;
using TestApp.Models;
using TestApp.Models.ServerDbContextModels;

namespace TestApp.Controllers
{
    public class SurveyController : Controller
    {
        private AppDbContext _appDbContext;
        private ServerDbContext _serverDbContext;
        public SurveyController(AppDbContext appDbContext, ServerDbContext serverDbContext)
        {
            _appDbContext = appDbContext;
            _serverDbContext = serverDbContext;
        }
        public IActionResult Index()
        {
            var first = _appDbContext.Questions.FirstOrDefault();

            var model = new SurveyViewModel() {
                Position = 0,
                Questions = _appDbContext.Questions.ToList(),
                Answer = new Answer(),
            };
            return View(model);
        }


        public IActionResult NextQuestion(SurveyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var saved = HttpContext.Session.GetString("Survey");
                List<Answer> list = default;
                if (saved != null)
                {
                    list = JsonConvert.DeserializeObject<List<Answer>>(saved);
                    list.Add(model.Answer);
                    var text = JsonConvert.SerializeObject(list);
                    HttpContext.Session.SetString("Survey", text);
                }
                else
                {
                    var create = new List<Answer> { model.Answer };
                    var text = JsonConvert.SerializeObject(create);
                    HttpContext.Session.SetString("Survey", text);
                }
                if(list.Count == _appDbContext.Questions.ToList().Count)
                {
                    var survey = new Survey() { Date = DateTime.Now };

                    _serverDbContext.Surveys.Add(survey);
                    _serverDbContext.SaveChanges();

                }
                else
                {
                    model.Position++;
                    model.Questions = _appDbContext.Questions.ToList();
                    model.Answer = new Answer();
                }
            }
            model.Questions = _appDbContext.Questions.ToList();
            return View("Index", model);
        }
        public IActionResult PrevQuestion(int position)
        {
            var saved = HttpContext.Session.GetString("Survey");
            if (saved != null)
            {
                var list = JsonConvert.DeserializeObject<List<Answer>>(saved);
                var el = list[position-1];

                var model = new SurveyViewModel() { 
                    Answer = el,
                    Position = --position,
                    Questions = _appDbContext.Questions.ToList() };

                return View("Index", model);
            }

            return View("Index");
        }
    }
}
