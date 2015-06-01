// Type: log4net.Config.XmlConfigurator
// Assembly: log4net, Version=1.2.10.0, Culture=neutral, PublicKeyToken=1b44e1d426115821
// Assembly location: D:\Users\Deisbel\Projects\EndToEnd\Mega\source\Resources\log4net.dll

using log4net.Repository;
using System;
using System.IO;
using System.Xml;

namespace log4net.Config
{
    public sealed class XmlConfigurator
    {
        public static void Configure();
        public static void Configure(ILoggerRepository repository);
        public static void Configure(XmlElement element);
        public static void Configure(ILoggerRepository repository, XmlElement element);
        public static void Configure(FileInfo configFile);
        public static void Configure(Uri configUri);
        public static void Configure(Stream configStream);
        public static void Configure(ILoggerRepository repository, FileInfo configFile);
        public static void Configure(ILoggerRepository repository, Uri configUri);
        public static void Configure(ILoggerRepository repository, Stream configStream);
        public static void ConfigureAndWatch(FileInfo configFile);
        public static void ConfigureAndWatch(ILoggerRepository repository, FileInfo configFile);
    }
}
