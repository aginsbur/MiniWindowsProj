using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace DAL
{
    class XMLTools
    {
        static string dir = @"xml\";
        static XMLTools()
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }
        #region SaveLoadWithXElement
        public static void SaveListToXMLElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dir + filePath);
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

        public static XElement LoadListFromXMLElement(string filePath)
        {
           try
            {
                if (File.Exists(dir + filePath))
                {
                    return XElement.Load(dir+filePath);
                }
                else
                {
                    XElement rootElem = new XElement(filePath);
                    rootElem.Save(dir + filePath);
                    return rootElem;
                }
        }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
}
        #endregion

        #region SaveLoadWithXMLSerializer
        public static void SaveListToXMLSerializer<T>(List<T> list, string filePath)
        {
            try
            {
                FileStream file = new FileStream(dir + filePath, FileMode.Create);
                XmlSerializer x = new XmlSerializer(list.GetType());
                x.Serialize(file, list);
                file.Close();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }
        public static List<T> LoadListFromXMLSerializer<T>(string filePath)
        {
            try
            {
                if (File.Exists(dir + filePath))
                {
                    List<T> list;
                    XmlSerializer x = new XmlSerializer(typeof(List<T>));
                    FileStream file = new FileStream(dir + filePath, FileMode.Open);
                    list = (List<T>)x.Deserialize(file);
                    file.Close();
                    return list;
                }
                else
                    return new List<T>();
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion
        #region***running number***
        public static int getRuningNumber(string filePath)
        {
            List<int> ListRunningNumber = XMLTools.LoadListFromXMLSerializer<int>(filePath);
            try
            {
                int tempSave;
                int tempReturn;
                if (ListRunningNumber.Count() == 0)// if using the runing number for the first time
                {
                    tempReturn = 100;
                    tempSave = 101;
                }
                else
                {
                    tempReturn= ListRunningNumber.First();
                    tempSave = tempReturn+1;//increase the running number by 1
                    ListRunningNumber.Remove(tempReturn);//remove the previous running running number
                }
                ListRunningNumber.Add(tempSave);//add the new running number
                SaveListToXMLSerializer(ListRunningNumber, filePath);//save the new running number
                return tempReturn;
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion

    }
}
