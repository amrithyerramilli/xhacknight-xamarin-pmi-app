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
        public ProfilePage()
        {
            // set the binding context for the entire page to the App user
            BindingContext = App.User;

            // design the profile page layout
            // based on : https://www.syntaxismyui.com/xamarin-forms-in-anger-phoenix-peaks/

            AbsoluteLayout peakLayout = new AbsoluteLayout
            {
                HeightRequest = 250,
                BackgroundColor = Color.Black
            };

            var title = new Label
            {
                //Text = "South Mountain",
                FontSize = 30,
                FontFamily = "AvenirNext-DemiBold",
                TextColor = Color.White
            };
            title.SetBinding(Label.TextProperty, "name");

            var where = new Label
            {
                Text = "Bengaluru, India",
                TextColor = Color.FromHex("#ddd"),
                FontFamily = "AvenirNextCondensed-Medium"
            };
            //where.SetBinding(Label.TextProperty, "name");

            var image = new Image()
            {
                //Source = ImageSource.FromFile("southmountain.jpg"),
                Aspect = Aspect.AspectFill,
            };
            image.SetBinding(Image.SourceProperty, "profilePicture");

            var overlay = new BoxView()
            {
                Color = Color.Black.MultiplyAlpha(.7f)
            };

            var pin = new Image()
            {
                //Source = ImageSource.FromFile("pin.png"),
                HeightRequest = 25,
                WidthRequest = 25
            };

            var descriptionLabel = new Label()
            {
                FontSize = 14,
                TextColor = Color.FromHex("#ddd"),
            };
            descriptionLabel.SetBinding(Label.TextProperty, "bio");

            var traitsLayout = new StackLayout();
            traitsLayout.Orientation = StackOrientation.Vertical;
            traitsLayout.Children.Add(descriptionLabel);
            
            for (int i = 0; i < App.User.PersonalityTraits.Count; i++)
            {
                var trait = App.User.PersonalityTraits[i];
                var nameLabel = new Label();
                nameLabel.FontSize = 14;
                nameLabel.TextColor = Color.Gray;
                nameLabel.SetBinding(Label.TextProperty, "Name");
                nameLabel.BindingContext = trait;
                
                var valueLabel = new Label();
                valueLabel.FontSize = 16;
                valueLabel.TextColor = Color.White;
                valueLabel.SetBinding(Label.TextProperty, "Value");
                valueLabel.BindingContext = trait;

                traitsLayout.Children.Add(nameLabel);
                traitsLayout.Children.Add(valueLabel);

            }
            var description = new Frame()
            {
                Padding = new Thickness(10, 5),
                HasShadow = false,
                BackgroundColor = Color.Transparent,
                Content = traitsLayout
            };

            AbsoluteLayout.SetLayoutFlags(overlay, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(overlay, new Rectangle(0, 1, 1, 0.3));

            AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(image, new Rectangle(0f, 0f, 1f, 1f));

            AbsoluteLayout.SetLayoutFlags(title, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(title,
                new Rectangle(0.1, 0.85, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
                );

            AbsoluteLayout.SetLayoutFlags(where, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(where,
                new Rectangle(0.1, 0.95, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
                );

            AbsoluteLayout.SetLayoutFlags(pin, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(pin,
                new Rectangle(0.95, 0.9, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize)
                );

            peakLayout.Children.Add(image);
            peakLayout.Children.Add(overlay);
            peakLayout.Children.Add(title);
            peakLayout.Children.Add(where);
            peakLayout.Children.Add(pin);

            Content = new StackLayout()
            {
                BackgroundColor = Color.FromHex("#333"),
                Children =
                {
                    peakLayout,
                    description,
                }
            };
        }
    }
}
