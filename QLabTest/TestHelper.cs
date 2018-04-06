using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace QLabTest
{
    class TestHelper // Обертка 
    {
        public bool DefaultStation(IWebDriver Chrome, string baseURL) // Дефолтное, первоначальное состояние теста
        {
            bool result;
            try
            {
                Chrome.Manage().Window.Maximize(); // Разворачиваем окно браузера
                Chrome.Navigate().GoToUrl(baseURL);
                Console.WriteLine("Корректное открытие страницы "+ baseURL);
                result = true;
            }
            catch
            {
                Console.WriteLine("Не корректное открытие страницы "+baseURL);
                result = false; // Не удачное открытие страницы
            }
            return result;
        }

        public bool LoginWData(IWebDriver Chrome, string login,  string pass) // Авторизация по уже введенным данным
        {
            bool result;            
            try
            {   // Заполнение полей и клик по кнопке
                Chrome.FindElement(By.CssSelector(".mailbox__login input.mailbox__rwd-control")).SendKeys(login);
                Chrome.FindElement(By.CssSelector(".mailbox__input_password")).SendKeys(pass);
                Chrome.FindElement(By.CssSelector("input.o-control")).Click();
                System.Threading.Thread.Sleep(800); // По какой - то причине, при мгновенная проверка инпута валит тест
                if (Chrome.FindElement(By.CssSelector("span.x-ph__menu__button_auth")).Text.Contains(login)) // Если удалось залогиниться
                { 
                    result = true;
                    Console.WriteLine("Удачная авторизация");
                }
                else
                {
                    Console.WriteLine("Не удачная авторизация");
                    result = false;
                }
            }
            catch
            {
                result = false;
                Console.WriteLine("Не удалось заполнить поля для авоторизации");
            }
            return result;
        }

        public bool CreateNewMail(IWebDriver Chrome, string toAddress)
        {
            bool result;
            try
            {
                Chrome.FindElement(By.CssSelector(".b-layout__col_1_2 .b-toolbar__btn")).Click(); // Кликаем на кнопку "Написать письмо" 
                Chrome.FindElement(By.CssSelector("textarea.compose__labels__input")).SendKeys(toAddress); // Вводим адрес
                Chrome.FindElement(By.CssSelector(".compose-head__field input.b-input")).SendKeys("Тест"); // Тема "Тест"
                Console.WriteLine("Удачное заполнение полей письма");
                Chrome.SwitchTo().Frame(Chrome.FindElement(By.XPath("//*[contains(@id,'composeEditor_ifr')]"))); // Переключаемся во фрейм редактора письма
                Chrome.FindElement(By.CssSelector(".mceContentBody")).SendKeys("Тест"); // Заполняем тело письма
                Chrome.SwitchTo().DefaultContent(); // Переключаемс в дефолтный фрейм
                Console.WriteLine("Удачное заполнение тела письма");
                Chrome.FindElement(By.XPath("//*[@id='b-toolbar__right']/div[3]/div/div[2]/div[1]/div")).Click(); // Нажимаем на кнопку "Отправить"
                try
                {
                    if (Chrome.FindElement(By.CssSelector(".message-sent__title")).Text.Contains("отправлено")) // Проверка удачного отправления
                    {
                        result = true;
                        Console.WriteLine("Удачная отправка письма");
                    } 
                    else
                    {
                        result = false;
                        Console.WriteLine("Не удачная отправка письма");
                    }
                }
                catch
                {
                    result = false;
                    Console.WriteLine("Не удачное заполнение  полей письма");
                }
            }
            catch
            {
                result = false;
                Console.WriteLine("Не удалось заполнить письмо");
            }
            return result;
        }

        public void CloseDriver(IWebDriver Chrome)
        {
            Chrome.Quit();
        }
    }
}
