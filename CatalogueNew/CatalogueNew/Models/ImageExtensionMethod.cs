using CatalogueNew.Models.Entities;
using Omu.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace CatalogueNew.Web.Models
{
    public static class ImageExtensionMethod
    {
        private const int ImgMaxWidth = 400;
        private const int ImgMaxHeight = 250;

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

        public static byte[] ResizeImage(this Image image)
        {
            var ms = new MemoryStream(image.Value);
            var source = Imager.Resize(System.Drawing.Image.FromStream(ms), ImgMaxWidth, ImgMaxHeight, false);
            var fileResize = new MemoryStream();

            source.Save(fileResize, ImageFormat.Png);

            return fileResize.ToArray();
        }
    }
}