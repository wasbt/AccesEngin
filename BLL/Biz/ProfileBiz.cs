using BLL.Common;
using DAL;
using log4net;
using Shared.Models;
using Shared.API.IN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace BLL.Biz
{
    public class ProfileBiz : CommonBiz
    {
        public ProfileBiz(OcpPerformanceDataContext  context, ILog log) : base(context, log)
        {
        }

        public bool AddUserProfile(Profile profile)
        {
            context.Profile.Add(profile);
            context.SaveChanges();
            return true;
        }

        public List<UsersAndRolesElement> GetUsersAndRolesViewModel(long companyId, long? entittyId)
        {
            var query = context.Profile.AsQueryable();
            //if (companyId != 0)
            //    query = query.Where(x => x.CompanyId == companyId);
            //if (entittyId != 0)
            //    query = query.Where(x => x.EntityId == entittyId);
            return query.Select(p => new UsersAndRolesElement()
            {
                ProfileId = p.Id,
                FullName = p.FullName,
                Email = p.Email,
                RolesNames = p.AspNetUsers.AspNetRoles.Select(r => r.Name)
            }).ToList();
        }

        public bool SaveUserRoles(SaveUserRolesModel model)
        {
            var user = context.AspNetUsers.Find(model.userId);

            if (user != null)
            {
                var query = $"delete from AspNetUserRoles where UserId = '{user.Id}'";
                context.Database.ExecuteSqlCommand(query);

                if (model.rolesList?.Count > 0)
                {
                    foreach (var role in model.rolesList)
                    {
                        string q = $"insert into AspNetUserRoles (UserId,RoleId) values ('{user.Id}','{role}')";
                        context.Database.ExecuteSqlCommand(q);
                    }
                }

                return true;
            }

            return false;
        }

        public List<RoleElement> GetUserRoles(GetUserRolesModel model, List<string> role = null)
        {
            #region All Roles
            var AllRoles = new List<RoleElement>();
            if (role == null)
            {
                AllRoles = context.AspNetRoles.Select(r => new RoleElement()
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = false
                })
                .ToList();
            }
            else
            {
                AllRoles = context.AspNetRoles.Select(r => new RoleElement()
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = false
                }).Where(q => !role.Contains(q.Name))
                .ToList();
            }

            #endregion

            #region Selected Roles Ids
            var UserRolesIds = context.Profile.Where(u => u.Id == model.UserId)
                    .FirstOrDefault()
                    .AspNetUsers
                    .AspNetRoles
                    .Select(r => r.Id)
                    .ToList();
            #endregion

            #region Flag selected roles
            foreach (var item in AllRoles)
            {
                if (UserRolesIds.Contains(item.Id))
                    item.IsSelected = true;
            }
            #endregion

            return AllRoles;
        }

        public List<RoleElement> GetAllRoles()
        {
            return context.AspNetRoles
                .Select(r => new RoleElement()
                {
                    Id = r.Id,
                    Name = r.Name,
                    IsSelected = false
                }).ToList();
        }

        public List<UsersAndRolesElement> GetUsersInRole(string roleName)
        {
            var query = from user in context.Profile
                        where user.AspNetUsers.AspNetRoles.Any(r => r.Name == roleName)
                        select user;


            return query.Select(p => new UsersAndRolesElement()
            {
                ProfileId = p.Id,
                FullName = p.FullName,
                Email = p.Email,
            }).ToList();
        }
    }
}
