using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiningRoomBusinessLogic.OfficePackage.HelperEnums;
using DiningRoomBusinessLogic.OfficePackage.HelperModels;

namespace DiningRoomBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToWord
    {
        public void CreateDoc(WordInfo info)
        {
            CreateWord(info);
            CreateParagraph(new WordParagraph
            {
                Texts = new List<(string, WordTextProperties)> { (info.Title, new WordTextProperties { Bold = true, Size = "24"}) },
                TextProperties = new WordTextProperties
                {
                    Size = "24",
                    JustificationType = WordJustificationType.Center
                }
            });
            foreach (var component in info.Cooks)
            {
                CreateParagraph(new WordParagraph
                {
                    Texts = new List<(string, WordTextProperties)> {("Повар: ", new WordTextProperties {Bold = true, Size = "24"}),
                        (component.Name, new WordTextProperties {Bold = false, Size = "24"})},
                    TextProperties = new WordTextProperties
                    {
                        Size = "24",
                        JustificationType = WordJustificationType.Both
                    }
                });
            }
            SaveWord(info);

        }
        protected abstract void CreateWord(WordInfo info);
        protected abstract void CreateParagraph(WordParagraph paragraph);
        protected abstract void SaveWord(WordInfo info);
    }
}
