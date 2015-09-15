using Match.AI.Pages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace Match.AI
{
    public class App : Application
    {
        public static User User;
        public static OAuthSettings OAuthSettings;
        public static HomePage HomePage;
        public static NavigationPage NavPage;
        public static List<User> AppUsers; 

        public static string Token { get; private set; }

        public App()
        {
            // The root page of your application
            OAuthSettings = new OAuthSettings(
                                    clientId: "",
                                    scope: "public_profile,email,user_friends,user_about_me,user_status",
                                    authorizeUrl: "https://m.facebook.com/dialog/oauth/",
                                    redirectUrl: "http://www.facebook.com/connect/login_success.html");
            AppUsers = InitializeUsers();

            HomePage = new HomePage();
            NavPage = new NavigationPage(HomePage);

            MainPage = NavPage;
        }

        public static Action SuccessfulLoginAction
        {
            get
            {
                return () =>
                {
                    NavPage.Navigation.PopModalAsync();
                };
            }
        }
        public static void SaveToken(string token)
        {
            Token = token;
            ApiClient.GetUserInfo();
            // broadcast a message that authentication was successful
            HomePage.Notify("Authenticated");           
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        public List<User> InitializeUsers()
        {
            List<User> appUsers = new List<User>();
            var baseUrl = "https://s3.amazonaws.com/rs.adx.io/test/";
            appUsers.Add(new User()
            {
                name = "Steve Jobs",
                picture = new Picture() { data = new Data() { url = baseUrl + "john.png" } },
                personality = "", // TODO : get actual personality
            });
            appUsers.Add(new User()
            {
                name = "Marilyn Monroe",
                picture = new Picture() { data = new Data() { url = baseUrl + "marlyn.png" } },
                personality = "", // TODO : get actual personality
            });
            appUsers.Add(new User()
            {
                name = "Hitler",
                picture = new Picture() { data = new Data() { url = baseUrl + "hitler.png" } },
                personality = "", // TODO : get actual personality
            });
            appUsers.Add(new User()
            {
                name = "Narendra Modi",
                picture = new Picture() { data = new Data() { url = baseUrl + "namo.png" } },
                personality = "", // TODO : get actual personality
            });
            appUsers.Add(new User()
            {
                name = "Barack Obama",
                picture = new Picture() { data = new Data() { url = baseUrl + "obama.png" } },
                personality = "", // TODO : get actual personality
            });
            appUsers.Add(new User()
            {
                name = "Michelle Obama",
                picture = new Picture() { data = new Data() { url = baseUrl + "michelle.png" } },
                personality = "", // TODO : get actual personality
            });

            return appUsers;
        } 
    }
}
