using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Instagram.DAL.Model;
using Windows.Data.Json;
using Windows.Storage;
using Instagram.DAL.DataSource;
using Windows.UI.Popups;
using Instagram.DAL.Net;
using Instagram.DAL.Storage.Model;

namespace Instagram.DAL.DataSource
{
    public class FeedSource : DataParser
    {
        private static FeedSource _feedSource = new FeedSource();

        private Feed Feed = new Feed();

        public static async Task<Feed> GetFeedAsync(bool refresh)
        {
            if (refresh)
            {
                _feedSource = new FeedSource();
            }

            await _feedSource.GetDataAsync();

            return _feedSource.Feed;
        }

        private async Task GetDataAsync()
        {
            Account account = await new Storage.Storage().GetActiveAccountAsync();
            string feedUri = "https://api.instagram.com/v1/users/self/feed?access_token=" + account.Token;
            
            if (Feed.Pagination.HasMorePages)
            {
                feedUri = Feed.Pagination.NextUrl;
            }

            //feedUri = new Uri("ms-appx:///DataSource/Feed.json");

            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(feedUri);

            //string jsonText = await FileIO.ReadTextAsync(file);

            string jsonText = await new Client().JsonObjectAsync(feedUri);

            var jsonObject = JsonObject.Parse(jsonText);
            var jsonArray = jsonObject["data"].GetArray();
            
            Feed.Pagination = GetPagination(jsonObject["pagination"].GetObject());

            foreach (JsonValue feedItem in jsonArray)
            {
                var itemObject = feedItem.GetObject();

                //await new MessageDialog(itemObject["type"].GetString()).ShowAsync();
                var Type = GetType(itemObject["type"].GetString());
                //MediaType Type = MediaType.Image;
                var user = GetUser(itemObject["user"].GetObject());
                //User user = null;
                var tags = GetTags(itemObject["tags"].GetArray());
                //Tags tags = null;
                var location = GetLocation(itemObject["location"]);
                //Location location = null;
                var comments = GetComments(itemObject["comments"].GetObject());
                //Comment comments = null;
                var filter = itemObject["filter"].GetString();
                //string filter = "";
                var createdTime = itemObject["created_time"].GetString();
                //string createdTime = "";
                var link = itemObject["link"].GetString();
                //string link = "";
                var likes = GetLikes(itemObject["likes"].GetObject());
                //Likes likes = null;
                var images = GetImages(itemObject["images"].GetObject());
                //Images images = null;
                var usersInPhoto = GetUsersInPhoto(itemObject["users_in_photo"].GetArray());
                //UsersInPhoto usersInPhoto = null;
                var caption = GetCaption(itemObject["caption"]);
                //Caption caption = null;
                var userhasLiked = itemObject["user_has_liked"].GetBoolean();
                //bool userhasLiked = false;
                var id = itemObject["id"].GetString();
                //string id = "";
                
                FeedItem item;

                if (Type == MediaType.Video)
                {
                    var videos = GetVideos(itemObject["videos"].GetObject());

                    item = new FeedItem(null, tags, Type, location, comments, filter, createdTime, link, likes, images, usersInPhoto, caption, userhasLiked, id, user, videos);
                }
                else
                {
                    item = new FeedItem(null, tags, Type, location, comments, filter, createdTime, link, likes, images, usersInPhoto, caption, userhasLiked, id, user);
                }

                Feed.Items.Add(item);
            }
        }
    }
}
