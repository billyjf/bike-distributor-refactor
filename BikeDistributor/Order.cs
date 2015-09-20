﻿using System;
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

        private double CalculateAmountPlusDiscount(Bike bike, int quantity)
        {
            var amountWithDiscount = 0d;

            switch (bike.Price)
            {
                case Bike.OneThousand:
                    if (quantity >= 20)
                        amountWithDiscount += quantity * bike.Price * .9d;
                    else
                        amountWithDiscount += quantity * bike.Price;
                    break;
                case Bike.TwoThousand:
                    if (quantity >= 10)
                        amountWithDiscount += quantity * bike.Price * .8d;
                    else
                        amountWithDiscount += quantity * bike.Price;
                    break;
                case Bike.FiveThousand:
                    if (quantity >= 5)
                        amountWithDiscount += quantity * bike.Price * .8d;
                    else
                        amountWithDiscount += quantity * bike.Price;
                    break;
            }

            return amountWithDiscount;
        }

        public string Receipt()
        {
            var totalAmount = 0d;
            var reportLines = new TupleList<Line, string>();
            foreach (var line in _lines)
            {
                var thisAmount = 0d;
                thisAmount += CalculateAmountPlusDiscount(line.Bike, line.Quantity);
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
                    thisAmount += CalculateAmountPlusDiscount(line.Bike, line.Quantity);
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
