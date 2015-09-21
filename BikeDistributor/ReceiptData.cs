using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeDistributor
{
    public class ReceiptData
    {
        public string company;
        public string totalAmount;
        public TupleList<Line, string> reportLines;
        public string tax;
        public string total;

        public ReceiptData(string company,
                           string totalAmount,
                           TupleList<Line, string> reportLines,
                           string tax,
                           string total)
        {
            this.company = company;
            this.totalAmount = totalAmount;
            this.reportLines = reportLines;
            this.tax = tax;
            this.total = total;
        }
    }
}
