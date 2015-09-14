using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Match.AI.Pages
{
    public class ProfilePage : ContentPage
    {
        public User myUser;
        public ProfilePage()
        {
            var nameLabel = new Label();
            //myUser = App._User.name
            //nameLabel.Text = App._User != null ? App._User.name : "Not ready yet. Please go back try again in a little while.";
            nameLabel.SetBinding(Label.TextProperty, "name");
            nameLabel.BindingContext = App._User;
            Content = new StackLayout()
            {
                Children = {
                        nameLabel
                    }
            };

        }
    }
}
