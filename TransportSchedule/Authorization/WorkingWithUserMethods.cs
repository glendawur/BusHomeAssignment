using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using TransportSchedule.Authorization;
using System.Security.Cryptography;
using TransportSchedule.Schedule.Modules;
using TransportSchedule.Repo;

namespace TransportSchedule
{
    public class WorkinhWithUserMethods
    {
        
        public static User Authorization(string _login, string _password)
        {
            WorkinhWithUserMethods m = new WorkinhWithUserMethods();
            DbRepository<User> Users = new DbUsers();
            List<User> users = Users.Items;
            User authorizeduser = new User();
            var currentUser = users.SingleOrDefault(x => x.Login == _login);
            if (currentUser == null)
                authorizeduser = null;
            else
            {
                if (currentUser.Password == GetHash(_password))
                {
                    var s = GetHash(_password);
                    authorizeduser = currentUser;
                }
                else authorizeduser = null;
            }
            return authorizeduser;
        }

        public static User SaveChanges(User _user, List<FavouriteStation> _stations)
        {
            var _users = GetUsers();
            _users.Remove(_users[_users.FindIndex(x => x.Login == _user.Login)]);
            _user = new User { Login = _user.Login, Email = _user.Email, Password = _user.Password, FavouriteStations = _stations };
            _users.Add(_user);
            var serializedItems = JsonConvert.SerializeObject(_users, Formatting.Indented);
            File.WriteAllText("WpfApp1/users.json", serializedItems);
            return _user;

           

        }
         
        public static string GetHash(string password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }


        public static bool RegistrationNewUser(string _login, string _password, string _email)
        {
            
            DbUsers Users = new DbUsers();
            if (!Users.Items.Exists(x => x.Login == _login))
            {
                WorkinhWithUserMethods m = new WorkinhWithUserMethods();
                List<User> users = Users.Items;
                Users.Add(new User { Login = _login, Password = GetHash(_password), Email = _email, FavouriteStations = new List<FavouriteStation>()});

                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            var text = File.ReadAllText("WpfApp1/users.json");
            users = JsonConvert.DeserializeObject<List<User>>(text);
            return users;
        }
    }
}
