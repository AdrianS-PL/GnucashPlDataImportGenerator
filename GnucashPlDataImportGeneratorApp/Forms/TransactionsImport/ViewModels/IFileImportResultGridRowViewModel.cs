using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BossaTestDataImporter.Imports.ViewModels
{
    public interface IFileImportResultGridRowViewModel
    {
        Bitmap Image { get; }
        string Filename { get; }
        string Message { get; }
    }
}
