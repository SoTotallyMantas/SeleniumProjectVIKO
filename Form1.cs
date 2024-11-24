using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.DevTools.V128.Debugger;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumProjectVIKO.Class;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Drawing;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
namespace SeleniumProjectVIKO
{
    public partial class Form1 : Form
    {
        public string TestUserUrl = "";
        IWebDriver driver = new ChromeDriver();
        SeleniumDriver seleniumDriver;
        public string imagePath1;
        public string imagePath2;
        public string textFilePath;


        public Form1()
        {
            InitializeComponent();
            seleniumDriver = new SeleniumDriver(driver);
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
             imagePath1 = Path.Combine(basePath, "TestMaterial", "Test1.png");
             imagePath2 = Path.Combine(basePath, "TestMaterial", "Test2.png");
             textFilePath = Path.Combine(basePath, "TestMaterial", "TestText.docx");
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
           

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            string startingUrl = "https://opensource-demo.orangehrmlive.com";


            seleniumDriver.GoToUrl(startingUrl);
            if (CheckIfAuth(seleniumDriver, driver, startingUrl) == false)
            {


                MessageBox.Show("Login Failed");
                return;
            }
            driver.Manage().Window.Maximize();
            CreateUser(seleniumDriver, imagePath1, "UserSupervisor", driver);
            TestUserUrl = CreateUser(seleniumDriver, imagePath2, "UserTesting", driver);

        }
        private bool CheckIfAuth(SeleniumDriver seleniumDriver, IWebDriver driver, string WantedUrl)
        {
            bool Check = true;
            if (driver.Url == "https://opensource-demo.orangehrmlive.com/web/index.php/auth/login")
            {
                try
                {
                    Login(seleniumDriver);

                    seleniumDriver.GoToUrl(WantedUrl);
                }
                catch
                {
                    Check = false;
                }
                return Check;

            }
            else
            {
                return true;
            }

        }
        private void Login(SeleniumDriver seleniumDriver)
        {

            IWebElement UsernameElement = seleniumDriver.FindElementByName("username");
            IWebElement PasswordElement = seleniumDriver.FindElementByName("password");
            UsernameElement.SendKeys("Admin");
            PasswordElement.SendKeys("admin123");
            IWebElement LoginButton = seleniumDriver.FindElementByClassName("oxd-button oxd-button--medium oxd-button--main orangehrm-login-button");
            seleniumDriver.Click(LoginButton);

        }

        private string CreateUser(SeleniumDriver seleniumDriver, string ImagePath, string username, IWebDriver driver)
        {

            seleniumDriver.GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/pim/addEmployee");
            System.Threading.Thread.Sleep(1000);
            IWebElement SendPicture = seleniumDriver.FindElementByCssSelector("input[type='file']");
            SendPicture.SendKeys(ImagePath);

            System.Threading.Thread.Sleep(1000);
            IWebElement FirstName = seleniumDriver.FindElementByName("firstName");
            FirstName.SendKeys(username);
            System.Threading.Thread.Sleep(1000);
            IWebElement LastName = seleniumDriver.FindElementByName("lastName");
            LastName.SendKeys("Test");
            System.Threading.Thread.Sleep(1000);
            IWebElement EmployeeID = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[1]/div[2]/div[1]/div[2]/div/div/div[2]/input");
            Random random = new Random();
            int randomNumber = random.Next(10, 999);
            EmployeeID.SendKeys(randomNumber.ToString());
            System.Threading.Thread.Sleep(1000);
            IWebElement CreateLoginDetails = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[1]/div[2]/div[2]/div/label");
            CreateLoginDetails.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement UserName = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[1]/div[2]/div[3]/div/div[1]/div/div[2]/input");
            UserName.SendKeys("Test123" + randomNumber.ToString());
            System.Threading.Thread.Sleep(1000);

            IWebElement Password = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[1]/div[2]/div[4]/div/div[1]/div/div[2]/input");
            Password.SendKeys("Test123");
            System.Threading.Thread.Sleep(1000);
            IWebElement ConfirmPassword = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[1]/div[2]/div[4]/div/div[2]/div/div[2]/input");
            ConfirmPassword.SendKeys("Test123");
            System.Threading.Thread.Sleep(1000);
            string CurrentUrl = driver.Url;

            IWebElement SaveButton = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/button[2]");
            SaveButton.Click();
            System.Threading.Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[1]/div[2]/div[6]/a")));
            string UserPathUrl = driver.Url;
            IWebElement JobButton = seleniumDriver.FindElementByXPath("//a[normalize-space()='Job']");
            JobButton.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement JoinedDate = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[1]/div/div[2]/div/div/input");
            JoinedDate.SendKeys("2024-01-11");

            IWebElement JobTitle = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[2]/div/div[2]/div/div/div[1]");
            JobTitle.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement JobTitleSelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[2]/div/div[2]/div/div[2]/div[2]");
            JobTitleSelect.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement Location = seleniumDriver.FindElementByXPath("/html[1]/body[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/form[1]/div[1]/div[1]/div[6]/div[1]/div[2]/div[1]/div[1]");
            Location.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement LocationSelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[6]/div/div[2]/div/div[2]/div[2]");
            LocationSelect.Click();
            System.Threading.Thread.Sleep(1000);

            IWebElement JobCategory = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[4]/div/div[2]/div/div/div[1]");
            JobCategory.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement JobCategorySelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[4]/div/div[2]/div/div[2]/div[2]");
            JobCategorySelect.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement EmploymentStatus = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[7]/div/div[2]/div/div/div[1]");
            EmploymentStatus.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement EmployementStatusSelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[7]/div/div[2]/div/div[2]/div[4]");
            EmployementStatusSelect.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement SaveJob = seleniumDriver.FindElementByCssSelector("button[type='submit']");
            SaveJob.Click();
            System.Threading.Thread.Sleep(1000);
            if (username != "UserSupervisor")
            {


                IWebElement ReportTo = seleniumDriver.FindElementByXPath("//a[normalize-space()='Report-to']");
                ReportTo.Click();
                System.Threading.Thread.Sleep(1000);

                IWebElement AddAssign = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[2]/div[1]/div/button");
                AddAssign.Click();
                System.Threading.Thread.Sleep(1000);
                IWebElement SupervisorAssign = seleniumDriver.FindElementByXPath("//input[@placeholder='Type for hints...']");
                SupervisorAssign.SendKeys("UserSupervisor");

                System.Threading.Thread.Sleep(5000);
                IWebElement AssignListBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[2]/div[1]/form/div[1]/div/div[1]/div/div[2]/div/div[2]/div[1]")));
                AssignListBox.Click();
                System.Threading.Thread.Sleep(1000);
                IWebElement Reporting = seleniumDriver.FindElementByXPath("//div[@class='oxd-select-text-input']");
                Reporting.Click();
                System.Threading.Thread.Sleep(1000);
                IWebElement ReportingSelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[2]/div[1]/form/div[1]/div/div[2]/div/div[2]/div/div[2]/div[2]");
                ReportingSelect.Click();
                System.Threading.Thread.Sleep(1000);

                IWebElement SaveReporting = seleniumDriver.FindElementByXPath("//button[@type='submit']");
                SaveReporting.Click();
                System.Threading.Thread.Sleep(1000);

                seleniumDriver.GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/pim/viewEmployeeList");
                System.Threading.Thread.Sleep(1000);
               // IWebElement ExpandSearch = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div[1]/div[1]/div[2]/div[3]/button");
               // ExpandSearch.Click();
                System.Threading.Thread.Sleep(1000);

                IWebElement EmployementStatusAll = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div[1]/div[2]/form/div[1]/div/div[3]/div/div[2]/div/div");
                EmployementStatusAll.Click();
                System.Threading.Thread.Sleep(1000);

                IWebElement EmployementStatusAllSelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div[1]/div[2]/form/div[1]/div/div[3]/div/div[2]/div/div[2]/div[4]");
                EmployementStatusAllSelect.Click();
                System.Threading.Thread.Sleep(1000);

                IWebElement SupervisorNameSearch = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div[1]/div[2]/form/div[1]/div/div[5]/div/div[2]/div/div/input");
                SupervisorNameSearch.SendKeys("UserSupervisor");
                System.Threading.Thread.Sleep(5000);
                IWebElement SelectSupervisor = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div[1]/div[2]/form/div[1]/div/div[5]/div/div[2]/div/div[2]/div")));
                SelectSupervisor.Click();
                System.Threading.Thread.Sleep(1000);
                IWebElement SubmitButton = seleniumDriver.FindElementByXPath("//button[@type='submit']");
                SubmitButton.Click();

            }

            return UserPathUrl;

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            seleniumDriver.GoToUrl(TestUserUrl);
            if (CheckIfAuth(seleniumDriver, driver, TestUserUrl) == false)
            {


                MessageBox.Show("Login Failed");
                return;
            }
            SecondScenario();



        }
        private void SecondScenario()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ReadOnlyCollection<IWebElement> inputs = driver.FindElements(By.XPath("//input[@class='oxd-input oxd-input--active']"));

            foreach (IWebElement input in inputs)
            {
                var placeholder = input.GetAttribute("placeholder");
                string text = input.Text.ToString();
                if (placeholder == "yyyy-dd-mm")
                {
                    input.SendKeys("2024-01-11");
                    System.Threading.Thread.Sleep(1000);


                }
                else
                {

                    Random random = new Random();
                    int randomNumber = random.Next(10, 99);
                    input.SendKeys(randomNumber.ToString());
                    System.Threading.Thread.Sleep(1000);
                }

            }
            ReadOnlyCollection<IWebElement> SelectInputs = driver.FindElements(By.XPath("//div[@class='oxd-select-text oxd-select-text--active']"));
            foreach (IWebElement input in SelectInputs)
            {
                input.Click();
                System.Threading.Thread.Sleep(1000);
                ReadOnlyCollection<IWebElement> SelectOptions = driver.FindElements(By.XPath("//div[@role='option']"));
                foreach (IWebElement option in SelectOptions)
                {
                    string selectionText = option.Text.ToString();
                    if (selectionText != "-- Select --" && selectionText != "--Select--")
                    {

                        option.Click();
                        System.Threading.Thread.Sleep(1000);
                        break;
                    }
                }
            }


            IWebElement RadioButton = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[3]/div[2]/div[2]/div/div[2]/div[1]/div[2]/div/label/span");
            RadioButton.Click();
            System.Threading.Thread.Sleep(1000);
            ReadOnlyCollection<IWebElement> SaveButtons = driver.FindElements(By.XPath("//button[@type='submit']"));
            foreach (IWebElement button in SaveButtons)
            {
                string ButtonText = button.Text.ToString();
                if (ButtonText == "Save")
                {
                    button.Click();
                    System.Threading.Thread.Sleep(1000);
                }
            }


            System.Threading.Thread.Sleep(1000);
            IWebElement AddAtachement = seleniumDriver.FindElementByXPath("//button[normalize-space()='Add']");

            AddAtachement.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement SendFile = seleniumDriver.FindElementByCssSelector("input[type='file']");

            IWebElement SumbitFile = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[3]/div/form/div[3]/button[2]");

            for (int i = 0; i < 2; i++)
            {
                System.Threading.Thread.Sleep(5000);
                if (i == 1)
                {
                    AddAtachement = seleniumDriver.FindElementByXPath("//button[normalize-space()='Add']");
                    AddAtachement.Click();
                    System.Threading.Thread.Sleep(1000);
                }
                
                SendFile = seleniumDriver.FindElementByCssSelector("input[type='file']");
                if (i == 0)
                {
                    SendFile.SendKeys(imagePath1);
                }
                else
                {
                    SendFile.SendKeys(imagePath2);
                }
                System.Threading.Thread.Sleep(1000);
                SumbitFile = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[3]/div/form/div[3]/button[2]");
                SumbitFile.Click();




            }
            System.Threading.Thread.Sleep(3000);
            IWebElement DownloadButton = seleniumDriver.FindElementByXPath("//i[@class='oxd-icon bi-download']");
            DownloadButton.Click();
            System.Threading.Thread.Sleep(3000);
            IWebElement EditFile = seleniumDriver.FindElementByXPath("//i[@class='oxd-icon bi-pencil-fill']");
            EditFile.Click();
            System.Threading.Thread.Sleep(3000);
            
            SendFile = seleniumDriver.FindElementByCssSelector("input[type='file']");
            SendFile.SendKeys(imagePath1);
            SumbitFile = seleniumDriver.FindElementByXPath("//div[@class='orangehrm-attachment']//button[@type='submit'][normalize-space()='Save']");
            SumbitFile.Click();
            System.Threading.Thread.Sleep(3000);
            IWebElement DeleteFile = seleniumDriver.FindElementByXPath("//i[@class='oxd-icon bi-trash']");
            DeleteFile.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement ConfirmDelete = seleniumDriver.FindElementByXPath("//button[normalize-space()='Yes, Delete']");
            ConfirmDelete.Click();
           
            
            
           
        }
        public void ThirdScenario()
        {
            driver.Manage().Window.Maximize();
            System.Threading.Thread.Sleep(1000);
            IWebElement AddCandidate = seleniumDriver.FindElementByXPath("//button[normalize-space()='Add']");
            AddCandidate.Click();
            System.Threading.Thread.Sleep(1000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ReadOnlyCollection<IWebElement> inputs = driver.FindElements(By.XPath("//input[@class='oxd-input oxd-input--active']"));


            //input[@placeholder='First Name']
            IWebElement FirstName = seleniumDriver.FindElementByXPath("//input[@placeholder='First Name']");
            FirstName.SendKeys("TestFirst");
            System.Threading.Thread.Sleep(1000);
            //input[@placeholder='Middle Name']
            IWebElement MiddleName = seleniumDriver.FindElementByXPath("//input[@placeholder='Middle Name']");
            MiddleName.SendKeys("TestMiddle");
            System.Threading.Thread.Sleep(1000);

            //input[@placeholder='Last Name']
            IWebElement LastName = seleniumDriver.FindElementByXPath("//input[@placeholder='Last Name']");
            LastName.SendKeys("TestLast");
            System.Threading.Thread.Sleep(1000);
            IWebElement Email = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[3]/div/div[1]/div/div[2]/input");
            Email.SendKeys("Test@test.com");
            System.Threading.Thread.Sleep(1000);

            IWebElement ContactNumber = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[3]/div/div[2]/div/div[2]/input");
            ContactNumber.SendKeys("1234567890");
            System.Threading.Thread.Sleep(1000);

            IWebElement Vacancy = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div/div/div[2]/div/div/div[1]");
            Vacancy.Click();


            System.Threading.Thread.Sleep(1000);
            ReadOnlyCollection<IWebElement> SelectOptions = driver.FindElements(By.XPath("//div[@role='option']"));
            foreach (IWebElement option in SelectOptions)
            {
                string selectionText = option.Text.ToString();
                if (selectionText == "Payroll Administrator")
                {
                    option.Click();
                    System.Threading.Thread.Sleep(1000);
                    break;
                }
            }
            Thread.Sleep(1000);
            IWebElement SendFile = seleniumDriver.FindElementByCssSelector("input[type='file']");
            SendFile.SendKeys(textFilePath);
            System.Threading.Thread.Sleep(1000);
            //IWebElement DateOfApplication = seleniumDriver.FindElementByXPath("//input[@placeholder='yyyy-dd-mm']");
            //DateOfApplication.SendKeys("2024-01-11");
            //System.Threading.Thread.Sleep(1000);
            IWebElement Keywords = seleniumDriver.FindElementByXPath("//input[@placeholder='Enter comma seperated words...']");
            Keywords.SendKeys("Test,tas,tos,tis");
            System.Threading.Thread.Sleep(1000);
            IWebElement Notes = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[6]/div/div/div/div[2]/textarea");
            Notes.SendKeys("TestNotes");
            System.Threading.Thread.Sleep(1000);
            IWebElement ConsentCheck = seleniumDriver.FindElementByXPath("//span[@class='oxd-checkbox-input oxd-checkbox-input--active --label-right oxd-checkbox-input']");
            ConsentCheck.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement SaveButton = seleniumDriver.FindElementByXPath("//button[@type='submit']");
            SaveButton.Click();
            System.Threading.Thread.Sleep(5000);

            IWebElement ShortlistButton = seleniumDriver.FindElementByXPath("//button[normalize-space()='Shortlist']");
            ShortlistButton.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement ShortlistNotes = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div/div/div[2]/textarea");
            ShortlistNotes.SendKeys("TestShortlistNotes");
            System.Threading.Thread.Sleep(1000);
            IWebElement ShortlistButtonSubmit = seleniumDriver.FindElementByXPath("//button[@type='submit']");

            ShortlistButtonSubmit.Click();
            System.Threading.Thread.Sleep(3000);
            IWebElement ScheduleInterview = seleniumDriver.FindElementByXPath("//button[normalize-space()='Schedule Interview']");

            ScheduleInterview.Click();
            System.Threading.Thread.Sleep(1000);


            IWebElement InterviewTitle = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div[1]/div/div[2]/input");
            InterviewTitle.SendKeys("TestInterviewTitle");
            System.Threading.Thread.Sleep(1000);



            IWebElement InterviewDate = seleniumDriver.FindElementByXPath("//input[@placeholder='yyyy-dd-mm']");
            InterviewDate.SendKeys("2024-01-11");
            System.Threading.Thread.Sleep(1000);
            IWebElement InterviewTime = seleniumDriver.FindElementByXPath("//input[@placeholder='hh:mm']");
            InterviewTime.SendKeys("12:00");
            System.Threading.Thread.Sleep(1000);
            IWebElement Interviewer = seleniumDriver.FindElementByXPath("//input[@placeholder='Type for hints...']");
            Interviewer.SendKeys("UserSupervisor");
            System.Threading.Thread.Sleep(5000);

            IWebElement InterviewerSelect = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div[2]/div/div/div[2]/div/div[2]/div[1]")));
            InterviewerSelect.Click();
            IWebElement InterviewNotes = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div[5]/div/div[2]/textarea");
            InterviewNotes.SendKeys("TestInterviewNotes");
            System.Threading.Thread.Sleep(1000);
            IWebElement InterviewSubmit = seleniumDriver.FindElementByXPath("//button[@type='submit']");
            InterviewSubmit.Click();
            System.Threading.Thread.Sleep(1000);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string url = "https://opensource-demo.orangehrmlive.com/web/index.php/recruitment/viewCandidates";
            seleniumDriver.GoToUrl(url);
            if (CheckIfAuth(seleniumDriver, driver, url) == false)
            {


                MessageBox.Show("Login Failed");
                return;
            }
            ThirdScenario();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string url = "https://opensource-demo.orangehrmlive.com/web/index.php/buzz/viewBuzz";
            seleniumDriver.GoToUrl(url);
            if (CheckIfAuth(seleniumDriver, driver, url) == false)
            {


                MessageBox.Show("Login Failed");
                return;
            }
            FourthScenario();
        }
        public void FourthScenario()
        {
            driver.Manage().Window.Maximize();
            System.Threading.Thread.Sleep(5000);
            IWebElement AddPicture = seleniumDriver.FindElementByXPath("//button[normalize-space()='Share Photos']");
            
            AddPicture.Click();
            System.Threading.Thread.Sleep(3000);

            IWebElement AddText = seleniumDriver.FindElementByXPath("//div[@class='orangehrm-buzz-post-modal-header-text']//textarea[@placeholder=\"What's on your mind?\"]");
            AddText.SendKeys("TestPost");
            System.Threading.Thread.Sleep(1000);
            IWebElement SendFile = seleniumDriver.FindElementByCssSelector("input[type='file']");   
            SendFile.SendKeys(imagePath1);
            System.Threading.Thread.Sleep(1000);
            IWebElement PostButton = seleniumDriver.FindElementByXPath("//div[@class='oxd-form-actions orangehrm-buzz-post-modal-actions']//button[1]");
            PostButton.Click();
            System.Threading.Thread.Sleep(3000);
           
            IWebElement LikeButtonPost = seleniumDriver.FindElementByXPath("//div[@class='orangehrm-buzz-newsfeed']//div[1]//div[1]//div[3]//div[1]//div[1]//*[name()='svg']//*[name()='g' and @id='Group']//*[name()='path' and @id='heart']");
            LikeButtonPost.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement ThreeDots = seleniumDriver.FindElementByXPath("//div[@class='orangehrm-buzz-newsfeed']//div[1]//div[1]//div[1]//div[1]//div[2]//li[1]//button[1]//i[1]");
            ThreeDots.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement EditPost = seleniumDriver.FindElementByXPath("//p[normalize-space()='Edit Post']");
            EditPost.Click();
            System.Threading.Thread.Sleep(1000);

            IWebElement EditPostText = seleniumDriver.FindElementByXPath("//div[@class='oxd-buzz-post oxd-buzz-post--active oxd-buzz-post--composing']//textarea[@class='oxd-buzz-post-input']");
            EditPostText.SendKeys("TestEdit");
            System.Threading.Thread.Sleep(1000);

            SendFile = seleniumDriver.FindElementByCssSelector("input[type='file']");

            SendFile.SendKeys(imagePath2);
            System.Threading.Thread.Sleep(1000);

            IWebElement PostEdit = seleniumDriver.FindElementByXPath("//div[@class='oxd-form-actions orangehrm-buzz-post-modal-actions']//button[@type='submit'][normalize-space()='Post']");
            PostEdit.Click();
                
            System.Threading.Thread.Sleep(1000);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.ClassName("oxd-toast-container--toast")));

            IWebElement ClickComment = seleniumDriver.FindElementByXPath("//div[@class='orangehrm-buzz-newsfeed']//div[1]//div[1]//div[3]//div[1]//button[1]");
            ClickComment.Click();
            System.Threading.Thread.Sleep(1000);

            IWebElement CommentText = seleniumDriver.FindElementByXPath("//input[@placeholder='Write your comment...']");
            CommentText.SendKeys("TestComment");
            System.Threading.Thread.Sleep(2000);
            CommentText.SendKeys(OpenQA.Selenium.Keys.Enter);
            System.Threading.Thread.Sleep(4000);
            IWebElement LikeComment = seleniumDriver.FindElementByXPath("//div[@class='orangehrm-post-comment-action-area']//p[1]");
            LikeComment.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement ThreeDotsComment = seleniumDriver.FindElementByXPath("//p[normalize-space()='Edit']");
            ThreeDotsComment.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement EditComment = seleniumDriver.FindElementByXPath("//input[@class='oxd-input oxd-input--focus']");
            EditComment.SendKeys("TestEdit");
            System.Threading.Thread.Sleep(3000);
            EditComment.SendKeys(OpenQA.Selenium.Keys.Enter);
            System.Threading.Thread.Sleep(3000);
            IWebElement DeleteComment = seleniumDriver.FindElementByXPath("//p[normalize-space()='Delete']");
            DeleteComment.Click();
            System.Threading.Thread.Sleep(2000);
            IWebElement ConfirmDeletion = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[3]/div/div/div/div[3]/button[2]");
            ConfirmDeletion.Click();
            System.Threading.Thread.Sleep(2000);
           
           
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@class='orangehrm-buzz-newsfeed']//div[1]//div[1]//div[1]//div[1]//div[2]//li[1]//button[1]//i[1]")));
            ThreeDots = seleniumDriver.FindElementByXPath("//div[@class='orangehrm-buzz-newsfeed']//div[1]//div[1]//div[1]//div[1]//div[2]//li[1]//button[1]//i[1]");
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            Thread.Sleep(2000);
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, 0);");

            Thread.Sleep(3000);
            ThreeDots.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement DeletePost = seleniumDriver.FindElementByXPath("//p[normalize-space()='Delete Post']");
            DeletePost.Click();
            System.Threading.Thread.Sleep(1000);
            ConfirmDeletion = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[3]/div/div/div/div[3]/button[2]");
            ConfirmDeletion.Click();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string url = "https://opensource-demo.orangehrmlive.com/web/index.php/pim/definePredefinedReport";
            seleniumDriver.GoToUrl(url);
            if (CheckIfAuth(seleniumDriver, driver, url) == false)
            {


                MessageBox.Show("Login Failed");
                return;
            }
            FifthScenario();
        }

        public void FifthScenario()
        {
            IWebElement ReportName = seleniumDriver.FindElementByXPath("//input[@placeholder='Type here ...']");
            ReportName.SendKeys("TestReport");
            System.Threading.Thread.Sleep(1000);
            IWebElement Criteria = seleniumDriver.FindElementByXPath("//body[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]/form[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]");
            Criteria.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement EmployeeCriteria = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div[1]/div[1]/div[2]/div/div[2]/div[2]");
            EmployeeCriteria.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement ClickAdd = seleniumDriver.FindElementByXPath("//div[@class='oxd-grid-4 orangehrm-full-width-grid']//div[1]//div[2]//div[2]//button[1]//i[1]");
            ClickAdd.Click();
            Thread.Sleep(1000);
            Criteria = seleniumDriver.FindElementByXPath("//body[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]/form[1]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]");
            Criteria.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement PayGrade = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div[1]/div[1]/div[2]/div/div[2]/div[2]");
            PayGrade.Click();
            System.Threading.Thread.Sleep(1000);
            ClickAdd = seleniumDriver.FindElementByXPath("//div[@class='oxd-grid-4 orangehrm-full-width-grid']//div[1]//div[2]//div[2]//button[1]//i[1]");
            ClickAdd.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement include = seleniumDriver.FindElementByXPath("//div[contains(text(),'Current Employees Only')]");
            include.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement includeSelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div[2]/div/div[2]/div/div[2]/div[2]");
            includeSelect.Click();
            System.Threading.Thread.Sleep(1000);
            IWebElement DeleteCriteria = seleniumDriver.FindElementByXPath("//body[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]/form[1]/div[2]/div[1]/div[3]/button[1]/i[1]");
            DeleteCriteria.Click();
            System.Threading.Thread.Sleep(1000);
            DeleteCriteria = seleniumDriver.FindElementByXPath("(//i[@class='oxd-icon bi-trash-fill'])[1]");
            DeleteCriteria.Click();
            System.Threading.Thread.Sleep(1000);

            IWebElement DisplayField = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[3]/div/div[2]/div[1]/div[2]/div/div[1]");
            IWebElement FieldGroup = seleniumDriver.FindElementByXPath("//body/div[@id='app']/div[@class='oxd-layout orangehrm-upgrade-layout']/div[@class='oxd-layout-container']/div[@class='oxd-layout-context']/div[@class='orangehrm-background-container']/div[@class='orangehrm-card-container']/form[@class='oxd-form']/div[3]/div[1]/div[1]/div[1]/div[2]/div[1]/div[1]");
            HashSet<string> SelectedDisplayFields = new HashSet<string>();
            
            for (int i=0;i<4; i++)
            {
                FieldGroup.Click();
                System.Threading.Thread.Sleep(1000);

                ReadOnlyCollection<IWebElement> SelectOptions = driver.FindElements(By.XPath("//div[@role='option']"));
                foreach (IWebElement option in SelectOptions)
                {
                    try
                    {


                        string selectionText = option.Text.ToString();

                        string Attribute = option.GetAttribute("class");

                        if (selectionText != "-- Select --" && selectionText != "--Select--" && !Attribute.Contains("oxd-select-option --selected") && !SelectedDisplayFields.Contains(selectionText))
                        {
                            SelectedDisplayFields.Add(selectionText);
                            option.Click();
                            System.Threading.Thread.Sleep(1000);
                            for (int j = 0; j < 4; j++)
                            {
                                DisplayField = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[3]/div/div[2]/div[1]/div[2]/div/div[1]");
                                DisplayField.Click();
                                System.Threading.Thread.Sleep(1000);
                                ReadOnlyCollection<IWebElement> SelectOptions2 = driver.FindElements(By.XPath("//div[@role='option']"));
                                foreach (IWebElement option2 in SelectOptions2)
                                {
                                    string selectionText2 = option2.Text.ToString();
                                    string Attribute2 = option2.GetAttribute("class");

                                    if (selectionText2 != "-- Select --" && selectionText2 != "--Select--" && !Attribute2.Contains("oxd-select-option --selected"))
                                    {

                                        option2.Click();
                                        System.Threading.Thread.Sleep(1000);
                                        IWebElement AddNewDisplayField = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[3]/div/div[2]/div[2]/div[2]/button/i");
                                        AddNewDisplayField.Click();
                                        break;
                                    }



                                }
                            }
                        }
                    }
                    catch
                    {
                        break;               
                    }
                }
            }

            ReadOnlyCollection<IWebElement> HeaderCheckbox = driver.FindElements(By.XPath("(//span[@class='oxd-switch-input oxd-switch-input--active --label-right'])"));
            foreach (IWebElement checkbox in HeaderCheckbox)
            {
                checkbox.Click();
                System.Threading.Thread.Sleep(1000);
            }

            
            System.Threading.Thread.Sleep(1000);
            IWebElement DeleteDisplayField = seleniumDriver.FindElementByXPath("//div[@id='app']//div[7]//div[1]//span[1][1]//i[1]");
            DeleteDisplayField.Click();
            System.Threading.Thread.Sleep(1000);
            DeleteDisplayField = seleniumDriver.FindElementByXPath("//div[@id='app']//div[10]//div[1]//span[1][1]//i[1]");
            DeleteDisplayField.Click();
            System.Threading.Thread.Sleep(1000);
            DeleteDisplayField = seleniumDriver.FindElementByXPath("//div[@id='app']//div[10]//div[1]//span[1][1]//i[1]");
            DeleteDisplayField.Click();
            System.Threading.Thread.Sleep(3000);
            IWebElement DeleteOne = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[3]/div/div[3]/button/i");
            DeleteOne.Click();
            System.Threading.Thread.Sleep(3000);
            IWebElement SaveButton = seleniumDriver.FindElementByXPath("//button[@type='submit']");
            SaveButton.Click();




        }
    }
}
