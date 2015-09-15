using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Match.AI.Pages
{
    public class ReviewsPage : ContentPage
    {
        public ReviewsPage()
        {
            var movies = InitializeMovies();

            // Layout inspired by : https://www.syntaxismyui.com/xamarin-forms-in-anger-cards/
            var vetlist = new ListView()
            {
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(typeof(VetCell)),
                ItemsSource = movies
            };
            vetlist.ItemSelected += OnSelection;
            //vetlist.ItemTapped += OnTap;

            var layout = new StackLayout
            {
                Children = {
                    //searchBar,
					vetlist
				}
            };
            
            Content = layout;
            BindingContext = movies;
        }
        void OnSelection(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            App.NavPage.Navigation.PushAsync(new RatingDetailPage((Movie)e.SelectedItem));

            //comment out if you want to keep selections
            ListView lst = (ListView)sender;
            lst.SelectedItem = null;
        }

        List<Movie> InitializeMovies()
        {
            List<Movie> movies = new List<Movie>();
            movies.Add(new Movie()
            {
                Name = "The Shawshank Redemption",
                Description =
                    "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                Ratings = GetRandomMovieRatings(),
                Poster = "http://s1.evcdn.com/images/block/movies/3725/3725_af.jpg"
            });
            movies.Add(new Movie()
            {
                Name = "The Godfather",
                Description =
                    "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                Ratings = GetRandomMovieRatings(),
                Poster = "http://rocketdock.com/images/screenshots/godfather_rund2.png"
            });
            movies.Add(new Movie()
            {
                Name = "The Godfather 2",
                Description =
                    "The compelling sequel to \"The Godfather,\" contrasting the life of Corleone father and son.",
                Ratings = GetRandomMovieRatings(),
                Poster = "http://orig05.deviantart.net/a5ac/f/2013/214/b/d/the_godfather_ii_icon_by_fatboynate2-d6ga9qc.png"
            });
            movies.Add(new Movie()
            {
                Name = "The Dark Knight",
                Description =
                    "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, the caped crusader must come to terms with one of the greatest psychological tests of his ability to fight injustice.",
                Ratings = GetRandomMovieRatings(),
                Poster = "http://www.fantasyfolder.com/wp-content/uploads/2011/01/batman-tdk-logo.jpg"
            });
            movies.Add(new Movie()
            {
                Name = "12 Angry Men",
                Description =
                    "A dissenting juror in a murder trial slowly manages to convince the others that the case is not as obviously clear as it seemed in court.",
                Ratings = GetRandomMovieRatings(),
                Poster = "http://icons.iconarchive.com/icons/firstline1/movie-mega-pack-4/256/12-Angry-Men-icon.png"
            });
            movies.Add(new Movie()
            {
                Name = "Fight Club",
                Description =
                    "An insomniac office worker, looking for a way to change his life, crosses paths with a devil-may-care soap maker, forming an underground fight club that evolves into something much, much more...",
                Ratings = GetRandomMovieRatings(),
                Poster = "http://files.gamebanana.com/img/ico/sprays/fightclubstencil.png"
            });

            return movies;
        }

        List<MovieRating> GetRandomMovieRatings()
        {
            var x = new List<MovieRating>();
            if (App.AppUsers != null)
                x.AddRange(App.AppUsers.Select(appUser => new MovieRating() { User = appUser, Value = new Random().Next(1, 5) }));
            return x;
        }
    }



    public class Movie
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public string ShortDescription
        {
            get { return Description.Length > 80 ? Description.Substring(0, 80) + "..." : Description; }
        }
        public string Poster { get; set; }
        public Xamarin.Forms.ImageSource PosterImageSource
        {
            get
            {
                if (!String.IsNullOrEmpty(Poster))
                    return Xamarin.Forms.ImageSource.FromUri(new Uri(Poster));
                return null;
            }
        }
        public List<MovieRating> Ratings { get; set; }
        public string AverageRating
        {
            get
            {
                var x = Ratings.Select(e => e.Value).Average();
                return Math.Round(x, 2, MidpointRounding.AwayFromZero).ToString();
            }
        }

    }

    public class MovieRating
    {
        public decimal Value { get; set; }
        public User User { get; set; }
    }

    public class VetCell : ViewCell
    {
        public VetCell()
        {
            var vetProfileImage = new Image { Aspect = Aspect.AspectFill, HeightRequest = 50, WidthRequest = 50 };
            vetProfileImage.SetBinding(Image.SourceProperty, "PosterImageSource");

            var nameLabel = new Label()
            {
                FontFamily = "HelveticaNeue-Medium",
                FontSize = 18,
                TextColor = Color.Black
            };
            nameLabel.SetBinding(Label.TextProperty, "Name");

            var distanceLabel = new Label()
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 12,
                TextColor = Color.FromHex("#666")
            };
            distanceLabel.SetBinding(Label.TextProperty, "ShortDescription");

            // Online image and label
            //var onlineImage = new Image()
            //{
            //    Source = "online.png",
            //    HeightRequest = 8,
            //    WidthRequest = 8
            //};
            //onlineImage.SetBinding(Image.IsVisibleProperty, "ShouldShowAsOnline");
            //var onLineLabel = new Label()
            //{
            //    Text = "Online",
            //    TextColor = Color.FromHex("#F2995D"),
            //    FontSize = 12,
            //};
            //onLineLabel.SetBinding(Label.IsVisibleProperty, "ShouldShowAsOnline");

            // Offline image and label
            //var offlineImage = new Image()
            //{
            //    Source = "offline.png",
            //    HeightRequest = 8,
            //    WidthRequest = 8
            //};
            //offlineImage.SetBinding(Image.IsVisibleProperty, "ShouldShowAsOffline");
            //var offLineLabel = new Label()
            //{
            //    Text = "5 hours ago",
            //    TextColor = Color.FromHex("#ddd"),
            //    FontSize = 12,
            //};
            //offLineLabel.SetBinding(Label.IsVisibleProperty, "ShouldShowAsOffline");

            // Vet rating label and star image
            var starLabel = new Label()
            {
                FontSize = 12,
                TextColor = Color.Gray
            };
            starLabel.SetBinding(Label.TextProperty, "AverageRating");

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
                Children = { distanceLabel }
            };

            var vetDetailsLayout = new StackLayout
            {
                Padding = new Thickness(10, 0, 0, 0),
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { nameLabel, statusLayout, ratingStack }
            };

            var tapImage = new Image()
            {
                Source = ImageSource.FromUri(new Uri("https://cdn4.iconfinder.com/data/icons/ionicons/512/icon-chevron-right-128.png")),
                HorizontalOptions = LayoutOptions.End,
                HeightRequest = 12,
            };

            var cellLayout = new StackLayout
            {
                Spacing = 0,
                Padding = new Thickness(10, 5, 10, 5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children = { vetProfileImage, vetDetailsLayout, tapImage }
            };

            this.View = cellLayout;

        }

    }


}
