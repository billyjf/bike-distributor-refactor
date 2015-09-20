using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    public class BaseReceiptData
    {
        public string m_Company;
        public string m_totalAmount;
        public TupleList<Line, string> m_reportLines;
        public string m_tax;
        public string m_total;

        public BaseReceiptData(string company,
                               string totalAmount,
                               TupleList<Line, string> reportLines,
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
