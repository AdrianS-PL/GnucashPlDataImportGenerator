using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BossaWebsite.Model
{
    public abstract class BossaDataFile
    {
        protected abstract void Initialize(Stream stream);

        public static T CreateBossaFile<T>(Stream stream)  where T : BossaDataFile, new()
        {
            T file = new T();
            file.Initialize(stream);
            return file;
        }
    }
}
