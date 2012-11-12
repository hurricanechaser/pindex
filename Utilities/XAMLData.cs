using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.XAMLData
{
    public struct XLabel_YValue
    {
        public string XLabel;
        public string YValue;
    }
    public struct XLabel_XValue_Href
    {
        public string XLabel;
        public string YValue;
        public string Href;
    }
    public struct XLabel_YValueCollec
    {
        public string LegendText;
        public IEnumerable<XLabel_YValue> Collec;
    }
    public struct XLabel_XValue_HrefCollec
    {
        public string LegendText;
        public IEnumerable<XLabel_XValue_Href> Collec;
    }
}

