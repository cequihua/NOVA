using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Mega.Configurator.Helpers
{
    public class ConfigSettingsHelper
    {
        private readonly string ConfigFilePath;
        private XmlDocument Document;
        private List<string> Exceptions = new List<string> { "SERVICE_NAME" };

        public ConfigSettingsHelper(string configFilePath)
        {
            ConfigFilePath = configFilePath;
            LoadConfigDocument();
        }

        public bool ExistsConnectionString()
        {
            var node = Document.SelectSingleNode(string.Format("//connectionStrings"));
            if (node == null)
            {
                return false;
            }
            node.InnerXml = node.InnerXml.Replace("\r\n", string.Empty);
            return node != null && (node.ChildNodes.Cast<XmlNode>().Where(setting => setting is XmlElement)).FirstOrDefault() != null ? true : false;
        }

        public string ReadConnectionString()
        {
            if (ExistsConnectionString())
            {
                var node = Document.SelectSingleNode(string.Format("//connectionStrings"));
                node.InnerXml = node.InnerXml.Replace("\r\n", string.Empty);
                return node.ChildNodes.Cast<XmlNode>().Where(setting => setting is XmlElement).FirstOrDefault().Attributes["connectionString"].InnerText;
            }

            return string.Empty;
        }

        public void WriteConnectionString(string value)
        {
            if (!ExistsConnectionString())
                throw new InvalidOperationException("No existe sección de ConnectionStrings en este fichero de configuración.");

            var node = Document.SelectSingleNode(string.Format("//connectionStrings"));

            node.ChildNodes.Cast<XmlNode>().Where(setting => setting is XmlElement).FirstOrDefault().Attributes["connectionString"].InnerText = value.Trim();
        }

        public bool ExistsApplicationSettings()
        {
            var node = Document.SelectSingleNode(string.Format("//applicationSettings"));
            return node != null ? true : false;
        }

        public bool ExistsAppSettings()
        {
            var node = Document.SelectSingleNode(string.Format("//appSettings"));
            return node != null ? true : false;
        }

        public string ReadApplicationSetting(string key)
        {
            LoadConfigDocument();
            var node = Document.SelectSingleNode(string.Format("//applicationSettings"));

            if (node == null)
                throw new InvalidOperationException("No existe sección de applicationSettings en este fichero de configuración.");

            if (!string.IsNullOrEmpty(key))
            {
                var elem = ((XmlElement)node.ChildNodes.Cast<XmlNode>().Where(setting => setting is XmlElement)).SelectSingleNode(string.Format("//setting[@name='{0}']", key));

                if (elem != null)
                {
                    return elem.InnerText;
                }
            }

            return string.Empty;
        }

        public string ReadAppSetting(string key)
        {
            LoadConfigDocument();
            var node = Document.SelectSingleNode(string.Format("//appSettings"));

            if (node == null)
                throw new InvalidOperationException("No existe sección de appSettings en este fichero de configuración.");

            if (!string.IsNullOrEmpty(key))
            {
                var elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

                if (elem != null)
                {
                    return elem.Attributes["value"].InnerText;
                }
            }

            return string.Empty;
        }

        public DataTable ReadApplicationSettings()
        {
            var node = Document.SelectSingleNode(string.Format("//applicationSettings"));

            if (node == null)
                return null;

            var dt = new DataTable("data");
            dt.Columns.Add("Key");
            dt.Columns.Add("Value");

            var xmlNodes = node.ChildNodes[0].SelectNodes("//setting").Cast<XmlNode>().Where(setting => setting is XmlElement).ToList();

            foreach (XmlNode setting in
                xmlNodes.Where(setting => !Exceptions.Contains(setting.Attributes["name"].InnerText)))
            {
                dt.Rows.Add(setting.Attributes["name"].InnerText, setting.ChildNodes[0].InnerXml.Trim());
            }

            return dt;
        }

        public DataTable ReadAppSettings()
        {
            var node = Document.SelectSingleNode(string.Format("//appSettings"));

            if (node == null)
                return null;

            var dt = new DataTable("data");
            dt.Columns.Add("Key");
            dt.Columns.Add("Value");

            var xmlNodes = node.ChildNodes.Cast<XmlNode>().Where(setting => setting is XmlElement).ToList();

            foreach (XmlElement setting in xmlNodes)
            {
                if (!Exceptions.Contains(setting.Attributes["key"].InnerText) && !string.IsNullOrEmpty(setting.Attributes["key"].InnerText))
                {
                    dt.Rows.Add(setting.Attributes["key"].InnerText, setting.Attributes["value"].InnerText.Trim());
                }
            }

            return dt;
        }

        public void WriteApplicationSetting(string key, string value)
        {
            var node = Document.SelectSingleNode("//applicationSettings");

            if (node == null)
                throw new InvalidOperationException("No existe sección de applicationSettings en este fichero de configuración.");

            var setting = (XmlElement)node.SelectSingleNode(string.Format("//setting[@name='{0}']", key));

            setting.ChildNodes[0].InnerXml = value.Trim();
        }

        public void WriteAppSetting(string key, string value)
        {
            var node = Document.SelectSingleNode("//appSettings");

            if (node == null)
                throw new InvalidOperationException("No existe sección de appSettings en este fichero de configuración.");

            var setting = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

            setting.Attributes["value"].InnerText = value.Trim();
        }

        private void LoadConfigDocument()
        {
            try
            {
                Document = new XmlDocument();
                Document.Load(ConfigFilePath);
            }
            catch (System.IO.FileNotFoundException e)
            {
                throw new Exception("Imposible Leer el fichero de Configuración", e);
            }
        }

        public void Save()
        {
            Document.InnerXml = Document.InnerXml.Replace("<value></value>", "<value />");

            Document.Save(ConfigFilePath);
        }
    }
}
