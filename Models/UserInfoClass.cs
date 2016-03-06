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
        private string _username;
        private string _clan;
        private string _vkLink;
        private string _pointsHistory;
        private int _honor;
        private int _thisWeekHonor;
        private int _lastWeekHonor;


        public UserInfo()
        {
            username = "";
            pointsHistory = "0";
            lastWeekHonor = 0;
        }

        public UserInfo(string name)
        {
            username = name;
            pointsHistory = "0";
            lastWeekHonor = 0;
        }

        [Key]
        public string username
        {
            get { return _username; }
            set { _username = value; }
        }

        public int honor
        {
            get { return _honor; }
            set { _honor = value; }
        }

        public int thisWeekHonor
        {
            get { return _thisWeekHonor; }
            set { _thisWeekHonor = value; }
        }

        public int lastWeekHonor
        {
            get { return _lastWeekHonor; }
            set { _lastWeekHonor = value; }
        }

        public string clan
        {
            get { return _clan; }
            set { _clan = value; }
        }

        [JsonIgnore]
        public string vkLink
        {
            get { return _vkLink; }
            set { _vkLink = value; }
        }

        [JsonIgnore]
        public string pointsHistory
        {
            get { return _pointsHistory; }
            set { _pointsHistory = value; }
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
                // Deserialize JSON into itself object class
                var jsonObject = JsonConvert.DeserializeObject<UserInfo>(json, jsonSerializerSettings);

                honor = jsonObject.honor;
                clan = jsonObject.clan;

                ConfigurePoints();
                
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

        public void ConfigurePoints()
        {
            var pointsArray = pointsHistory.Split(' ');
            lastWeekHonor = int.Parse(pointsArray.Last());
            thisWeekHonor = honor - lastWeekHonor;
        }
    }
}
