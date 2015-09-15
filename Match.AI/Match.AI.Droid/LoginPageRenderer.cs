using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Match.AI;
using Match.AI.Pages;
using Match.AI.Droid;
using Xamarin.Auth;
using Xamarin.Forms;


[assembly: ExportRenderer(typeof(Match.AI.Pages.LoginPage), typeof(LoginPageRenderer))]
namespace Match.AI.Droid
{
    public class LoginPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            // this is a ViewGroup - so should be able to load an AXML file and FindView<>
            var activity = this.Context as Activity;

            var auth = new OAuth2Authenticator(
                clientId: App.OAuthSettings.ClientId, // your OAuth2 client id
                scope: App.OAuthSettings.Scope, // the scopes for the particular API you're accessing, delimited by "+" symbols
                authorizeUrl: new Uri(App.OAuthSettings.AuthorizeUrl), // the auth URL for the service
                redirectUrl: new Uri(App.OAuthSettings.RedirectUrl)); // the redirect URL for the service

            auth.Completed += (sender, eventArgs) =>
            {
                if (eventArgs.IsAuthenticated)
                {
                    App.SuccessfulLoginAction.Invoke();
                    // Use eventArgs.Account to do wonderful things
                    App.SaveToken(eventArgs.Account.Properties["access_token"]);
                }
                else
                {
                    // The user cancelled
                    // TODO : Show appropriate error message. For now, closing the oAuth popup
                    App.SuccessfulLoginAction.Invoke();                    
                }
            };

            activity.StartActivity(auth.GetUI(activity));
        }
    }
}