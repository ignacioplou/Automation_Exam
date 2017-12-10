using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automation_Exam.Providers
{
    public class WebDriverFactory
    {
        public IWebDriver Create()
        {
            IWebDriver _webDriver = null;
            var chromeOptions = new ChromeOptions();

            chromeOptions.AddArguments("chrome.switches", "--disable-extensions --disable-extensions-file-access-check --disable-extensions-http-throttling --disable-infobars --enable-automation --start-maximized");
            chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            _webDriver = new ChromeDriver(chromeOptions);

            return _webDriver;
        }
    }
}
