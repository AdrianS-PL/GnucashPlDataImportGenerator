using GnucashPlDataImportGeneratorApp.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace BossaTestDataImporter.Imports.ViewModels
{
    public class FileImportResultGridRowViewModel : IFileImportResultGridRowViewModel
    {
        public string Message { get; set; }
        public string Filename { get; set; }
        public Bitmap Image
        {
            get
            {
                if (IsError)
                {
                    return new Bitmap(GnucashPlDataImportGeneratorAppResources.error_icon_24.ToBitmap());
                }
                else
                {
                    return new Bitmap(GnucashPlDataImportGeneratorAppResources.ok_icon_24.ToBitmap());
                }
            }
        }
        public bool IsError { get; set; }
    }
}
