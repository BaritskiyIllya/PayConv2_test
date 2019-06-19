﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace SoftGenConverter
{
    class Aval
    {
        
        public string name { get; set; }
        public int isAval { get; set; }
        public int datePayment { get; set; }
        public string mfo { get; set; }
        public string rahunok { get; set; }
        public string zkpo { get; set; }
        //public string cliBankCode { get; set; }
        public string recivPayNum { get; set; }
        public string summa { get; set; }
        public DateTime dateP { get; set; }

        public void piece(string line, DateTime date, bool aval)
        {



            {
                string[] parts = line.Split(';');  //Разделитель в CSV файле.
                if (aval)
                {
                    name = parts[0];
                    mfo = parts[2];
                    rahunok = parts[3];
                    zkpo = parts[4];
                    dateP = date;
                    summa = parts[6];
                    isAval = 1;
                }
                else
                {
                    name = parts[0];
                    mfo = parts[1];
                    rahunok = parts[2];
                    zkpo = parts[3];
                    summa = parts[5];
                    isAval = 0;
                }
                
            }


        }
        public static List<Aval> ReadFile(string filename)
        {
            List<Aval> res = new List<Aval>();
            int date = Properties.Settings.Default.datePayment;
            Regex regexDate = new Regex(@"\w*[0-9]{2}[.][0-9]{2}[.][0-9]{2}р.");
            Regex regexLine = new Regex(@".+;.*;.+;.+;.+;.+;.*;.*;.+;.*");
            bool flag = false;
            bool aval = false;
        DateTime datePl = new DateTime();



            using (StreamReader sr = new StreamReader(filename, Encoding.GetEncoding(1251)))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    MatchCollection dateMatch = regexDate.Matches(line);
                    if (dateMatch.Count > 0)
                    {
                        CultureInfo MyCultureInfo = new CultureInfo("de-DE");
                        //MessageBox.Show(line);
                        MatchCollection matchess = Regex.Matches(line, regexDate.ToString(), RegexOptions.IgnoreCase);
                        date = Int32.Parse(matchess[0].ToString().Replace("за", "").Replace("р.", "").Trim().Replace(".", ""));
                        datePl = DateTime.Parse(matchess[0].ToString().Replace("за", "").Replace("р.", "").Trim(), MyCultureInfo);
                        flag = false;
                        aval = true;
                    }

                    
                    MatchCollection lineMatch = regexLine.Matches(line);
                    //MessageBox.Show(lines2);
                    if (lineMatch.Count > 0)
                    {
                        if (flag)
                        {
                            //MessageBox.Show(line);
                            if ((line.IndexOf("з банку \"АВАЛЬ\"")) > 0)
                            {
                               
                                flag = false;
                                aval = true;
                            }
                            Aval p = new Aval();
                            p.piece(line, datePl, aval);
                            res.Add(p);
                        }

                        flag = true;
                    }


                }
            }

            return res;
        }
    }
}
