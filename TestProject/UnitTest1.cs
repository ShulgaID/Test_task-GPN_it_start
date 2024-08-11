using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoTests
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        [Test]
        public void Test_Login_With_Valid_Credentials() // ��������� ����������� ����� � ����������� �������
        {
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.ClassName("btn_action")).Click();
            Assert.IsTrue(driver.Title.Contains("Swag Labs"));
        }

        [Test]
        public void Test_Add_Product_To_Cart() // ��������� ���������� ������ � �������
        {
            Test_Login_With_Valid_Credentials(); // �������� ����� ������ ����� ����������� ������
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            Assert.IsTrue(driver.PageSource.Contains("1"));
        }

        [Test]
        public void Test_Remove_Product_From_Cart() // ��������� �������� ������ �� �������
        {
            Test_Add_Product_To_Cart(); // �������� ����� ���������� ������ ����� ���������
            driver.FindElement(By.ClassName("btn_secondary")).Click();
            Assert.IsFalse(driver.PageSource.Contains("Remove"));
        }

        [Test]
        public void Test_Checkout_Ordering() // ��������� ������� ���������� ������
        {
            Test_Add_Product_To_Cart(); // �������� ����� ���������� ������ ����� ���������
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            driver.FindElement(By.ClassName("btn_action")).Click();
            driver.FindElement(By.Id("first-name")).SendKeys("John");
            driver.FindElement(By.Id("last-name")).SendKeys("Doe");
            driver.FindElement(By.Id("postal-code")).SendKeys("12345");
            driver.FindElement(By.Id("continue")).Click();
            driver.FindElement(By.Id("finish")).Click();
            Assert.IsTrue(driver.PageSource.Contains("Checkout: Complete!"));
        }

        [TearDown]
        public void Teardown()
        {
            driver.Quit();
        }
    }
}
