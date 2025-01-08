using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.DataModels;

namespace Repository.RepositoryInterfaces
{
    public interface IItemRepo
    {
        public void SaveItem(ItemModel model);

        public void DeleteItem(int id);

        public void UpdateItem(ItemModel model);

        public ItemModel? GetItem(int id);

        public List<ItemModel> GetAll();
    }
}
