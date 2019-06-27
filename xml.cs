﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace SoftGenConverter
{
    internal class Xml
    {
        public static void loadXml(DataGridView dataGridView1, string path)
        {
            
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }

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

                        dataGridView1.Rows[n].Cells[2].Value = item["RRahunok"];
                        dataGridView1.Rows[n].Cells[3].Value = Bank.shortText(item["Comment"].ToString());
                        if (dataGridView1.Rows[n].Cells[3].Value.Equals("null"))
                        {
                            dataGridView1.Rows[n].DefaultCellStyle.BackColor = Color.BurlyWood;
                        }
                    }
                }
                catch (Exception) { }

            }
            //else
            //{
            //    MessageBox.Show("PayConverterData.xml файл не знайдений. Файл створено з конфігурації програми.", "Помилка.", MessageBoxButtons.OK,
            //        MessageBoxIcon.Warning);

            //    XmlDocument doc = new XmlDocument();
            //    doc.LoadXml(path2);
            //    doc.Save("PayConverterData.xml");
            //}

        }

        public static void isExistsFile(string path, string text)
        {
            string file = path = path.Remove(0, path.LastIndexOf("\\") + 1);

            if (!File.Exists(path)) //
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(text);
                // MessageBox.Show(file);
                StreamWriter outStream = System.IO.File.CreateText(file);
                doc.Save(outStream);
                outStream.Close();
                Thread.Sleep(300);
                MessageBox.Show(file + " файл не знайдений!" + Environment.NewLine + " Файл створено з конфігурації програми.", "Помилка.", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
           

        }
        public static List<Bank> loadXml(string path)
        {

            DataSet ds = new DataSet();
            List<Bank> config = new List<Bank>();
            
            if (File.Exists(path)) //
            {
                ds.ReadXml(path); try
                {
                    foreach (DataRow item in ds.Tables["Bank"].Rows)
                    {
                        Bank bank = new Bank();
                        bank.name =  item["NAME"].ToString();
                        bank.mfo = item["MFO"].ToString();
                        bank.edrpou = item["edrpou"].ToString();
                        bank.rahunok = item["RRAHUNOK"].ToString();
                        bank.clientBankCode = item["clientBankCode"].ToString();
                        try
                        {
                            bank.isAval = Convert.ToInt32(item["STATE"]);
                        }
                        catch
                        {
                        }
                        finally
                        {
                            config.Add(bank);
                        }

                    }
                }
                catch (Exception) { }
            }


            return config;
        }

        public static void saveXml(DataGridView dataGridView, string path)
        {
            try
            {
                DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
                DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
                dt.TableName = "Employee"; // название таблицы
                dt.Columns.Add("NAME"); // название колонок
                dt.Columns.Add("ERDPO"); // название колонок
                dt.Columns.Add("RRahunok");
                dt.Columns.Add("Comment");

                ds.Tables.Add(dt); //в ds создается таблица, с названием и колонками, созданными выше

                foreach (DataGridViewRow r in dataGridView.Rows) // пока в dataGridView1 есть строки
                {
                    if (r.Cells != null)
                    {
                        DataRow row = ds.Tables["Employee"].NewRow(); // создаем новую строку в таблице, занесенной в ds
                        row["Name"] = r.Cells[0].Value;
                        row["ERDPO"] = r.Cells[1].Value;  //в столбец этой строки заносим данные из первого столбца dataGridView1
                        row["Comment"] = r.Cells[3].Value; // то же самое со вторыми столбцами
                        row["RRahunok"] = r.Cells[2].Value;

                        ds.Tables["Employee"].Rows.Add(row); //добавление всей этой строки в таблицу ds.
                    }
                }

                ds.WriteXml(path);
                // MessageBox.Show("XML файл успішно збережений.", "Виконано.");
            }
            catch //(System.Exception ex)
            {
               // MessageBox.Show("Неможливо зберегти дані в XML файл.", "Помилка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
               // MessageBox.Show(ex.Message);
            }



        }
        public static void saveXml(string path)
        {
            XElement contacts =
                new XElement("Contacts",
                    new XElement("Contact",
                        new XElement("Name", "Patrick Hines"),
                        new XElement("Phone", "206-555-0144",
                            new XAttribute("Type", "Home")),
                        new XElement("phone", "425-555-0145",
                            new XAttribute("Type", "Work")),
                        new XElement("Address",
                            new XElement("Street1", "123 Main St"),
                            new XElement("City", "Mercer Island"),
                            new XElement("State", "WA"),
                            new XElement("Postal", "68042")
                        )
                    )
                );
            contacts.Save(path);
            //var doc2 = new XDocument();
            //doc2.Element("Bank").Add(new XAttribute("id", 0),
            //    new XElement("NAME", "Aval"),
            //    new XElement("MFO", "302021"),
            //    new XElement("ERDPOU", "12456"),
            //    new XElement("RRAHUNOK", "454545"),
            //    new XElement("clientBankCode", "1111"),
            //    new XElement("STATE", "0")
            //    );
            //doc2.Save(path);


            //try
            //{
            //    DataSet ds = new DataSet(); 
            //    DataTable dt = new DataTable(); 
            //    dt.TableName = "BANK"; 
            //    dt.Columns.Add("NAME"); 
            //    dt.Columns.Add("MFO"); 
            //    dt.Columns.Add("ERDPOU"); 
            //    dt.Columns.Add("RRAHUNOK");
            //    dt.Columns.Add("clientBankCode");
            //    dt.Columns.Add("STATE");

            //    ds.Tables.Add(dt); 
            //    for (int i = 0; i <= bank.Count - 1; i++)
            //    {
            //        DataRow row = ds.Tables["BANK"].NewRow(); 
            //            row["NAME"] = banks[i].name;
            //            row["MFO"] = banks[i].mfo;
            //            row["ERDPOU"] = banks[i].edrpou;  
            //        row["RRAHUNOK"] = banks[i].rahunok; 
            //        row["clientBankCode"] = banks[i].clientBankCode;
            //        row["STATE"] = banks[i].isAval;

            //        ds.Tables["BANK"].Rows.Add(row); 

            //    }

            //    ds.Load(path);
            //     MessageBox.Show("XML файл успішно збережений." + banks.Count, "Виконано.");
            //}
            //catch (System.Exception ex)
            //{
            //    MessageBox.Show("Неможливо зберегти дані в XML файл.", "Помилка.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    MessageBox.Show(ex.Message);
            //}



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
        //    XmlElement recivPayNumt = xDoc.CreateElement("clientBankCode");
        //    XmlElement edrpou = xDoc.CreateElement("edrpou");
        //    XmlElement state = xDoc.CreateElement("state");

        //    // создаем текстовые значения для элементов и атрибута

        //    XmlText nameText = xDoc.CreateTextNode(bank.name);
        //    XmlText platNumberText = xDoc.CreateTextNode(bank.platNumber.ToString());
        //    XmlText datePaymentText = xDoc.CreateTextNode(bank.datePayment.ToString());
        //    XmlText mfoText = xDoc.CreateTextNode(bank.mfo);
        //    XmlText rahunokText = xDoc.CreateTextNode(bank.rahunok);
        //    XmlText cliBankCodeText = xDoc.CreateTextNode(bank.cliBankCode);
        //    XmlText recivPayNumtText = xDoc.CreateTextNode(bank.clientBankCode);
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
        //            if (childnode.Name == "clientBankCode")
        //                bank.clientBankCode = childnode.InnerText;
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
