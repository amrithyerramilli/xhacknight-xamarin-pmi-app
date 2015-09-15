using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Match.AI.Pages
{
    public class RatingDetailPage : ContentPage
    {
        public RatingDetailPage(Movie selectedMovie)
        {
            var vetlist = new ListView()
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(RatingDetailCell)),
                ItemsSource = selectedMovie.Ratings
            };
            //disable selection
            vetlist.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
            //vetlist.ItemTapped += OnTap;

            var layout = new StackLayout
            {
                Children = {
                    //searchBar,
					vetlist
				}
            };
            Content = layout;

            BindingContext = selectedMovie.Ratings;
        }
    }
    public class RatingDetailCell : ViewCell
    {
        public RatingDetailCell()
        {
            var vetProfileImage = new Image { Aspect = Aspect.AspectFill, HeightRequest = 50, WidthRequest = 50 };
            vetProfileImage.SetBinding(Image.SourceProperty, "User.profilePicture");

            var nameLabel = new Label()
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = 18,
                TextColor = Color.Black
            };
            nameLabel.SetBinding(Label.TextProperty, "User.name");

            var distanceLabel = new Label()
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 12,
                TextColor = Color.FromHex("#666")
            };
            distanceLabel.SetBinding(Label.TextProperty, "User.bio");
            var onLineLabel = new Label()
            {
                TextColor = Color.FromHex("#F2995D"),
                FontSize = 12,
            };
            onLineLabel.SetBinding(Label.TextProperty, new Binding("User.MatchData[0].Name", stringFormat: "PMI : {0}"));

            var offLineLabel = new Label()
            {
                TextColor = Color.FromHex("#ddd"),
                FontSize = 12,
            };
            offLineLabel.SetBinding(Label.TextProperty, "User.MatchData[0].Value");

            // Vet rating label and star image
            var starLabel = new Label()
            {
                FontSize = 12,
                TextColor = Color.Gray
            };
            starLabel.SetBinding(Label.TextProperty, "Value");

            var starImage = new Image()
            {
                Source = ImageSource.FromUri(new Uri("https://cdn4.iconfinder.com/data/icons/small-n-flat/24/star-128.png")),
                HeightRequest = 12,
                WidthRequest = 12
            };

            var ratingStack = new StackLayout()
            {
                Spacing = 3,
                Orientation = StackOrientation.Horizontal,
                Children = { starImage, starLabel }
            };

            var statusLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Children = { distanceLabel, onLineLabel, offLineLabel }
            };

            var vetDetailsLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 0, 0),
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { nameLabel, statusLayout, ratingStack }
            };

            var cellLayout = new StackLayout
            {
                Spacing = 0,
                Padding = new Thickness(10, 5, 10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { vetProfileImage, vetDetailsLayout }
            };

            this.View = cellLayout;
        }

    }
}
