using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomContracts.BindingModels
{
    public class AddedProductCooksBindingModel
    {
        public int ProductId { get; set; }
        public List<int> CooksId { get; set; }
        public string Method { get; set; }
    }
}
