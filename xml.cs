﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
namespace SoftGenConverter
{
    class Xml
    {
        public static void loadXml(DataGridView dataGridView1, string path)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }

            {
                if (File.Exists(path)) //
                {
                    DataSet ds = new DataSet();
                    ds.ReadXml(path);
                    try
                    {
                        foreach (DataRow item in ds.Tables["Employee"].Rows)
                        {
                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = item["NAME"];
                            dataGridView1.Rows[n].Cells[1].Value = item["ERDPO"];
                            dataGridView1.Rows[n].Cells[2].Value = item["Comment"];

                        }
                    }
                    catch (Exception e) { }

                }
                else
                {
                    MessageBox.Show("XML файл не найден.", "Ошибка.");
                }
            }
        }
        public static DataGridView loadXml(string path)
        {
            DataGridView dataGridView = new DataGridView();
           DataSet ds = new DataSet(); 
            if (File.Exists(path)) //
                {
                    ds.ReadXml(path);try
                    {
                     foreach (DataRow item in ds.Tables["Employee"].Rows)
                            {
                                int n = dataGridView.Rows.Add(); 
                                dataGridView.Rows[n].Cells[0].Value = item["NAME"]; 
                                dataGridView.Rows[n].Cells[1].Value = item["ERDPO"]; 
                                dataGridView.Rows[n].Cells[2].Value = item["Comment"]; 

                            }
                    }catch(Exception e) { }
                 }   
                    
            return dataGridView;
        }
        #region MyRegion
        //public static void createConfig(string Path)
        //{

        //    XmlTextWriter textWritter = new XmlTextWriter(Path, Encoding.UTF8);

        //    textWritter.WriteStartDocument();
        //    textWritter.WriteStartElement("head");
        //    textWritter.WriteEndElement();
        //    textWritter.Close();
        //}

        //public static void editXml(string Path, Bank bank)
        //{
        //    XmlDocument xDoc = new XmlDocument();
        //    xDoc.Load(Path);
        //    XmlElement xRoot = xDoc.DocumentElement;
        //    XmlNodeList childnodes = xRoot.SelectNodes("bank");
        //    foreach (XmlNode n in childnodes)
        //    {
        //        if (!n.SelectSingleNode("@name").Value.Equals(bank.name))
        //        {
        //            CreteConfig(Path, bank);
        //        }
        //        else
        //        {

        //        }
        //    }
        //}
        //public static  void CreteConfig(string Path, Bank bank)
        //{
        //    if (!System.IO.File.Exists(Path))
        //    {
        //        createConfig(Path);
        //    }

        //    XmlDocument xDoc = new XmlDocument();
        //    xDoc.Load(Path);
        //    XmlElement xRoot = xDoc.DocumentElement;
        //    // создаем новый элемент bank
        //    XmlElement userElem = xDoc.CreateElement("bank");
        //    // создаем атрибут name
        //    XmlAttribute nameAttr = xDoc.CreateAttribute("name");
        //    // создаем элементы 
        //    XmlElement platNumber = xDoc.CreateElement("platNumber");
        //    XmlElement datePayment = xDoc.CreateElement("datePayment");
        //    XmlElement mfo = xDoc.CreateElement("mfo");
        //    XmlElement rahunok = xDoc.CreateElement("rahunok");
        //    XmlElement cliBankCode = xDoc.CreateElement("cliBankCode");
        //    XmlElement recivPayNumt = xDoc.CreateElement("recivPayNum");
        //    XmlElement edrpou = xDoc.CreateElement("edrpou");
        //    XmlElement state = xDoc.CreateElement("state");

        //    // создаем текстовые значения для элементов и атрибута

        //    XmlText nameText = xDoc.CreateTextNode(bank.name);
        //    XmlText platNumberText = xDoc.CreateTextNode(bank.platNumber.ToString());
        //    XmlText datePaymentText = xDoc.CreateTextNode(bank.datePayment.ToString());
        //    XmlText mfoText = xDoc.CreateTextNode(bank.mfo);
        //    XmlText rahunokText = xDoc.CreateTextNode(bank.rahunok);
        //    XmlText cliBankCodeText = xDoc.CreateTextNode(bank.cliBankCode);
        //    XmlText recivPayNumtText = xDoc.CreateTextNode(bank.recivPayNum);
        //    XmlText edrpouText = xDoc.CreateTextNode(bank.edrpou);
        //    XmlText stateText = xDoc.CreateTextNode(bank.state.ToString());

        //    //добавляем узлы
        //    nameAttr.AppendChild(nameText);
        //    platNumber.AppendChild(platNumberText);
        //    datePayment.AppendChild(datePaymentText);
        //    mfo.AppendChild(mfoText);
        //    rahunok.AppendChild(rahunokText);
        //    cliBankCode.AppendChild(cliBankCodeText);
        //    recivPayNumt.AppendChild(recivPayNumtText);
        //    edrpou.AppendChild(edrpouText);
        //    state.AppendChild(stateText);

        //    userElem.Attributes.Append(nameAttr);
        //    userElem.AppendChild(platNumber);
        //    userElem.AppendChild(datePayment);
        //    userElem.AppendChild(mfo);
        //    userElem.AppendChild(rahunok);
        //    userElem.AppendChild(cliBankCode);
        //    userElem.AppendChild(recivPayNumt);
        //    userElem.AppendChild(edrpou);
        //    userElem.AppendChild(state);

        //    xRoot.AppendChild(userElem);
        //    xDoc.Save(Path);

        //}

        //public static List<Bank> ReadXml(string Path)
        //{
        //    List<Bank> banks = new List<Bank>();

        //    XmlDocument document = new XmlDocument();
        //    document.Load(Path);
        //    XmlElement xRoot = document.DocumentElement;
        //    foreach (XmlElement xnode in document)
        //    {
        //        Bank bank = new Bank();
        //        XmlNode attr = xnode.Attributes.GetNamedItem("name");
        //        if (attr != null)
        //            bank.name = attr.Value;
        //        foreach (XmlNode childnode in xnode.ChildNodes)
        //        {
        //            if (childnode.Name == "platNumber")
        //                bank.platNumber = Int64.Parse(childnode.InnerText);

        //            if (childnode.Name == "datePayment")
        //                bank.datePayment = Int32.Parse(childnode.InnerText);
        //            if (childnode.Name == "mfo")
        //                bank.mfo = childnode.InnerText;
        //            if (childnode.Name == "rahunok")
        //                bank.rahunok = childnode.InnerText;
        //            if (childnode.Name == "cliBankCode")
        //                bank.cliBankCode = childnode.InnerText;
        //            if (childnode.Name == "recivPayNum")
        //                bank.recivPayNum = childnode.InnerText;
        //            if (childnode.Name == "edrpou")
        //                bank.edrpou = childnode.InnerText;
        //            if (childnode.Name == "state")
        //                bank.state = Int32.Parse(childnode.InnerText);
        //        }
        //            banks.Add(bank);
        //    }

        //    return banks;

        //}

        //public static void deleteElementXml(string Path)
        //{
        //    XmlDocument xDoc = new XmlDocument();
        //    xDoc.Load(Path);
        //    XmlElement xRoot = xDoc.DocumentElement;

        //    XmlNode firstNode = xRoot.FirstChild;
        //    xRoot.RemoveChild(firstNode);
        //    xDoc.Save(Path);
        //}
        #endregion
    }
}