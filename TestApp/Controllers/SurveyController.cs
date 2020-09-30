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

            var model = new SurveyViewModel()
            {
                Position = 0,
                Questions = _appDbContext.Questions.ToList(),
                Answer = new Answer(),
            };
            return View(model);
        }
        /// <summary>
        /// Кнопка следующий вопрос
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult NextQuestion(SurveyViewModel model)
        {
            if (ModelState.IsValid)
            {
                SaveAnswerToSession(model.Answer, model.Position);

                var savedList = GetSavedAnswerList();
                var questionsList = _appDbContext.Questions.ToList();

                if (savedList?.Count() == questionsList.Count())
                {
                    SendSurvey(savedList);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.Position = model.Position + 1;
                    model.Questions = questionsList;
                    model.Answer = GetSavedAnswer(model.Position);
                }
            }
            model.Questions = _appDbContext.Questions.ToList();
            return View("Index", model);
        }

        public void SaveAnswerToSession(Answer answer, int position)
        {
            var saved = HttpContext.Session.GetString("Survey");
            if (saved != null)
            {
                var list = JsonConvert.DeserializeObject<List<Answer>>(saved);
                if (list.Count() == position) //если не нужно обновить ответ
                {
                    list.Add(answer);
                    var text = JsonConvert.SerializeObject(list);
                    HttpContext.Session.SetString("Survey", text);
                }
                else
                {
                    list[position] = answer;
                    var text = JsonConvert.SerializeObject(list);
                    HttpContext.Session.SetString("Survey", text);
                }
            }
            else
            {
                var create = new List<Answer> { answer };
                var text = JsonConvert.SerializeObject(create);
                HttpContext.Session.SetString("Survey", text);
            }
        }
        public Answer GetSavedAnswer(int position)
        {
            var saved = HttpContext.Session.GetString("Survey");
            var list = JsonConvert.DeserializeObject<List<Answer>>(saved);

            if (position == list.Count())
            {
                return new Answer();
            }
            else
            {
                return list[position];
            }

        }
        public List<Answer> GetSavedAnswerList()
        {
            var saved = HttpContext.Session.GetString("Survey");
            var list = JsonConvert.DeserializeObject<List<Answer>>(saved);
            return list;
        }
        public void SendSurvey(List<Answer> list)
        {
            var survey = new Survey() { Date = DateTime.Now };

            var questions = _appDbContext.Questions.ToList();
            int counter = 0;
            foreach (var el in questions)
            {
                list[counter].Survey = survey;
                list[counter].QuestionId = el.Id;
                _serverDbContext.Answers.Add(list[counter]);
                counter++;
            }
            _serverDbContext.SaveChanges();
            HttpContext.Session.Clear();
        }
        /// <summary>
        /// Кнопка предыдущий вопрос
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public IActionResult PrevQuestion(int position)
        {

            var model = new SurveyViewModel()
            {
                Answer = GetSavedAnswer(position - 1),
                Position = position - 1,
                Questions = _appDbContext.Questions.ToList()
            };

            return View("Index", model);
        }
    }
}
