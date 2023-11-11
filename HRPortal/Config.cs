using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace HRPortal
{
    public class Config
    {
        //public Nav.NAV ReturnNav()
        //{

        //    Nav.NAV nav = new Nav.NAV(new Uri(ConfigurationManager.AppSettings["ODATA_URI"]))
        //    {
        //        Credentials =
        //            new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
        //                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"])
        //    };
        //    return nav;
        //}
        public static string FilesLocation()
        {
            return ConfigurationManager.AppSettings["FilesLocation"];
        }
        public bool IsAllowedExtension(string extension)
        {
            bool check = Convert.ToBoolean(ConfigurationManager.AppSettings["CheckFileExtensions"]);
            if (check)
            {
                string allowedFileTypes = ConfigurationManager.AppSettings["AllowedFileExtensions"];
                string[] info = allowedFileTypes.Split(',');
                extension = extension.Replace('.', ' ');
                extension = extension.Trim();
                extension = extension.ToLower();
                foreach (string fileExtension in info)
                {
                    string myExtension = fileExtension;
                    myExtension = myExtension.Replace('.', ' ');
                    myExtension = myExtension.Trim();
                    myExtension = myExtension.ToLower();
                    if (myExtension == extension)
                    {
                        return true;
                    }
                }

            }
            else
            {
                return true;
            }
            return false;
        }
        public HRPortal.HRPortal ObjNav()
        {
            var ws = new HRPortal.HRPortal();
            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                ws.Credentials = credentials;
                ws.PreAuthenticate = true;

            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return ws;
        }
        public QueriesESS.QueriesESS ObjNav1()
        {
            var ws = new QueriesESS.QueriesESS();
            try
            {
                var credentials = new NetworkCredential(ConfigurationManager.AppSettings["W_USER"],
                    ConfigurationManager.AppSettings["W_PWD"], ConfigurationManager.AppSettings["DOMAIN"]);
                ws.Credentials = credentials;
                ws.PreAuthenticate = true;


            }
            catch (Exception ex)
            {
                ex.Data.Clear();
            }
            return ws;
        }
    }
}