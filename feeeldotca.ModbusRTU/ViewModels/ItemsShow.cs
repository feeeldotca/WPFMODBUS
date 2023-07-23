using feeeldotca.ModbusRTU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace feeeldotca.ModbusRTU.ViewModels
{
    public class ItemsShow
    {
        public List<ItemsModel> ItemList { get; set; } = new List<ItemsModel>();
        public ItemsShow()
        {
            ItemList.Add(new ItemsModel() { SlaveId = 1 }); ;
            ItemList.Add(new ItemsModel() { SlaveId = 2 });
            ItemList.Add(new ItemsModel() { SlaveId = 3 });
            ItemList.Add(new ItemsModel() { SlaveId = 4 });
        }

    }
}
