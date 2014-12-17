using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public static class ExtensionMethod
    {
        public static byte[] GetFileData(this string fileName, string filePath)
        {
            var fullFilePath = string.Format("{0}/{1}", filePath, fileName);
            if (!System.IO.File.Exists(fullFilePath))
            {
                throw new FileNotFoundException("The file does not exist.",
                fullFilePath);
            }
            return System.IO.File.ReadAllBytes(fullFilePath);
        }

        public static string GetMimeType(this string fileName)
        {
            string mimeType = "application/unknown";
            string ext = System.IO.Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}