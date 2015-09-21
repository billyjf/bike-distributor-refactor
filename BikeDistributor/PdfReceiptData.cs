using System;
using System.IO;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;

namespace BikeDistributor
{
    public partial class PdfReceipt
    {
        private ReceiptData data;
        private string Base64EncodedPDF;
        private Document document;

        // PDF Report Objects
        private Section section;

        public PdfReceipt(ReceiptData data)
        {
            this.data = data;
            this.Base64EncodedPDF = GeneratePDF();
        }

        public string GeneratePDF()
        {
            this.document = new Document();
            var titleSubject = "Order Receipt for " + data.company;
            this.document.Info.Title = titleSubject;
            this.document.Info.Subject = titleSubject;
            this.document.Info.Author = this.data.company;

            DefineStyles();
            CreatePage();
            FillContent();

            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(false, PdfFontEmbedding.None);
            pdfRenderer.Document = this.document;
            pdfRenderer.RenderDocument();
            MemoryStream stream = new MemoryStream();
            pdfRenderer.PdfDocument.Save(stream, true);
            var result = Convert.ToBase64String(stream.ToArray());

            return result;
        }

        private void DefineStyles()
        {
            // Get the predefined style Normal.
            Style style = this.document.Styles["Normal"];
            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = "Verdana";

            // Create new styles to mimic HTML format for ease of reference
            style = this.document.Styles.AddStyle("h1", "Normal");
            style.ParagraphFormat.Font.Name = "Times New Roman";
            style.ParagraphFormat.Font.Bold = true;
            style.ParagraphFormat.Font.Size = 14;
            style.ParagraphFormat.SpaceAfter = 3;
            style.ParagraphFormat.Font.Bold = true;

            style = this.document.Styles.AddStyle("h2", "Normal");
            style.ParagraphFormat.Font.Name = "Times New Roman";
            style.ParagraphFormat.Font.Bold = true;
            style.ParagraphFormat.Font.Size = 12;
            style.ParagraphFormat.SpaceAfter = 3;
            style.ParagraphFormat.Font.Bold = true;

            style = this.document.Styles.AddStyle("h3", "Normal");
            style.ParagraphFormat.Font.Name = "Times New Roman";
            style.ParagraphFormat.Font.Bold = true;
            style.ParagraphFormat.Font.Size = 10;
            style.ParagraphFormat.SpaceAfter = 3;
            style.ParagraphFormat.Font.Bold = true;
        }

        private void CreatePage()
        {
            this.section = this.document.AddSection();

            Paragraph paragraph = this.section.AddParagraph(String.Format("Order Receipt for {0}", this.data.company));
            paragraph.Style = "h1";
            paragraph.Format.SpaceAfter = 3;
        }

        private void FillContent()
        {
            Paragraph paragraph;

            // Iterate the invoice items
            foreach(Tuple<Line, string>line in data.reportLines)
            {
                paragraph = section.AddParagraph();
                paragraph.AddCharacter(SymbolName.Tab);
                paragraph.AddCharacter(SymbolName.Bullet);
                paragraph.Add(new Text(String.Format(" {0} x {1} {2} = {3}", line.Item1.Quantity.ToString(),
                                                                             line.Item1.Bike.Brand,
                                                                             line.Item1.Bike.Model,
                                                                             line.Item2)));
            }

            paragraph = section.AddParagraph();
            paragraph.AddCharacter(SymbolName.ParaBreak);
            paragraph = section.AddParagraph(String.Format("Sub-Total: {0}", data.totalAmount.ToString()));
            paragraph.Style = "h3";
            paragraph = section.AddParagraph(String.Format("Tax: {0}", data.tax.ToString()));
            paragraph.Style = "h3";
            paragraph = section.AddParagraph(String.Format("Total: {0}", data.total.ToString()));
            paragraph.Style = "h2";
        }
    }
}
