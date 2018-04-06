using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace QLabTest
{
    [TestClass]
    public class UnitTest1
    {
        static string baseURL;
        private static int wait_time; // кол-во секунд для ожидания элемента на странице        
        private static ChromeDriver Chrome;
        private static TestHelper TH;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext) // 
        {            
            baseURL = "https://mail.ru/";
            wait_time = 5;
            TimeSpan interval = new TimeSpan(0, 0, wait_time);
            Chrome = new ChromeDriver();
            Chrome.Manage().Timeouts().ImplicitlyWait(interval);
            TH = new TestHelper();
        }

        [DataTestMethod]
        [DataRow("testermim@mail.ru", "05042018MIM")]
        public void MailruTest(string login, string pass)
        { 
            Assert.IsTrue(TH.DefaultStation(Chrome, baseURL));             
            Assert.IsTrue(TH.LoginWData(Chrome, login, pass));
            Assert.IsTrue(TH.CreateNewMail(Chrome, login));
            TH.CloseDriver(Chrome);
            Console.WriteLine("Тест завершен");
        }
    }
}
