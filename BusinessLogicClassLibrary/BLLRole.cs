using System;
using System.Collections.Generic;
using System.Text;
using DataAccessClassLibrary;

namespace BusinessLogicClassLibrary
{
    public class BLLRole
    {
        int ID;
        string RoleName;
        BLLRole bLLRole;

        public BLLRole()
        {
            this.ID = 1;
            this.RoleName = "Guest";
        }
        public BLLRole(int iD, string roleName)
        {
            ID = iD;
            RoleName = roleName;
        }
        public string getRole(int iD)
        {
            DARole dARole = new DARole();   
           bLLRole= Map(dARole.GetRole(iD));
            return bLLRole.RoleName;
        }
        public BLLRole Map(DARole dARole)
        {
            bLLRole = new BLLRole();
            bLLRole.ID = dARole.ID;
            bLLRole.RoleName = dARole.RoleName;
            return bLLRole;
        }
    }
}
