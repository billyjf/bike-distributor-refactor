using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    partial class HtmlReceipt
    {
        private string m_Company;
        private string m_totalAmount;
        private TupleList<Line, string> m_reportLines;
        private string m_tax;
        private string m_total;

        public HtmlReceipt(string company,
                           string totalAmount,
                           TupleList<Line, string>reportLines,
                           string tax,
                           string total)
        {
            this.m_Company = company;
            this.m_totalAmount = totalAmount;
            this.m_reportLines = reportLines;
            this.m_tax = tax;
            this.m_total = total;
        }
    }
}
