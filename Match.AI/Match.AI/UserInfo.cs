using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match.AI
{

    public class Datum
    {
        public string id { get; set; }
        public string message { get; set; }
    }

    public class Paging
    {
        public string previous { get; set; }
        public string next { get; set; }
    }

    public class Posts
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }

    public class Data
    {
        public bool is_silhouette { get; set; }
        public string url { get; set; }
    }

    public class Picture
    {
        public Data data { get; set; }
    }

    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public Posts posts { get; set; }
        public string bio { get; set; }
        public Picture picture { get; set; }
        public Xamarin.Forms.ImageSource profilePicture
        {
            get
            {
                if (picture != null && picture.data != null && picture.data.url != null)
                    return Xamarin.Forms.ImageSource.FromUri(new Uri(picture.data.url));
                return null;
            }
        }
        public string personality { get; set; }
    }


}
