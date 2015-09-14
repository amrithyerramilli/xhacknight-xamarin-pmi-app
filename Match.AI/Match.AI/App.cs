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
        public static User _User;
        public static OAuthSettings _OAuthSettings;
        public static HomePage _HomePage;
        public static NavigationPage _NavPage;
        static string _Token;
        public static string Token
        {
            get { return _Token; }
        }

        public App()
        {
            // The root page of your application
            _OAuthSettings = new OAuthSettings(
                                    clientId: "",
                                    scope: "public_profile,email,user_friends,user_about_me,user_status",
                                    authorizeUrl: "https://m.facebook.com/dialog/oauth/",
                                    redirectUrl: "http://www.facebook.com/connect/login_success.html");

            _HomePage = new HomePage();
            _NavPage = new NavigationPage(_HomePage);

            MainPage = _NavPage;
        }

        public static Action SuccessfulLoginAction
        {
            get
            {
                return new Action(() =>
                {
                    _NavPage.Navigation.PopModalAsync();
                });
            }
        }
        public static void SaveToken(string token)
        {
            _Token = token;
            ApiClient.GetUserInfo();
            // broadcast a message that authentication was successful
            _HomePage.Notify("Authenticated");           
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
    }
}
