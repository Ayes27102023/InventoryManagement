using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.ViewModels;

namespace Business.BusinessInterfaces
{
    public interface IItemBusiness
    {
        public int InsertLogic(ItemViewModel vm);

        public List<ItemViewModel> DisplayAllItems();

        public ItemViewModel? GetItemDetailsByItemID(int itemId);

        public int UpdateItem(ItemViewModel vm);

        public void DeleteItemByItemID(int itemId);
    }
}
