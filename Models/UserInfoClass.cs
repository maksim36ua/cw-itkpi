using Microsoft.Data.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace cw_itkpi.Models
{
    public class UserInfo
    {
        private string nameId;
        private string clanName;
        private string vkPageLink;
        private int points;
        private int thisWeekRating;

        public UserInfo()
        {
            username = "";
        }

        public UserInfo(string name)
        {
            username = name;
        }

        [Key]
        public string username
        {
            get { return nameId; }
            set { nameId = value; }
        }

        public int honor
        {
            get { return points; }
            set { points = value; }
        }

        public int weeklyPoints
        {
            get { return thisWeekRating; }
            set { thisWeekRating = value; }
        }

        public string clan
        {
            get { return clanName; }
            set { clanName = value; }
        }

        [JsonIgnore]
        public string vkLink
        {
            get { return vkPageLink; }
            set { vkPageLink = value; }
        }

        public string RetrieveValues()
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

            var json = GetJsonResponse(username).Result;

            if (string.IsNullOrEmpty(json))
            {
                return "";
            }
            else
            {
                var jsonObject = JsonConvert.DeserializeObject<UserInfo>(json, jsonSerializerSettings);

                honor = jsonObject.honor;
                clan = jsonObject.clan;
                return "Ok";
            }
        }


        private async Task<string> GetJsonResponse(string username)
        {
            using (var request = new HttpClient())
            {
                request.DefaultRequestHeaders.Add("Authorization", "c7T7urhMy8ycrT7TxTsC");
                var baseUri = "https://www.codewars.com/api/v1/users/" + username;
                request.BaseAddress = new Uri(baseUri);

                var response = await request.GetAsync(baseUri);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }

                else
                    return "";
            }
        }

        public void ClearVkLink()
        {
            if (!string.IsNullOrEmpty(vkLink))
            {
                var lastIndex = vkLink.LastIndexOf('/');
                if (lastIndex != -1)
                {
                    vkLink = vkLink.Substring(lastIndex + 1, vkLink.Length - (lastIndex + 1));
                }
                else
                    vkLink = "";
            }
        }
    }
}
