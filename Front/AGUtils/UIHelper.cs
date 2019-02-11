using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using Shared;

namespace Front.AGUtils
{
    public static class UIHelper
    {
        public const string CrudTableCss = "table table-hover table-stripped m-t-10";

        public static SelectList GetSelectList(string tableName)
        {
            tableName = tableName.Replace("GlobalHSESuite.Web.DAL.", "");

            List<string> colsList = new List<string>();

            using (var context = new EnginDbContext())
            {
                // Sql Server
                //#if DEBUG

                var reqColumnsList = $@"select COLUMN_NAME 
                                                        from LocalDbAuditEngin.INFORMATION_SCHEMA.COLUMNS IC
                                                        where TABLE_NAME = '{tableName}'";
                //#else

                //var reqColumnsList = $@"select COLUMN_NAME 
                //                        from ghse_suite_db_azure.INFORMATION_SCHEMA.COLUMNS IC
                //                        where TABLE_NAME = '{tableName}'";
                //#endif

                colsList = context.Database.SqlQuery<string>(reqColumnsList).ToList();
            }

            return new SelectList(colsList);
        }

        public static object ProcessWhere(this IQueryable query, string columnName, string content)
        {
            try
            {
                int intContent = -1;

                if (int.TryParse(content, out intContent))
                {
                    query = query.Where(string.Format("{0}=={1}", columnName, content));
                }
                else
                {
                    query = query.Where(string.Format("{0}.ToLower().Contains(\"{1}\".ToLower())", columnName, content));
                }

            }
            catch (Exception)
            {
                try
                {
                    query = query.Where(string.Format("{0}.Contains(\"{1}\")", columnName, content));
                }
                catch (Exception)
                {

                }
            }


            return query;
        }

        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static bool IsInRole(this HttpSessionStateBase Session, string roleName)
        {
            return ((List<string>)Session[ConstsAccesEngin.SESSION_UserRoleNamesList]).Any(s => s.Equals(roleName));
        }

        public static bool HasRoles(this HttpSessionStateBase Session)
        {
            return ((List<string>)Session[ConstsAccesEngin.SESSION_UserRoleNamesList])?.Count > 0;
        }

        public static string SafeQueryString(this HttpRequestBase Request, string Key)
        {
            return Request?.QueryString?[Key];
        }



    }
}