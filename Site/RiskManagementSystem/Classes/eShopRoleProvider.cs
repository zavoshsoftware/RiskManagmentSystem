using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using RiskManagementSystem.Classes;
 

namespace eShop.Classes
{
    public class eShopRoleProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            int userID = Convert.ToInt32(username);
            var n = from us in DataContext.Context.Users
                    join rl in DataContext.Context.Roles
                       on us.RoleID equals rl.RoleID
                    where
                   us.UserID == userID
                    select rl;
            string rolename = n.First().RoleName;
            int i = 0;
            string[] a = new string[1];

            foreach (var roleName in n)
            {

                a[i] = rolename.ToString();
                i++;
            }

              return a;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}