using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V128.Debugger;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SeleniumProjectVIKO.Class;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace SeleniumProjectVIKO
{
    public partial class Form1 : Form
    {
        public string TestUserUrl = "";
        IWebDriver driver = new ChromeDriver();
        SeleniumDriver seleniumDriver;
        public Form1()
        {
            InitializeComponent();
            seleniumDriver = new SeleniumDriver(driver);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string ImagePath = "";
            openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImagePath = openFileDialog.FileName;
            }

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            string startingUrl = "https://opensource-demo.orangehrmlive.com";


            seleniumDriver.GoToUrl(startingUrl);
            if (CheckIfAuth(seleniumDriver, driver, startingUrl) == false)
            {


                MessageBox.Show("Login Failed");
                return;
            }

            CreateUser(seleniumDriver, ImagePath, "UserSupervisor", driver);
            TestUserUrl = CreateUser(seleniumDriver, ImagePath, "UserTesting", driver);

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

            IWebElement SendPicture = seleniumDriver.FindElementByCssSelector("input[type='file']");
            SendPicture.SendKeys(ImagePath);

            IWebElement FirstName = seleniumDriver.FindElementByName("firstName");
            FirstName.SendKeys(username);

            IWebElement LastName = seleniumDriver.FindElementByName("lastName");
            LastName.SendKeys("Test");

            IWebElement EmployeeID = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[1]/div[2]/div[1]/div[2]/div/div/div[2]/input");
            Random random = new Random();
            int randomNumber = random.Next(1000, 9999);
            EmployeeID.SendKeys(randomNumber.ToString());

            IWebElement CreateLoginDetails = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[1]/div[2]/div[2]/div/label");
            CreateLoginDetails.Click();
            IWebElement UserName = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[1]/div[2]/div[3]/div/div[1]/div/div[2]/input");
            UserName.SendKeys("Test123" + randomNumber.ToString());

            IWebElement Password = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[1]/div[2]/div[4]/div/div[1]/div/div[2]/input");
            Password.SendKeys("Test123");

            IWebElement ConfirmPassword = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[1]/div[2]/div[4]/div/div[2]/div/div[2]/input");
            ConfirmPassword.SendKeys("Test123");

            string CurrentUrl = driver.Url;

            IWebElement SaveButton = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/button[2]");
            SaveButton.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[1]/div[2]/div[6]/a")));
            string UserPathUrl = driver.Url;
            IWebElement JobButton = seleniumDriver.FindElementByXPath("//a[normalize-space()='Job']");
            JobButton.Click();

            IWebElement JoinedDate = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[1]/div/div[2]/div/div/input");
            JoinedDate.SendKeys("2024-01-11");

            IWebElement JobTitle = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[2]/div/div[2]/div/div/div[1]");
            JobTitle.Click();
            IWebElement JobTitleSelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[2]/div/div[2]/div/div[2]/div[2]");
            JobTitleSelect.Click();
            IWebElement Location = seleniumDriver.FindElementByXPath("/html[1]/body[1]/div[1]/div[1]/div[2]/div[2]/div[1]/div[1]/div[1]/div[2]/div[1]/form[1]/div[1]/div[1]/div[6]/div[1]/div[2]/div[1]/div[1]");
            Location.Click();
            IWebElement LocationSelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[6]/div/div[2]/div/div[2]/div[2]");
            LocationSelect.Click();

            IWebElement JobCategory = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[4]/div/div[2]/div/div/div[1]");
            JobCategory.Click();
            IWebElement JobCategorySelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[4]/div/div[2]/div/div[2]/div[2]");
            JobCategorySelect.Click();

            IWebElement EmploymentStatus = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[7]/div/div[2]/div/div/div[1]");
            EmploymentStatus.Click();
            IWebElement EmployementStatusSelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[1]/div/div[7]/div/div[2]/div/div[2]/div[4]");
            EmployementStatusSelect.Click();
            IWebElement SaveJob = seleniumDriver.FindElementByCssSelector("button[type='submit']");
            SaveJob.Click();
            if (username != "UserSupervisor")
            {


                IWebElement ReportTo = seleniumDriver.FindElementByXPath("//a[normalize-space()='Report-to']");
                ReportTo.Click();

                IWebElement AddAssign = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[2]/div[1]/div/button");
                AddAssign.Click();

                IWebElement SupervisorAssign = seleniumDriver.FindElementByXPath("//input[@placeholder='Type for hints...']");
                SupervisorAssign.SendKeys("UserSupervisor");
                System.Threading.Thread.Sleep(5000);
                IWebElement AssignListBox = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[2]/div[1]/form/div[1]/div/div[1]/div/div[2]/div/div[2]/div[1]")));
                AssignListBox.Click();
                IWebElement Reporting = seleniumDriver.FindElementByXPath("//div[@class='oxd-select-text-input']");
                Reporting.Click();
                IWebElement ReportingSelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[2]/div[1]/form/div[1]/div/div[2]/div/div[2]/div/div[2]/div[2]");
                ReportingSelect.Click();

                IWebElement SaveReporting = seleniumDriver.FindElementByXPath("//button[@type='submit']");
                SaveReporting.Click();

                seleniumDriver.GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/pim/viewEmployeeList");
                IWebElement ExpandSearch = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div[1]/div[1]/div[2]/div[3]/button");
                ExpandSearch.Click();

                IWebElement EmployementStatusAll = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div[1]/div[2]/form/div[1]/div/div[3]/div/div[2]/div/div");
                EmployementStatusAll.Click();

                IWebElement EmployementStatusAllSelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div[1]/div[2]/form/div[1]/div/div[3]/div/div[2]/div/div[2]/div[4]");
                EmployementStatusAllSelect.Click();

                IWebElement SupervisorNameSearch = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div[1]/div[2]/form/div[1]/div/div[5]/div/div[2]/div/div/input");
                SupervisorNameSearch.SendKeys("UserSupervisor");
                System.Threading.Thread.Sleep(5000);
                IWebElement SelectSupervisor = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div[1]/div[2]/form/div[1]/div/div[5]/div/div[2]/div/div[2]/div")));
                SelectSupervisor.Click();

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


                }
                else if (text == "")
                {
                    Random random = new Random();
                    int randomNumber = random.Next(100, 999);
                    input.SendKeys(randomNumber.ToString());
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
                    if (selectionText != "-- Select --")
                    {
                        option.Click();
                        break;
                    }
                }
            }
            IWebElement RadioButton = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[3]/div[2]/div[2]/div/div[2]/div[1]/div[2]/div/label/span");
            RadioButton.Click();

            ReadOnlyCollection<IWebElement> SaveButtons = driver.FindElements(By.XPath("//button[@type='submit']"));
            foreach (IWebElement button in SaveButtons)
            {
                string ButtonText = button.Text.ToString();
                if (ButtonText == "Save")
                {
                    button.Click();
                }
            }

            /*
            IWebElement OtherId = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[2]/div[1]/div[2]/div/div[2]/input");
            OtherId.SendKeys("1234");

            IWebElement DriverNumber = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[2]/div[2]/div[1]/div/div[2]/input");
            DriverNumber.SendKeys("1234");

            IWebElement ExpireDate = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[2]/div[2]/div[2]/div/div[2]/div/div/input");
            ExpireDate.SendKeys("2024-01-11");
            
            IWebElement Nationality = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[3]/div[1]/div[1]/div/div[2]/div/div");
            Nationality.Click();

            IWebElement NationalitySelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[3]/div[1]/div[1]/div/div[2]/div/div[2]/div[2]");
            NationalitySelect.Click();

            IWebElement MaritalStatus = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[3]/div[1]/div[2]/div/div[2]/div/div/div[1]");
            MaritalStatus.Click();

            IWebElement MaritalStatusSelect = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[3]/div[1]/div[2]/div/div[2]/div/div[2]/div[2]");
            MaritalStatusSelect.Click();

            IWebElement DateOfBirth = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[3]/div[2]/div[1]/div/div[2]/div/div/input");
            DateOfBirth.SendKeys("2002-01-11");

            IWebElement SelectGenderRadioButton = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[1]/form/div[3]/div[2]/div[2]/div/div[2]/div[1]/div[2]/div/label/span");
            SelectGenderRadioButton.Click();

            IWebElement SavePersonalDetails = seleniumDriver.FindElementByXPath("//div[@class='orangehrm-horizontal-padding orangehrm-vertical-padding']//button[@type='submit'][normalize-space()='Save']");
            SavePersonalDetails.Click();

            IWebElement BloodType = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[2]/div/form/div[1]/div/div[1]/div/div[2]/div/div/div[1]");
            BloodType.Click();

            IWebElement SelectBloodType = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[2]/div/form/div[1]/div/div[1]/div/div[2]/div/div[2]/div[2]");

            SelectBloodType.Click();

            IWebElement TestField = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[2]/div/form/div[1]/div/div[2]/div/div[2]/input");
            TestField.SendKeys("Test");

            IWebElement SaveMedical = seleniumDriver.FindElementByXPath("//div[@class='orangehrm-custom-fields']//button[@type='submit'][normalize-space()='Save']");
            SaveMedical.Click();
            */
            System.Threading.Thread.Sleep(1000);
            IWebElement AddAtachement = seleniumDriver.FindElementByXPath("//button[normalize-space()='Add']");

            AddAtachement.Click();
            IWebElement SendFile = seleniumDriver.FindElementByCssSelector("input[type='file']");

            OpenFileDialog openFileDialog = new OpenFileDialog();
            string FilePath = "";

            IWebElement SumbitFile = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[3]/div/form/div[3]/button[2]");

            for (int i = 0; i < 2; i++)
            {
                System.Threading.Thread.Sleep(5000);
                if (i == 1)
                {
                    AddAtachement = seleniumDriver.FindElementByXPath("//button[normalize-space()='Add']");
                    AddAtachement.Click();
                }
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = openFileDialog.FileName;
                }
                SendFile = seleniumDriver.FindElementByCssSelector("input[type='file']");
                SendFile.SendKeys(FilePath);
                System.Threading.Thread.Sleep(1000);
                SumbitFile = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[3]/div/form/div[3]/button[2]");
                SumbitFile.Click();




            }
            System.Threading.Thread.Sleep(3000);
            IWebElement DownloadButton = seleniumDriver.FindElementByXPath("//i[@class='oxd-icon bi-download']");
            DownloadButton.Click();
            System.Threading.Thread.Sleep(3000);
            IWebElement EditFile = seleniumDriver.FindElementByXPath("//i[@class='oxd-icon bi-pencil-fill']");
            System.Threading.Thread.Sleep(3000);
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = openFileDialog.FileName;
            }
            SendFile = seleniumDriver.FindElementByCssSelector("input[type='file']");
            SendFile.SendKeys(FilePath);
            SumbitFile = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/div/div[2]/div[3]/div/form/div[3]/button[2]");
            SumbitFile.Click();
            System.Threading.Thread.Sleep(3000);
            IWebElement DeleteFile = seleniumDriver.FindElementByXPath("//i[@class='oxd-icon bi-trash']");
            DeleteFile.Click();
        }
        public void ThirdScenario()
        {

            IWebElement AddCandidate = seleniumDriver.FindElementByXPath("//button[normalize-space()='Add']");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            ReadOnlyCollection<IWebElement> inputs = driver.FindElements(By.XPath("//input[@class='oxd-input oxd-input--active']"));

           
            //input[@placeholder='First Name']
            IWebElement FirstName = seleniumDriver.FindElementByXPath("//input[@placeholder='First Name']");
            FirstName.SendKeys("TestFirst");
            //input[@placeholder='Middle Name']
            IWebElement MiddleName = seleniumDriver.FindElementByXPath("//input[@placeholder='Middle Name']");
            MiddleName.SendKeys("TestMiddle");

            //input[@placeholder='Last Name']
            IWebElement LastName = seleniumDriver.FindElementByXPath("//input[@placeholder='Last Name']");
            LastName.SendKeys("TestLast");
            IWebElement Email = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[3]/div/div[1]/div/div[2]/input");
            Email.SendKeys("Test@test.com");
            
            IWebElement ContactNumber = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[3]/div/div[2]/div/div[2]/input");
            ContactNumber.SendKeys("1234567890");

            IWebElement Vacancy = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div/div/div[2]/div/div/div[1]");
            Vacancy.Click();

            System.Threading.Thread.Sleep(1000);
                ReadOnlyCollection<IWebElement> SelectOptions = driver.FindElements(By.XPath("//div[@role='option']"));
                foreach (IWebElement option in SelectOptions)
                {
                    string selectionText = option.Text.ToString();
                    if (selectionText != "-- Select --")
                    {
                        option.Click();
                        break;
                    }
                }
                IWebElement SendFile = seleniumDriver.FindElementByCssSelector("input[type='file']");
    
                OpenFileDialog openFileDialog = new OpenFileDialog();
                 string FilePath = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FilePath = openFileDialog.FileName;
            }
            SendFile.SendKeys(FilePath);

            IWebElement DateOfApplication = seleniumDriver.FindElementByXPath("//input[@placeholder='yyyy-dd-mm']");
                 DateOfApplication.SendKeys("2024-01-11");

            IWebElement Keywords = seleniumDriver.FindElementByXPath("//input[@placeholder='Enter comma seperated words...']");
            Keywords.SendKeys("Test,tas,tos,tis");

            IWebElement Notes = seleniumDriver.FindElementByXPath("//*[@id=\"app\"]/div[1]/div[2]/div[2]/div/div/form/div[6]/div/div/div/div[2]/textarea");
            Notes.SendKeys("TestNotes");
            IWebElement ConsentCheck = seleniumDriver.FindElementByXPath("//span[@class='oxd-checkbox-input oxd-checkbox-input--active --label-right oxd-checkbox-input']");
            ConsentCheck.Click();
            IWebElement SaveButton = seleniumDriver.FindElementByXPath("//button[@type='submit']");
            SaveButton.Click();

            //button[normalize-space()='Shortlist']
            //*[@id="app"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div/div/div[2]/textarea 
            //button[@type='submit']
            //button[normalize-space()='Schedule Interview']


            //*[@id="app"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div[1]/div/div[2]/input
            //input[@placeholder='yyyy-dd-mm']
            //input[@placeholder='hh:mm']
            //input[@placeholder='Type for hints...']
            //*[@id="app"]/div[1]/div[2]/div[2]/div/div/form/div[2]/div/div[5]/div/div[2]/textarea
            //button[@type='submit']
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
    }
}
