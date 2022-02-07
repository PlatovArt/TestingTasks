using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace TestingTaskSelenium
{
    internal class Program
    {
        public static string url = "https://careers.veeam.ru/vacancies";
        public static IWebElement webElement;
        public static ChromeDriver driver;
        public const string departmentField = "//*[contains(text(),'Все отделы')]";
        public const string languageField = "//*[contains(text(),'Все языки')]";
        public static int numberOfRealSample;

        static void Main(string[] args)
        {
            StartAndPrepareChrome();

            string departmentFilter = args[0];
            departmentFilter = departmentFilter.Replace('_', ' ');
            string languageFilter = args[1];
            int expectedSample = Convert.ToInt32(args[2]);

            FindAndClick(departmentField);
            FindAndClick("//*[contains(text(),'" + departmentFilter + "')]");
            FindAndClick(languageField);
            FindAndClick("//*[contains(text(),'" + languageFilter + "')]");
            var list = driver.FindElements(By.ClassName("card"));
            numberOfRealSample = list.Count;
            if (numberOfRealSample != expectedSample)
                Console.WriteLine("Размеры ожидаемой и действительной выборки не совпадают");
        }

        public static void StartAndPrepareChrome()
        {
            driver = new ChromeDriver();
            driver.Url = url;
            driver.Manage().Window.Maximize();
        }
        public static void FindAndClick(string xPath)
        {
            webElement = driver.FindElement(By.XPath(xPath));
            webElement.Click();
        }
    }
}
