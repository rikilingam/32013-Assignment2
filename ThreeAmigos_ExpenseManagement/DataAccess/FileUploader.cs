using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ThreeAmigos_ExpenseManagement.DataAccess
{
    public class FileUploader
    {
        IConfigurationDAL config;        
        readonly string ReceiptKeyName = "ReceiptItemFilePath"; // Key name in web.config for the receipt upload location

        public FileUploader()
        {
            config = new ConfigurationDAL();
            
        }

        public FileUploader(IConfigurationDAL config)
        {
            this.config = config;
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

                    fileUpload.SaveAs(System.Web.HttpContext.Current.Server.MapPath(config.GetAppSetting(ReceiptKeyName) as string) + newFileName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem uploading the file " + fileUpload.FileName + ": " + ex.Message);
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