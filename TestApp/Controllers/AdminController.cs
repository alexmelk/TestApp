using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.Context;
using TestApp.Models;
using TestApp.Models.AppDbContextModels;

namespace TestApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private AppDbContext _appDbContext;
        private ServerDbContext _serverDbContext;
        public AdminController(AppDbContext appDbContext, ServerDbContext serverDbContext)
        {
            _appDbContext = appDbContext;
            _serverDbContext = serverDbContext;
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public IActionResult Index()
        {
            var model = new AdminViewModel
            {
                Users = _serverDbContext.Users.ToList(),
                Questions = _appDbContext.Questions.ToList(),
            };
            return View(model);
        }
        /// <summary>
        /// Отобразить окно изменения добавления вопроса
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult EditQuestion(int id)
        {
            var model = _appDbContext.Questions.Find(id);
            if (model != null)
            {
                ViewData["Title"] = "Редактировать вопрос";
            }
            else
            {
                ViewData["Title"] = "Добавить вопрос";
                model = new Question();
            }
            return View(model);

        }
        /// <summary>
        /// Изменить/создать вопрос
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <param name="fillCard"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EditQuestion(Question fillForm)
        {
            if (ModelState.IsValid)
            {
                //проверяем нужно ли обновить данные
                var question = _appDbContext.Questions.FirstOrDefault(x => x.Id == fillForm.Id);

                //вопрос уже есть в бд (обновляем)
                if (question != null)
                {
                    question = fillForm;
                    _appDbContext.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    _appDbContext.Questions.Add(fillForm);
                    _appDbContext.SaveChanges();
                }
            }
            else
            {
                return View(fillForm);
            }

            return RedirectToAction("Index");
        }
        /// <summary>
        /// Удалить вопрос
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult RemoveQuestion(int id)
        {
            _appDbContext.Questions.Remove(_appDbContext.Questions.Find(id));
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Questions()
        {
            var model = new AdminViewModel
            {
                Users = _serverDbContext.Users.ToList(),
                Questions = _appDbContext.Questions.ToList(),
            };
            return View(model);
        }

        public IActionResult ShowServeyList()
        {
            var model = _serverDbContext.Surveys.ToList();
            return View(model);
        }
        public IActionResult ShowAnswers(int id)
        {
            var answers = _serverDbContext.Answers.Include(x => x.Survey).Where(x=>x.Survey.Id==id).ToList();
            var model = new ShowAnswersViewModel();
            foreach(var el in answers)
            {
                var question = _appDbContext.Questions.Find(el.QuestionId);
                model.Answers.Add((el, question));
            }
            return View(model);
        }


    }
}
