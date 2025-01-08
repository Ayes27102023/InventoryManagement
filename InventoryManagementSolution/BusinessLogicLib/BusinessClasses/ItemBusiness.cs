using Business.BusinessInterfaces;
using Business.ViewModels;
using Repository.DataModels;
using Repository.RepositoryInterfaces;

namespace Business.BusinessClasses
{
    //Contains All the Implented Business Logic methods declared in the Business Interface
    public class ItemBusiness : IItemBusiness
    {
        //Declaration of required components
        IItemRepo repo;

        //Constructor based dependency injection
        public ItemBusiness(IItemRepo itemRepo)
        {
            repo = itemRepo;
        }

        //Item Insertion
        public int InsertLogic(ItemViewModel vm)
        {
            #region Validation of Viewmodels
            //ViewModel validations
            if (vm.itemName == default || vm.itemName == null || vm.itemName == "")
            {
                Console.WriteLine("Enter Name");
                return 0;
            }
            if (vm.itemDescription == default || vm.itemDescription == null || vm.itemDescription == "")
            {
                Console.WriteLine("Enter Description");
                return 0;
            }
            if (vm.itemPrice <= 0)
            {
                Console.WriteLine("Enter Price");
                return 0;
            }
            #endregion
            //Instantiating new model to save
            ItemModel model = new ItemModel();
            //generating and intializing auto-incremented ItemId
            var existingItems = repo.GetAll();
            if(existingItems.Count == 0)
            {
                model.itemId = 1;
            }
            else
            {
                var ItemIdList = existingItems.Select(x => x.itemId).ToList();
                model.itemId = ItemIdList.Max() + 1;
            }
            //model.itemId = repo.GetAll().Count + 1;
            model.itemName = vm.itemName;
            model.itemDescription = vm.itemDescription;
            model.itemPrice = vm.itemPrice;
            //Calling repository method to save in to collection
            repo.SaveItem(model);
            //checking wheather inserted or not
            var insertedItem = repo.GetItem(model.itemId);
            if (insertedItem != null)
            {
                return (insertedItem.itemId);
            }
            else return 0;
        }


        //Business logic to display all Items
        public List<ItemViewModel> DisplayAllItems()
        {
            //Getting all items
            var itemsList = repo.GetAll();
            if (itemsList == null || itemsList == default || itemsList.Count == 0)
            {
                return new List<ItemViewModel>();
            }
            else
            {
                //logic to make a resultant viewModels from collection  to display 
                var result = new List<ItemViewModel>();
                foreach (var item in itemsList)
                {
                    result.Add(new ItemViewModel()
                    {
                        itemId = item.itemId,
                        itemName = item.itemName,
                        itemDescription = item.itemDescription,
                        itemPrice = item.itemPrice
                    }
                    );
                }
                return result;
            }
        }

        //Method gets Item from collection if exists or else returns null
        public ItemViewModel? GetItemDetailsByItemID(int itemId)
        {
            var item = repo.GetItem(itemId);
            if (item == null) return null;
            //returning the converted ViewModel
            return new ItemViewModel()
            {
                itemId = itemId,
                itemName = item.itemName,
                itemDescription = item.itemDescription,
                itemPrice = item.itemPrice
            };
        }

        //Updating existing Item from collection
        public int UpdateItem(ItemViewModel vm)
        {   
            //Validations for the ViewModel
            #region Validation
            if (vm.itemName == default || vm.itemName == null || vm.itemName == "")
            {
                Console.WriteLine("Enter Name");
                return 0;
            }
            if (vm.itemDescription == default || vm.itemDescription == null || vm.itemDescription == "")
            {
                Console.WriteLine("Enter Description");
                return 0;
            }
            if (vm.itemPrice <= 0)
            {
                Console.WriteLine("Enter Price");
                return 0;
            }
            #endregion
            //assigning the updated values
            var updateModel = repo.GetItem(vm.itemId);
            updateModel.itemName = vm.itemName;
            updateModel.itemDescription = vm.itemDescription;
            updateModel.itemPrice = vm.itemPrice;
            updateModel.isUpdated = true;
            updateModel.updatedOn = DateTime.Now;
            repo.UpdateItem(updateModel);
            return updateModel.itemId;
        }

        //Method to delete item from collection
        public void DeleteItemByItemID(int itemId)
        {
            repo.DeleteItem(itemId);
        }
    }
}
