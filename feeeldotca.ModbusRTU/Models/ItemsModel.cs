using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace feeeldotca.ModbusRTU.Models
{
    public class ItemsModel
    {
        public int SlaveId { get; set; } = 1;
        public double Temperature { get; set; } = 30;
    }
}
