using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DataModels
{
    public class ItemModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime createdOn { get; set; } = DateTime.Now;
        public DateTime updatedOn { get; set; } = DateTime.Now;
        public bool isUpdated { get; set; } = false;
        public int itemId { get; set; }
        public string itemName { get; set; }
        public string itemDescription { get; set; }
        public decimal itemPrice { get; set; }

    }
}
