using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomBusinessLogic.OfficePackage.HelperEnums;

namespace DiningRoomBusinessLogic.OfficePackage.HelperModels
{
    public class PdfRowParameters
    {
        public List<string> Texts { get; set; }
        public string Style { get; set; }
        public PdfParagraphAlignmentType ParagraphAlignment { get; set; }

    }
}
