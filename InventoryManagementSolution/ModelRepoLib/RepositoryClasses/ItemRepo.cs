using Repository.DataModels;
using Repository.RepositoryInterfaces;

namespace Repository.RepositoryClasses
{
    public class ItemRepo : IItemRepo
    {
        //Declaring a runtime object to save items
        List<ItemModel> items;
        //Intializing the required extensions in Default Constructor using Constructor based dependency injection
        public ItemRepo()
        {
            items = new List<ItemModel>();
        }

        //To save in collection
        public void SaveItem(ItemModel model)
        {
            items.Add(model);
        }

        //To delete an item from collection
        public void DeleteItem(int id)
        {
            items.Remove(items.Where(x => x.itemId == id).First());
        }

        //To update an item from collection
        public void UpdateItem(ItemModel model)
        {
            //Removing the existing item
            items.Remove(items.Where(x => x.itemId == model.itemId).First());
            //inserting an updated item
            items.Add(model);
        }

        //Get an Item through item Id
        public ItemModel? GetItem(int id)
        {
            //If finds I will return Item or else null value
            return items.Where(x => x.itemId == id).FirstOrDefault();
        }

        //To get all Items from collection
        public List<ItemModel> GetAll()
        {
            return items.ToList();
        }

    }
}
