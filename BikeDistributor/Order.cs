using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BikeDistributor
{
    public class Order
    {
        private const double TaxRate = .0725d;
        private readonly IList<Line> _lines = new List<Line>();

        public Order(string company)
        {
            Company = company;
        }

        public string Company { get; private set; }

        public void AddLine(Line line)
        {
            _lines.Add(line);
        }

        public string Receipt()
        {
            var totalAmount = 0d;
            var reportLines = new TupleList<Line, string>();
            foreach (var line in _lines)
            {
                var thisAmount = 0d;
                switch (line.Bike.Price)
                {
                    case Bike.OneThousand:
                        if (line.Quantity >= 20)
                            thisAmount += line.Quantity * line.Bike.Price * .9d;
                        else
                            thisAmount += line.Quantity * line.Bike.Price;
                        break;
                    case Bike.TwoThousand:
                        if (line.Quantity >= 10)
                            thisAmount += line.Quantity * line.Bike.Price * .8d;
                        else
                            thisAmount += line.Quantity * line.Bike.Price;
                        break;
                    case Bike.FiveThousand:
                        if (line.Quantity >= 5)
                            thisAmount += line.Quantity * line.Bike.Price * .8d;
                        else
                            thisAmount += line.Quantity * line.Bike.Price;
                        break;
                }
                reportLines.Add(line, thisAmount.ToString("C"));
                totalAmount += thisAmount;
            }
            var tax = totalAmount * TaxRate;

            var receipt = new TextReceipt(new BaseReceiptData(Company,
                                                              totalAmount.ToString("C"),
                                                              reportLines,
                                                              tax.ToString("C"),
                                                              (totalAmount + tax).ToString("C")));
            var receiptText = receipt.TransformText();

            return receiptText;
        }

        public string HtmlReceipt()
        {
            var totalAmount = 0d;
            var reportLines = new TupleList<Line, string>();
            
            if (_lines.Any())
            {
                foreach (var line in _lines)
                {
                    var thisAmount = 0d;
                    switch (line.Bike.Price)
                    {
                        case Bike.OneThousand:
                            if (line.Quantity >= 20)
                                thisAmount += line.Quantity*line.Bike.Price*.9d;
                            else
                                thisAmount += line.Quantity*line.Bike.Price;
                            break;
                        case Bike.TwoThousand:
                            if (line.Quantity >= 10)
                                thisAmount += line.Quantity*line.Bike.Price*.8d;
                            else
                                thisAmount += line.Quantity*line.Bike.Price;
                            break;
                        case Bike.FiveThousand:
                            if (line.Quantity >= 5)
                                thisAmount += line.Quantity*line.Bike.Price*.8d;
                            else
                                thisAmount += line.Quantity*line.Bike.Price;
                            break;
                    }
                    reportLines.Add(line, thisAmount.ToString("C"));
                    totalAmount += thisAmount;
                }
            }
            var tax = totalAmount * TaxRate;

            var receipt = new HtmlReceipt(new BaseReceiptData(Company,
                                                              totalAmount.ToString("C"),
                                                              reportLines,
                                                              tax.ToString("C"),
                                                              (totalAmount + tax).ToString("C")));
            var receiptHTML = receipt.TransformText();

            return receiptHTML;
        }

    }
}
