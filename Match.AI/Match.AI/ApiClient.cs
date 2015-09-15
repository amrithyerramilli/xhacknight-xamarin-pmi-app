﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Match.AI
{
    public static class ApiClient
    {
        public static async void GetUserInfo()
        {
            var client = new HttpClient();
            var access_token = App.Token;

            string fields = "id,name,posts.limit(200){message},about,bio,picture,cover";
            string apiVersion = "v2.4";
            var apiEndpointUrl = string.Format("https://graph.facebook.com/{0}/me?fields={1}&access_token={2}", apiVersion, fields, access_token);

            var getUserDetailsTask = await client.GetAsync(apiEndpointUrl).ConfigureAwait(false);
            if (getUserDetailsTask.IsSuccessStatusCode)
            {
                var responseJsonString = await getUserDetailsTask.Content.ReadAsStringAsync();
                var jsonData = JsonConvert.DeserializeObject<User>(responseJsonString);
                App.User = jsonData;

                // Make API call to fetch user's personality insights
                var values = new List<KeyValuePair<string, string>>();
                //TODO : generate the text from the user's post's message data. Clean it up to remove invalid characters and send to bluemix.
                List<string> messages = new List<string>();
                if (App.User.posts != null)
                {
                    foreach (var post in App.User.posts.data)
                    {
                        string onlyAscii = post.message;
                        try
                        {
                            if (!String.IsNullOrEmpty(post.message))
                                onlyAscii = Regex.Replace(post.message, @"[^\u0000-\u007F]", string.Empty, RegexOptions.None, TimeSpan.FromSeconds(1));
                        }
                        catch (RegexMatchTimeoutException)
                        {

                            //throw;
                        }

                        //var moreCleanup = Regex.Replace(onlyAscii, @"[^\w\.@-\\%]", String.Empty);
                        if (!String.IsNullOrEmpty(onlyAscii))
                            messages.Add(onlyAscii);
                    }
                }
                var bio = App.User.bio ?? "";
                var combined = bio + "." + String.Join(". ", messages);
                if (combined.Length < 100)
                {

                }
                values.Add(new KeyValuePair<string, string>("text", combined));

                var content = new FormUrlEncodedContent(values);
                var client2 = new HttpClient();
                var response = await client2.PostAsync("http://xlab.mybluemix.net/map", content).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var jsonOut = await response.Content.ReadAsStringAsync();
                    var y = JsonConvert.DeserializeObject(jsonOut);
                    App.User.personality = y.ToString();

                    // Make API call to do bulk match of personalities and retreive the PMI
                    var client3 = new HttpClient();
                    var matchResponse = await client3.PostAsync("http://xlab.mybluemix.net/bulk_match", content).ConfigureAwait(false);
                    if (matchResponse.IsSuccessStatusCode)
                    {
                        var matchDataString = await matchResponse.Content.ReadAsStringAsync();
                        JArray matchDataJson = (JArray)JsonConvert.DeserializeObject(matchDataString);
                        for (int i = 0; i < matchDataJson.Count; i++)
                        {
                            var j = JObject.FromObject(matchDataJson[i]);
                            var matches = new List<Blah>();
                            foreach (KeyValuePair<string, JToken> pair in j)
                            {
                                matches.Add(new Blah(){Name = pair.Key, Value = pair.Value.Value<String>()});
                            }
                            App.AppUsers[i].MatchData = matches;
                        }
                        App.HomePage.Notify("Ready");
                    }
                }
            }
        }
    }
}
