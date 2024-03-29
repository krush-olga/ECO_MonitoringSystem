﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ChartModule
{
    public partial class ChartM : Form
    {
        private String title;
        private String legend;
        private Object[] arrayOfX;
        private Object[] arrayOfY;

        private Button printButton = new Button();
        private SeriesChartType chartType; // переменная для хранения типа графика

        public ChartM(String legend, String chartType) : base()
        {
            //InitializeComponent();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            //   System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.SuspendLayout();
            //
            // chart1
            //
            this.title = "";
            this.legend = legend;
            setChartType(chartType);
            chartArea1.Name = "ChartArea";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = this.legend;
            this.chart1.Titles.Add(getTitle());
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(100, 12);
            this.chart1.Name = "chart";
            // series1.ChartArea = "ChartArea";
            // series1.Legend = legend;
            //  series1.Name = "Series";
            //this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(500, 500);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart";
            //this.chart1.Click += new System.EventHandler(this.chart1_Click);
            //
            // Chart
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 600);
            this.Controls.Add(this.chart1);
            this.Name = "Chart";
            this.Text = "Графік";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

            printButton.Text = "Друк";
            printButton.Click += new EventHandler(printButt_Click);
            printButton.Location = new System.Drawing.Point(625, 12);
            this.Controls.Add(printButton);
        }

        public void draw(Object[] arrayOfX, Object[] arrayOfY)
        {
            this.chart1.Series.Clear();
            this.chart1.ChartAreas[0].AxisX.Title = "Серії розрахунків";
            this.chart1.ChartAreas[0].AxisY.Title = "Значення";
            Series series = this.chart1.Series.Add("");
            if (chartType == SeriesChartType.Spline)
            {
                if (arrayOfX.Length == 1 & arrayOfY.Length == 1)
                {
                    series.ChartType = SeriesChartType.Point;
                }
                else
                {
                    series.ChartType = chartType;
                }
            }
            else
            {
                series.ChartType = chartType;
            }
            series.LegendText = legend;
            series.Color = Color.RoyalBlue;
            for (int i = 0; i < arrayOfX.Length; i++)
            {
                series.Points.AddXY(arrayOfY[i], arrayOfX[i]);
            }
        }

        public void drawIssue(List<object> serias, List<List<object>> listsOfX, List<object> names)
        {
            this.chart1.Series.Clear();
            this.chart1.ChartAreas[0].AxisX.Title = "Серії розрахунків";
            this.chart1.ChartAreas[0].AxisY.Title = "Значення";
            for (int i = 0; i < names.Count; i++)
            {
                chart1.Series.Add(names[i].ToString());
            }

            for (int i = 0; i < listsOfX.Count; i++)
            {
                for (int j = 0; j < listsOfX[i].Count; j++)
                {
                    if (chartType == SeriesChartType.Spline)
                    {
                        if (listsOfX[i].Count == 1)
                        {
                            chart1.Series[i].ChartType = SeriesChartType.Point;
                        }
                        else
                        {
                            chart1.Series[i].ChartType = chartType;
                        }
                    }
                    else
                    {
                        chart1.Series[i].ChartType = chartType;
                    }
                    chart1.Series[i].Points.AddXY(serias[j], listsOfX[i][j]);
                }
            }
        }

        public void setChartType(String chartType)
        {
            if (chartType.Equals("line"))
            {
                setChartType(SeriesChartType.Spline);
            }
            else if (chartType.Equals("range"))
            {
                setChartType(SeriesChartType.RangeColumn);
            }
        }

        public void setChartType(SeriesChartType chartType)
        {
            this.chartType = chartType;
        }

        public SeriesChartType getChartType()
        {
            return chartType;
        }

        public void setTitle(String title)
        {
            this.title = title;
        }

        public String getTitle()
        {
            return this.title;
        }

        public void setLegend(String legend)
        {
            this.legend = legend;
        }

        public String getLegend()
        {
            return this.legend;
        }

        public void setArrayOfX(Object[] arrayOfX)
        {
            this.arrayOfX = arrayOfX;
        }

        public Object[] getArrayOfX()
        {
            return this.arrayOfX;
        }

        public void setArrayOfY(Object[] arrayOfY)
        {
            this.arrayOfY = arrayOfY;
        }

        public Object[] getArrayOfY()
        {
            return this.arrayOfY;
        }

        // метод обрабатывает событие нажатия кнопки Друк
        private void printButt_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            PrintPreviewDialog printDialog = new PrintPreviewDialog();
            printDoc.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            printDialog.Document = printDoc;
            printDialog.ShowDialog();
        }

        // метод создает изображения для печати
        private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            // Создает структуру прямоугольника для хранения изображения графика
            Rectangle rectangle = new System.Drawing.Rectangle(10, 30, 800, 1100);
            chart1.Printing.PrintPaint(ev.Graphics, rectangle);
        }
    }
}