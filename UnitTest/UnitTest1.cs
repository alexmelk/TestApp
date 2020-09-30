using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.Extensions;
using System;
using TestApp.Context;
using TestApp.Controllers;
using TestApp.Models;
using System.Text.RegularExpressions;

namespace UnitTest
{
    [TestClass]
    public class SurveyControllerTests
    {
        IWebDriver driver;
        WebDriverWait wait;
        public string login = "admin@mail.ru";
        public string pass = "adminAdmin1!";

        [TestMethod]
        public void SurveyTestSimple()
        {
            var date = DateTime.Now;

            PassSurvey(date);

            CheckSurvey(date);

        }
        //проверяем, корректно ли добавились данные
        public void CheckSurvey(DateTime date)
        {
            //авторизируемся
            driver.Navigate().GoToUrl("https://localhost:44304/Account/Login");
            driver.ExecuteJavaScript($"$(\"[type = 'email']\")[0].value = \"{login}\"");
            driver.ExecuteJavaScript($"$(\"[type = 'password']\")[0].value = \"{pass}\"");
            driver.ExecuteJavaScript("$(\"[type = 'submit']\")[0].click()");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            driver.Navigate().GoToUrl("https://localhost:44304/Admin/ShowServeyList");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            //получаем последнее значение из таблицы опросов
            driver.ExecuteJavaScript($" $(\"[role = 'row']\").last().find('a')[0].click()");
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            //проверяем дату на корректность
            var list = driver.FindElements(By.CssSelector("[role='row']"));
            bool flag = false;
            foreach (var el in list)
            {
                var answer = el.Text;
                Regex r = new Regex(date.ToString("dd.MM.yyyy HH:mm:ss"));
                flag = r.Match(answer).Success;
                if (flag) break;
            }

            Assert.IsTrue(flag);
        }
        public void PassSurvey(DateTime date)
        {
            {
                var options = new ChromeOptions() { };
                //options.AddArguments("--headless");
                driver = new ChromeDriver();
                driver.Navigate().GoToUrl("https://localhost:44304/");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                //нажали кнопку пройти опрос
                driver.ExecuteJavaScript("$('.btn.btn-success')[0].click()");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                //ввели первый ответ
                driver.ExecuteJavaScript($"$('#Answer_Text')[0].value = 'Test [{date.ToString("dd.MM.yyyy HH:mm:ss")}]'");
                driver.ExecuteJavaScript("$('#next')[0].click();");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));


                driver.ExecuteJavaScript($"$('#Answer_Text')[0].value = '10'");
                driver.ExecuteJavaScript("$('#next')[0].click();");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                driver.ExecuteJavaScript($"$('select')[0].options[1].selected = true;");
                driver.ExecuteJavaScript("$('#next')[0].click();");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                driver.ExecuteJavaScript($"$('#Answer_Text')[0].value = '{date.ToString("yyyy-MM-dd")}'");
                driver.ExecuteJavaScript("$('#next')[0].click();");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                driver.ExecuteJavaScript($"$('select')[0].options[1].selected = true;");
                driver.ExecuteJavaScript("$('#next')[0].click();");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

                driver.ExecuteJavaScript($"$('#Answer_IsCheck')[0].checked");
                driver.ExecuteJavaScript("$('#next')[0].click();");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            }

        }
    }
}
