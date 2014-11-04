using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Windows.Data.Json;
using Instagram.DAL.Model;

namespace Instagram.DAL.DataSource
{
    public abstract class DataParser
    {
        protected Pagination GetPagination(JsonObject paginationObject)
        {
            if (paginationObject.ContainsKey("next_url") && paginationObject.ContainsKey("next_max_id"))
            {
                var nextUrl = paginationObject["next_url"].GetString();
                var nextMaxId = paginationObject["next_max_id"].GetString();

                return new Pagination(nextUrl, nextMaxId);
            }

            return new Pagination();
        }

        protected Meta GetMeta(JsonObject metaObject)
        {
            int code = (int)metaObject["code"].GetNumber();

            if (code == 200)
            {
                return new Meta(code);
            }

            string errorType = metaObject["error_type"].GetString();
            string errorMessage = metaObject["error_message"].GetString();

            return new Meta(code, errorType, errorMessage);
        }

        protected Tags GetTags(JsonArray tagsArray)
        {
            if (tagsArray.Count == 0)
            {
                return null;
            }

            List<Tag> tags = new List<Tag>();
            foreach (var tag in tagsArray)
            {
                tags.Add(new Tag(tag.GetString()));
            }

            return new Tags(tags);
        }

        protected MediaType GetType(string type)
        {
            if (type.Equals("video"))
            {
                return MediaType.Video;
            }

            return MediaType.Image;
        }

        protected Location GetLocation(IJsonValue locationValue)
        {
            if (locationValue.ValueType == JsonValueType.Null)
            {
                return null;
            }

            var locationObject = locationValue.GetObject();

            double latitude = 0;
            double longitude = 0;

            if (locationObject.ContainsKey("latitude") && locationObject.ContainsKey("longitude"))
            {
                latitude = locationObject["latitude"].GetNumber();
                longitude = locationObject["longitude"].GetNumber();
            }

            if (locationObject.ContainsKey("name") && locationObject.ContainsKey("id"))
            {
                var name = locationObject["name"].GetString();
                var id = locationObject["id"].GetNumber();

                return new Location(latitude, longitude, name, id);
            }

            if (locationObject.ContainsKey("id"))
            {
                var id = locationObject["id"].GetNumber();

                return new Location(id);
            }

            return new Location(latitude, longitude);
        }

        protected Comments GetComments(JsonObject commentsObject)
        {
            int count = (int)commentsObject["count"].GetNumber();

            JsonArray commentsArray = commentsObject["data"].GetArray();

            ObservableCollection<Comment> comments = new ObservableCollection<Comment>();

            foreach (JsonValue commentValue in commentsArray)
            {
                var commentObject = commentValue.GetObject();
                var createdTime = commentObject["created_time"].GetString();
                var text = commentObject["text"].GetString();
                var id = commentObject["id"].GetString();
                var from = GetUser(commentObject["from"].GetObject());

                comments.Add(new Comment(createdTime, text, from, id));
            }

            return new Comments(count, comments);
        }

        protected Likes GetLikes(JsonObject likesObject)
        {
            int count = (int)likesObject["count"].GetNumber();

            JsonArray likesArray = likesObject["data"].GetArray();

            ObservableCollection<User> liked = new ObservableCollection<User>();
            foreach (JsonValue likeValue in likesArray)
            {
                var likeObject = likeValue.GetObject();
                var user = GetUser(likeObject);
                liked.Add(user);
            }

            return new Likes(count, liked);
        }

        protected Images GetImages(JsonObject imagesObject)
        {
            var standardObject = imagesObject["standard_resolution"].GetObject();
            StandardResolution standardResolution = new StandardResolution(standardObject["url"].GetString(), standardObject["width"].GetNumber(), standardObject["height"].GetNumber());

            var lowObject = imagesObject["low_resolution"].GetObject();
            LowResolution lowResolution = new LowResolution(lowObject["url"].GetString(), lowObject["width"].GetNumber(), lowObject["height"].GetNumber());

            var thumbObject = imagesObject["thumbnail"].GetObject();
            Thumbnail thumbnail = new Thumbnail(thumbObject["url"].GetString(), thumbObject["width"].GetNumber(), thumbObject["height"].GetNumber());

            return new Images(standardResolution, lowResolution, thumbnail);
        }

        protected Videos GetVideos(JsonObject videoObject)
        {
            var standardObject = videoObject["standard_resolution"].GetObject();
            StandardResolution standardResolution = new StandardResolution(standardObject["url"].GetString(), standardObject["width"].GetNumber(), standardObject["height"].GetNumber());

            var lowObject = videoObject["low_resolution"].GetObject();
            LowResolution lowResolution = new LowResolution(lowObject["url"].GetString(), lowObject["width"].GetNumber(), lowObject["height"].GetNumber());

            var thumbObject = videoObject["low_bandwidth"].GetObject();
            LowBandwidth lowBandwidth = new LowBandwidth(thumbObject["url"].GetString(), thumbObject["width"].GetNumber(), thumbObject["height"].GetNumber());

            return new Videos(lowBandwidth, lowResolution, standardResolution);
        }

        protected UsersInPhoto GetUsersInPhoto(JsonArray usersInPhotoArray)
        {
            ObservableCollection<UserInPhoto> users = new ObservableCollection<UserInPhoto>();

            foreach (var userInPhotoValue in usersInPhotoArray)
            {
                var userInPhotoObject = userInPhotoValue.GetObject();

                Position position = new Position(userInPhotoObject["position"].GetObject()["x"].GetNumber(), userInPhotoObject["position"].GetObject()["y"].GetNumber());
                User user = GetUser(userInPhotoObject["user"].GetObject());
            }

            return new UsersInPhoto(users);
        }

        protected Caption GetCaption(IJsonValue captionValue)
        {
            if (captionValue.ValueType == JsonValueType.Null)
            {
                return null;
            }

            var captionObject = captionValue.GetObject();
            var createdTime = captionObject["created_time"].GetString();
            var text = captionObject["text"].GetString();
            var from = GetUser(captionObject["from"].GetObject());
            var id = captionObject["id"].GetString();

            return new Caption(createdTime, text, from, id);
        }

        protected User GetUser(JsonObject userObject)
        {
            var username = userObject["username"].GetString();
            var profilePicture = userObject["profile_picture"].GetString();
            var id = userObject["id"].GetString();
            var fullName = userObject["full_name"].GetString();

            return new User(username, profilePicture, fullName, id);
        }

        protected Counts GetCounts(JsonObject countsObject)
        {
            var media = (int)countsObject["media"].GetNumber();
            var follows = (int)countsObject["follows"].GetNumber();
            var followedBy = (int)countsObject["followed_by"].GetNumber();

            return new Counts(media, follows, followedBy);
        }
    }
}
