using System.IO;
using Ionic.Zip;

namespace Mega.Synchronizer.Common.Helpers
{
    public static class ZipHelper
    {
        public static bool UnZip(string zipFileName, string entryName)
        {
            using (var zip = ZipFile.Read(zipFileName))
            {
                var e = zip[entryName];

                e.Extract(Path.GetDirectoryName(zipFileName), ExtractExistingFileAction.OverwriteSilently);
                return true;
            }
        }

        public static void Zip(string zipFileName, string entryName)
        {
            using (var zip = new ZipFile())
            {
                zip.AddItem(entryName, "");
                zip.Save(zipFileName);
            }
        }
    }
}
