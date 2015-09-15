using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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

        public List<Blah> PersonalityTraits
        {
            get
            {
                var l = new List<Blah>();
                JArray p = null;
                if (!String.IsNullOrEmpty(personality))
                {
                    var x = JObject.Parse(personality);
                    var big5 = JArray.FromObject(x["tree"]["children"][0]["children"][0]["children"]);
                    p = big5;
                }

                if (p != null)
                {
                    foreach (var trait in p)
                    {
                        var x = new Blah();
                        x.Name = trait["name"].Value<string>();
                        x.Value =
                            Math.Round(trait["percentage"].Value<decimal>()*100, 2, MidpointRounding.AwayFromZero)
                                .ToString();
                        l.Add(x);
                    }
                }
                return l;
            }
        }
        public List<Blah> MatchData { get; set; } 
    }

    public class Blah
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }


}
