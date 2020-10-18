﻿using otodik_beadando_jo.Entities;
using otodik_beadando_jo.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace otodik_beadando_jo
{
    public partial class Form1 : Form
    {

        BindingList<RateData> rates = new BindingList<RateData>();
        public string result;

        public Form1()
        {
            InitializeComponent();

            refreshdata();
        }

        private void refreshdata()
        {
            rates.Clear();

            webszolg();
            feldolgozas();
            adatokmegjelenitese();

            dataGridView1.DataSource = rates;
        }

        private void webszolg()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = comboBox1.SelectedItem.ToString(),
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };

            var response = mnbService.GetExchangeRates(request);
            result = response.GetExchangeRatesResult;

        }

        private void feldolgozas()
        {
            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                rates.Add(rate);

                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }
        }

        private void adatokmegjelenitese()
        {
            chartRateData.DataSource = rates;

            var series = chartRateData.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chartRateData.Legends[0];
            legend.Enabled = false;

            var chartArea = chartRateData.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            refreshdata();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            refreshdata();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshdata();
        }
    }
}
