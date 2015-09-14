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
            //Content = new StackLayout(){
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    Spacing = 15,
            //    Orientation = StackOrientation.Vertical,
            //    Padding = new Thickness (0, 20, 0, 0),
            //    Children = {button1, button2, button3, listView }
            //}
            //MessagingCenter.Subscribe<HomePage>(this, "Ready", (sender) =>
            //{
                
            //});
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
            MessagingCenter.Send<HomePage>(this, message);
        }
    }
}
