﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Data.Entity.Migrations.Model;

namespace excelgeneralasa_ezajo
{
    public partial class Form1 : Form
    {

        RealEstateEntities context = new RealEstateEntities();
        List<Flat> Flats;

        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;

        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
        }
        private void LoadData()
        {
            Flats = context.Flats.ToList();
        }

        private void CreateExcel()
        {
            try
            {
                
                xlApp = new Excel.Application();

          
                xlWB = xlApp.Workbooks.Add(Missing.Value);

                
                xlSheet = xlWB.ActiveSheet;

                
                CreateTable(); 

                
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) 
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        private void CreateTable()
        {
            string[] headers = new string[] {
                "Kód",
                "Eladó",
                "Oldal",
                "Kerület",
                "Lift",
                "Szobák száma",
                "Alapterület (m2)",
                "Ár (mFt)",
                "Négyzetméter ár (Ft/m2)"};
        }

    }
}
