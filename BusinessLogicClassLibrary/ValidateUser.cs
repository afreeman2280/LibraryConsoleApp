using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicClassLibrary
{
    public class ValidateUser
    {
        Roles _roles;
        public void ValidateEnumValue(string OsValue)
        {
            bool success = Enum.IsDefined(typeof(Roles), OsValue);
            if (success)
            {
                Console.WriteLine("Valid Enum Value");
            }
            else
            {
                Console.WriteLine("InValid Enum Value");
            }
        }
    }
}
