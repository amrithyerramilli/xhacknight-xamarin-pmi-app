using System;
using System.Collections.Generic;
using System.Text;
using Match.AI.iOS;
using Match.AI.Pages;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(LoginPage), typeof(LoginPageRenderer))]

namespace Match.AI.iOS
{
    public class LoginPageRenderer : PageRenderer
    {
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            var auth = new OAuth2Authenticator(
                clientId: App._OAuthSettings.ClientId, // your OAuth2 client id
                scope: App._OAuthSettings.Scope, // the scopes for the particular API you're accessing, delimited by "+" symbols
                authorizeUrl: new Uri(App._OAuthSettings.AuthorizeUrl), // the auth URL for the service
                redirectUrl: new Uri(App._OAuthSettings.RedirectUrl)); // the redirect URL for the service

            auth.Completed += (sender, eventArgs) =>
            {
                // We presented the UI, so it's up to us to dimiss it on iOS.
                App.SuccessfulLoginAction.Invoke();

                if (eventArgs.IsAuthenticated)
                {
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

            PresentViewController(auth.GetUI(), true, null);
        }
    }
}

