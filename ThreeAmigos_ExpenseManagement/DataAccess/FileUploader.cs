using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ThreeAmigos_ExpenseManagement.DataAccess
{
    public class FileUploader
    {
        string destinationPath;

        public FileUploader()
        {
            destinationPath = ConfigurationManager.AppSettings["ReceiptItemFilePath"];
        }

        //Upload file to file path defined in web.config
        public string Upload(HttpPostedFileBase fileUpload)
        {
            string newFileName = "";

            try
            {
                if (fileUpload!=null && fileUpload.ContentLength>0)
                {
                    newFileName = GenerateNewFileName() + ".pdf";

                    fileUpload.SaveAs(System.Web.HttpContext.Current.Server.MapPath(destinationPath) + newFileName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem uploading file " + fileUpload.FileName + ": " + ex.Message);
            }

            return newFileName;
        }

        //Generate new file name to ensure there are no duplicates
        private string GenerateNewFileName()
        {
            Guid guid;
            guid = Guid.NewGuid();

            return guid.ToString();
        }
    }
}