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

        private bool IsShown;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            // Fixed the issue that on iOS 8, the modal wouldn't be popped.
            // url : http://stackoverflow.com/questions/24105390/how-to-login-to-facebook-in-xamarin-forms
            if (!IsShown)
            {

                IsShown = true;

                var auth = new OAuth2Authenticator(
                    clientId: App._OAuthSettings.ClientId, // your OAuth2 client id
                    scope: App._OAuthSettings.Scope,
                    // The scopes for the particular API you're accessing. The format for this will vary by API.
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
                    }
                };

                PresentViewController(auth.GetUI(), true, null);

            }

        }
    }
}

