using System;
using System.Collections.Generic;
using System.Linq;
using DataAcessClassLibrary;

namespace BusinessLogicClassLibrary
{
   public enum Roles
    {
        guest,
        administrator,
        librarian,
        patron
    }
    public class BLLUser
    {
        int ID;
        string UserName { get; set; }
        string password { get; set; }
        int RoleId { get; set; }
       public Roles Role { get; set; }
      //  BLLUser User = new BLLUser();
        List<BLLUser> bLLUsers = new List<BLLUser>();
        List<DAUser> daUsers = new List<DAUser>();
        DAUser user = new DAUser();
    public BLLUser()
        {
            ID = 0;
            UserName = string.Empty;
            password = string.Empty;
            Role =  Roles.guest;
        }
    public BLLUser(int iD, string userName, Roles role,string password)
        {
           this.ID = iD;
            this.UserName = userName;
            this.password = password;
           this.Role = role;
        }

        public BLLUser(int role, string userName, string password)
        {
            RoleId = role;
            UserName = userName;
            this.password = password;
        }

        public void addUser(BLLUser users)
        {
            int roleID = (int)user.Role;
           // user.AddUser(users.RoleId, users.UserName, users.password);

            Console.WriteLine("New user added");
        }
        public void addUser(string userName,int role,string password)
        {
            bLLUsers.Add(new BLLUser(role,userName,password));
           user.AddUser(role, userName, password);


        }
        public bool userExist(int id)
        {
            List<BLLUser> userList = Map(user.GetAllUser());

            return userList.Any(p=> p.ID == id);
        }
        public void removeUser(int id)
        {
            user.RemoveUser(id);

        }
        public bool Login(string userName,string password)
        {
            List<BLLUser> userList = Map(user.GetAllUser());


            bool sucess = userList.Any(p => p.UserName == userName && p.password == password);

            return sucess;
        }
        public int getUserInfoForLogin(string userName, string password)
        {
            List<BLLUser> userList = Map(user.GetAllUser());

            BLLUser loginUser = userList.FirstOrDefault(p => p.UserName == userName && p.password == password);

            return loginUser.RoleId;

        }
        public Roles getUserRole(string userName, string password)
        {
            
            foreach(BLLUser user in bLLUsers)
                if(user.UserName == userName && user.password == password)
                {
                    return user.Role;
                }
            return Roles.guest;
        }
        public void updateUserName (int id,string newUserName)
        {
            user.UpdateUsername(id,newUserName);    

           
        }

        public void updatePassword(int id, string password)
        {
            user.UpadatePassword(id,password);

        }
        public void printSingleUser(int id)
        {
            BLLRole role = new BLLRole();
            user = user.GetUser(id);
            if (user != null)
            {
                BLLUser bLLUser = Map(user);
                Console.WriteLine("User ID: " + bLLUser.ID);
                Console.WriteLine("Username: " + bLLUser.UserName);
                Console.WriteLine("User Role: " + role.getRole(bLLUser.RoleId));
            }
            else
            {
                Console.WriteLine("User does not exist ");
            }

        }
        public void printAllUsers()
        {
            BLLRole role = new BLLRole();
            List<BLLUser> userList = Map(user.GetAllUser());
            foreach (BLLUser user in userList)
            {
                Console.WriteLine("User ID: " + user.ID);
                Console.WriteLine("Username: " + user.UserName);
                Console.WriteLine("User Role: " + role.getRole(user.RoleId));
            }
        }
        public List<BLLUser> Map(List<DAUser> dAUsers)
        {
            
            foreach(DAUser dAUser in dAUsers)
            {
                BLLUser user = new BLLUser();
                user.ID = dAUser.ID;
                user.UserName = dAUser.UserName;
                user.password = dAUser.password;
                user.RoleId = dAUser.Role;
                bLLUsers.Add(user);
            }
            return bLLUsers;    
        }
        public BLLUser Map(DAUser dAUser)
        {
            BLLUser user = new BLLUser();
            user.ID = dAUser.ID;
            user.UserName = dAUser.UserName;
            user.password = dAUser.password;
            user.RoleId = dAUser.Role;
            return user;
        }
    }
}
