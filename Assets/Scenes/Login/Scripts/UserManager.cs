
namespace KidsTodo.User
{
    using System.Collections;
    using System.Collections.Generic;

    using SimpleJSON;

    using KidsTodo.Common;

    public class UserManager
    {
        private static UserManager _instance;
        public static UserManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserManager();
                }
                return _instance;
            }
        }
        private string name = "Null";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string gender = "Null";
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        private int age = 0;
        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        private string key = "";
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        private string jwtTokenAccess = "";
        public string JwtTokenAccess
        {
            get { return jwtTokenAccess; }
            set { jwtTokenAccess = value; }
        }

        private string jwtTokenRefresh = "";
        public string JwtTokenRefresh
        {
            get { return jwtTokenRefresh; }
            set { jwtTokenRefresh = value; }
        }

        public void UpdateUserData(string userJson)
        {
            var userData = SimpleJSON.JSON.Parse(userJson);
            name = userData["name"];
            gender = userData["gender"];
            age = userData["age"];
        }
    }
}
