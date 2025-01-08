using System;
using Business.BusinessClasses;
using Business.ViewModels;
using Business.BusinessInterfaces;
using Repository.RepositoryClasses;

namespace InventoryCRUD
{
    public class Item
    {
        public ItemViewModel vm;
        public IItemBusiness b;
        public Item()
        {
            vm = new ItemViewModel();
            b = new ItemBusiness(new ItemRepo());
        }
        
        public static void Main(string[] args)
        {
            //Declaring and Intstantiating Business class object
            bool tempVariable = true;
            Item item = new Item();
            //Do while loop iteration
            do
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to Inventory items CRUD");
                Console.WriteLine("1.Create");
                Console.WriteLine("2.Display");
                Console.WriteLine("3.Update");
                Console.WriteLine("4.Delete");
                Console.WriteLine("5.Exit");
                int option = default;
                //Exception Handling for reading data from Console
                try
                {
                    option = int.Parse(Console.ReadLine());
                }
                catch(Exception e)

                { Console.WriteLine("Enter only numbers"); }
                //switch case execution based on the input option
                switch (option)
                {
                    //Insertion
                    case 1:
                        item.InsertanItem();
                        break;

                    //Displaying 
                    case 2:
                        item.DisplayItems();
                        break;

                    //updation
                    case 3:
                        item.UpdateItem();
                        break;

                    //Deletion
                    case 4:
                        item.DeleteItem();
                        break;

                    case 5:
                        //setting tempVariable to false to exit do-while loop
                        tempVariable = false;
                        break;
                    }
            } while (tempVariable) ;
        }

        public  void InsertanItem()
        {
            try
            {
                //Reading
                Console.Write("Name : ");
                vm.itemName = Console.ReadLine();
                Console.Write("Description : ");
                vm.itemDescription = Console.ReadLine();
                Console.Write("Price : ");
                vm.itemPrice = Decimal.Parse(Console.ReadLine());
                //passing viewmodel to business logic method for validation
                var insertedItemID = b.InsertLogic(vm);
                if (insertedItemID != 0) Console.WriteLine("Sucessfully Inserted with ItemID " + insertedItemID.ToString());
                else Console.WriteLine("Insertion Failed");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Incorrect format");
            }
        }

        public void DisplayItems()
        {
            //Getting items from repository through business class as ViewModels
            var itemsList = b.DisplayAllItems();

            //Validating items
            if (itemsList == null || itemsList == default || itemsList.Count == 0)
            {
                Console.WriteLine("No items to display");
            }
            else
            {
                //Items display using foreach loop
                Console.WriteLine("Available items as below");

                Console.WriteLine("ItemId - Item name - Item Description - Item Price");
                Console.WriteLine("--------------------------------------------------");
                foreach (var i in itemsList)
                {
                    Console.WriteLine(i.itemId.ToString() + "-----" + i.itemName + "--" + i.itemDescription + "--" + i.itemPrice.ToString());
                }
            }
        }

        public void UpdateItem()
        {
            try
            {
                //Reading ItemId to update the specific item
                Console.WriteLine("Enter Item ID to update details");
                Console.Write("ID : ");
                var updateItemId = int.Parse(Console.ReadLine());
                #region ItemID Validation
                //Validating ItemId
                if (updateItemId <= 0)
                {
                    Console.WriteLine("Item Id should not be 0 or Lessthan");
                }
                else
                {
                    //Verifying the ItemId
                    var existingItem = b.GetItemDetailsByItemID(updateItemId);

                    if (existingItem == null)
                    {
                        Console.WriteLine("Could not found the item with this ID");
                    }
                    #endregion
                    else
                    {
                        //Reading data to be updated
                        Console.Write("Name : ");
                        existingItem.itemName = Console.ReadLine();
                        Console.Write("Description : ");
                        existingItem.itemDescription = Console.ReadLine();
                        Console.Write("Price : ");
                        existingItem.itemPrice = Decimal.Parse(Console.ReadLine());
                        //Passing the Viewmodel to Business UpdateItem method to convert and update into Item Model
                        var updated = b.UpdateItem(existingItem);
                        if (updated != 0)
                        {
                            Console.WriteLine("Sucessfully updated");
                        }
                        else
                        {
                            Console.WriteLine("Update failed");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Incorrect format");
            }
        }

        public void DeleteItem()
        {
            try
            {
                //Reading the ItemId
                Console.WriteLine(" Enter the Item Id to delete");
                Console.Write("ID : ");
                #region ItemID Validation
                var deleteItemId = int.Parse(Console.ReadLine());
                //ItemId Validation
                if (deleteItemId <= 0)
                {
                    Console.WriteLine("Item Id should not be 0 or Lessthan");
                }
                else
                {
                    //Verifying ItemId
                    var deleteItem = b.GetItemDetailsByItemID(deleteItemId);
                    if (deleteItem == null)
                    {
                        Console.WriteLine("Could not found the item with this ID");
                    }
                    else
                    {
                        #endregion
                        //Passing itemId to delete 
                        b.DeleteItemByItemID(deleteItemId);
                        Console.WriteLine("Sucessfully deleted");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Incorrect format");
            }
        }
    }
}
