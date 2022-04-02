using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomBusinessLogic.OfficePackage.HelperEnums;
using DiningRoomBusinessLogic.OfficePackage.HelperModels;

namespace DiningRoomBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToPdf
    {
        public void CreateDoc(PdfInfo info)
        {
            CreatePdf(info);
            CreateParagraph(new PdfParagraph
            {
                Text = info.Title,
                Style = "NormalTitle"
            });
            CreateParagraph(new PdfParagraph
            {
                Text = $"с { info.DateAfter.ToShortDateString() } по { info.DateBefore.ToShortDateString() }", Style = "Normal"
            });
            CreateTable(new List<string> { "2cm", "2cm", "2cm", "5cm", "3cm", "3cm" });
            CreateRow(new PdfRowParameters
            {
                Texts = new List<string> { "Дата обеда", "Стоимость обеда", "Вес обеда", "Имя повара", "Название прибора", "Количество приборов" },
                Style = "NormalTitle",
                ParagraphAlignment = PdfParagraphAlignmentType.Center
            });
            foreach (var lunch in info.Lunches)
            {
                CreateRow(new PdfRowParameters
                {
                    Texts = new List<string> { lunch.DateCreate.ToShortDateString(), lunch.price.ToString(), lunch.Weight.ToString(), "", "", ""},
                    Style = "Normal",
                    ParagraphAlignment = PdfParagraphAlignmentType.Left
                });
                for (int i = 0; i < Math.Max(lunch.Cooks.Count, lunch.Cutleries.Count); ++i)
                {
                    PdfRowParameters newItem = new PdfRowParameters();
                    newItem.Style = "Normal";
                    newItem.ParagraphAlignment = PdfParagraphAlignmentType.Center;
                    newItem.Texts = new List<string> { "", "", ""};
                    if (i < lunch.Cooks.Count)
                    {
                        newItem.Texts.Add(lunch.Cooks[i].Name);
                    }
                    else
                    {
                        newItem.Texts.Add("");
                    }
                    if (i < lunch.Cutleries.Count)
                    {
                        newItem.Texts.Add(lunch.Cutleries[i].Name);
                        newItem.Texts.Add(lunch.Cutleries[i].Count.ToString());
                    }
                    else
                    {
                        newItem.Texts.Add("");
                        newItem.Texts.Add("");
                    }
                    CreateRow(newItem);
                }
            }
            SavePdf(info);
        }
        protected abstract void CreatePdf(PdfInfo info);
        protected abstract void CreateParagraph(PdfParagraph paragraph);
        protected abstract void CreateTable(List<string> columns);
        protected abstract void CreateRow(PdfRowParameters rowParameters);
        protected abstract void SavePdf(PdfInfo info);
    }
}
