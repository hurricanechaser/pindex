using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Collections;
using Utilities.XAMLData;
using System.Security;

namespace Utilities.Visifire
{
    public static class VisifireExtension
    {

        public static void WriteColumnChart(this HttpResponse httpResponse, int width, int height, int yAxisMinimum, int yAxisMaximum, string title, string xAxisTitle, string yAxisTitle, IEnumerable<XLabel_YValue> query)
        {
            Func<IEnumerable<XLabel_YValue>, string> SerializeasXAML = ((IEnumerable<XLabel_YValue> coll) =>
            {
                StringBuilder xaml = new StringBuilder();
                foreach (XLabel_YValue o in coll)
                    xaml.AppendLine().AppendFormat(@"<vc:DataPoint AxisXLabel=""{0}""  YValue=""{1}"" />", SecurityElement.Escape(o.XLabel), SecurityElement.Escape(o.YValue));
                return xaml.ToString();
            });
            httpResponse.Write(string.Format(Properties.Settings.Default.ColumnGraphXaml, width, height, title, xAxisTitle, yAxisTitle, yAxisMinimum, yAxisMaximum, SerializeasXAML(query)));
        }
        public static void WriteColumnChartWithTrendLine(this HttpResponse httpResponse, int width, int height, int yAxisMinimum, int yAxisMaximum, string title, string xAxisTitle, string yAxisTitle, int trendLineValue, string TrendLineTooltip, IEnumerable<XLabel_YValue> query)
        {
            Func<IEnumerable<XLabel_YValue>, string> SerializeasXAML = ((IEnumerable<XLabel_YValue> coll) =>
            {
                StringBuilder xaml = new StringBuilder();
                foreach (XLabel_YValue o in coll)
                    xaml.AppendLine().AppendFormat(@"<vc:DataPoint AxisXLabel=""{0}""  YValue=""{1}"" />", SecurityElement.Escape(o.XLabel), SecurityElement.Escape(o.YValue));
                return xaml.ToString();
            });
            string trendLine = string.Format(@"<vc:Chart.TrendLines><vc:TrendLine  Value=""{0}"" Enabled=""True"" ToolTipText=""{1}""></vc:TrendLine></vc:Chart.TrendLines>", trendLineValue, SecurityElement.Escape(TrendLineTooltip));
            httpResponse.Write(string.Format(Properties.Settings.Default.ColumnGraphXamlWithTrendLines, width, height, title, trendLine, xAxisTitle, yAxisTitle, yAxisMinimum, yAxisMaximum, SerializeasXAML(query)));
        }
        public static void WriteColumnChart(this HttpResponse httpResponse, int width, int height, int yAxisMinimum, int yAxisMaximum, string title, string xAxisTitle, string yAxisTitle, IEnumerable<XLabel_XValue_Href> query)
        {
            Func<IEnumerable<XLabel_XValue_Href>, string> SerializeasXAML = ((IEnumerable<XLabel_XValue_Href> coll) =>
            {
                StringBuilder xaml = new StringBuilder();
                foreach (XLabel_XValue_Href o in coll)
                    xaml.AppendLine().AppendFormat(@"<vc:DataPoint AxisXLabel=""{0}"" Href=""{2}"" YValue=""{1}"" Cursor=""Hand"" />", SecurityElement.Escape(o.XLabel), SecurityElement.Escape(o.YValue), SecurityElement.Escape(o.Href));
                return xaml.ToString();
            });
            httpResponse.Write(string.Format(Properties.Settings.Default.ColumnGraphXaml, width, height, title, xAxisTitle, yAxisTitle, yAxisMinimum, yAxisMaximum, SerializeasXAML(query)));
        }
        public static void WriteColumnChartWithTrendLine(this HttpResponse httpResponse, int width, int height, int yAxisMinimum, int yAxisMaximum, string title, string xAxisTitle, string yAxisTitle, int trendLineValue, string TrendLineTooltip, IEnumerable<XLabel_XValue_Href> query)
        {
            Func<IEnumerable<XLabel_XValue_Href>, string> SerializeasXAML = ((IEnumerable<XLabel_XValue_Href> coll) =>
            {
                StringBuilder xaml = new StringBuilder();                
                foreach (XLabel_XValue_Href o in coll)
                    xaml.AppendLine().AppendFormat(@"<vc:DataPoint AxisXLabel=""{0}"" Href=""{2}"" YValue=""{1}"" Cursor=""Hand"" />", SecurityElement.Escape(o.XLabel), SecurityElement.Escape(o.YValue), SecurityElement.Escape(o.Href));
                return xaml.ToString();
            });
            string trendLine = string.Format(@"<vc:Chart.TrendLines><vc:TrendLine  Value=""{0}"" Enabled=""True"" ToolTipText=""{1}""></vc:TrendLine></vc:Chart.TrendLines>", trendLineValue, SecurityElement.Escape(TrendLineTooltip));
            httpResponse.Write(string.Format(Properties.Settings.Default.ColumnGraphXamlWithTrendLines, width, height, title,trendLine, xAxisTitle, yAxisTitle, yAxisMinimum, yAxisMaximum, SerializeasXAML(query)));
        }
        public static void WriteLineChart(this HttpResponse httpResponse, int width, int height, int yAxisMinimum, int yAxisMaximum, string title, string xAxisTitle, string yAxisTitle, IEnumerable<XLabel_YValueCollec> query)
        {
            Func<IEnumerable<XLabel_YValueCollec>, string> SerializeasXAML = ((IEnumerable<XLabel_YValueCollec> coll) =>
            {
                StringBuilder xaml = new StringBuilder();
                foreach (XLabel_YValueCollec o in coll)
                {
                    xaml.AppendFormat(@"<vc:DataSeries LegendText=""{0}"" Legend=""Legend1"" RenderAs=""Line"" MarkerSize=""8"" LineThickness=""3.5"" SelectionEnabled=""True"" MovingMarkerEnabled=""True"" ><vc:DataSeries.DataPoints>", o.LegendText).AppendLine();
                    foreach (XLabel_YValue obj in o.Collec)
                        xaml.AppendLine().AppendFormat(@"<vc:DataPoint AxisXLabel=""{0}"" YValue=""{1}"" />", SecurityElement.Escape(obj.XLabel), SecurityElement.Escape(obj.YValue)).AppendLine();
                    xaml.Append(@"</vc:DataSeries.DataPoints></vc:DataSeries>").AppendLine();
                }
                return xaml.ToString();
            });
            httpResponse.Write(string.Format(Properties.Settings.Default.LineGraphXaml, width, height, title, xAxisTitle, yAxisMinimum, yAxisMaximum, yAxisTitle, SerializeasXAML(query)));
        }
        public static void WriteLineChart(this HttpResponse httpResponse, int width, int height, int yAxisMinimum, int yAxisMaximum, string title, string xAxisTitle, string yAxisTitle, IEnumerable<XLabel_XValue_HrefCollec> query)
        {
            Func<IEnumerable<XLabel_XValue_HrefCollec>, string> SerializeasXAML = ((IEnumerable<XLabel_XValue_HrefCollec> coll) =>
            {
                StringBuilder xaml = new StringBuilder();
                foreach (XLabel_XValue_HrefCollec o in coll)
                {
                    xaml.AppendFormat(@"<vc:DataSeries LegendText=""{0}"" Legend=""Legend1"" RenderAs=""Line""  MarkerSize=""8"" LineThickness=""3.5"" SelectionEnabled=""True"" MovingMarkerEnabled=""True"" ><vc:DataSeries.DataPoints>", o.LegendText).AppendLine();
                    foreach (XLabel_XValue_Href obj in o.Collec)
                        xaml.AppendLine().AppendFormat(@"<vc:DataPoint AxisXLabel=""{0}"" Href=""{2}"" YValue=""{1}"" Cursor=""Hand"" />", SecurityElement.Escape(obj.XLabel), SecurityElement.Escape(obj.YValue), SecurityElement.Escape(obj.Href)).AppendLine();
                    xaml.Append(@"</vc:DataSeries.DataPoints></vc:DataSeries>").AppendLine();
                }
                return xaml.ToString();
            });
            httpResponse.Write(string.Format(Properties.Settings.Default.LineGraphXaml, width, height, title, xAxisTitle, yAxisMinimum, yAxisMaximum, yAxisTitle, SerializeasXAML(query)));
        }
        public static void WriteBarChart(this HttpResponse httpResponse, int width, int height, string title, string xAxisTitle, string yAxisTitle, IEnumerable<XLabel_YValue> query)
        {
            Func<IEnumerable<XLabel_YValue>, string> SerializeasXAML = ((IEnumerable<XLabel_YValue> coll) =>
            {
                StringBuilder xaml = new StringBuilder(@"<vc:DataSeries RenderAs=""Column"" AxisYType=""Primary"" ><vc:DataSeries.DataPoints>");
                foreach (XLabel_YValue o in coll)
                    xaml.AppendLine().AppendFormat(@"<vc:DataPoint AxisXLabel=""{0}"" YValue=""{1}"" />", SecurityElement.Escape(o.XLabel), SecurityElement.Escape(o.YValue));
                return xaml.AppendLine().Append(@"</vc:DataSeries.DataPoints></vc:DataSeries>").ToString();
            });
            httpResponse.Write(string.Format(Properties.Settings.Default.ColumnGraphXaml, width, height, title, xAxisTitle, yAxisTitle, SerializeasXAML(query)));
        }
    }
}
