
namespace KidsTodo.User
{
    using System.Collections;
    using System.Collections.Generic;

    using SimpleJSON;

    using KidsTodo.Network;

    public class UserManager
    {
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

        public void UpdateUserData(string userJson)
        {
            var userData = SimpleJSON.JSON.Parse(userJson);
            name = userData["name"];
            gender = userData["gender"];
            age = userData["age"];
        }
    }
}
