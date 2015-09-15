using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Match.AI.Pages;
namespace Match.AI
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<HomePage>(this, "Ready", (sender) =>
            {
                //http://stackoverflow.com/questions/30241334/animators-may-only-be-run-on-looper-threads-during-on-device-intrumentation-te
                Device.BeginInvokeOnMainThread(() =>
                {
                    profileButton.IsEnabled = true;
                    reviewsButton.IsEnabled = true;
                });
            });
        }

        void OnLogin(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new LoginPage());
        }
        void OnProfile(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }
        void OnReviews(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ReviewsPage());
        }
        public void Notify(string message)
        {
            MessagingCenter.Send(this, message);
        }
    }
}
