using Business.BusinessClasses;
using Business.BusinessInterfaces;
using Business.ViewModels;
using Repository.RepositoryClasses;

namespace UnitTestProject
{
    public class Tests
    {
        //Declaration of objects required
        public ItemViewModel item {  get; set; }
        public IItemBusiness iBusiness;

        //Intialization of Test Data before executing every Test case
        [SetUp]
        public void Setup()
        {
             item = new ItemViewModel()
             {
                 //Initialising the Test Data to insert
                 itemName = "Test",
                 itemDescription = "descp",
                 itemPrice = 100
             };
             iBusiness = new ItemBusiness(new ItemRepo());
        }

        //Creation of Item Test
        [Test()]
        public void ItemCreation()
        {   
            //Calling the businessLogic to insert
            var InsertedItemID = iBusiness.InsertLogic(item);
            //Checking the result
            Assert.NotNull(InsertedItemID);
            Assert.NotZero(InsertedItemID);
        }

        //updating an Item Test
        [Test()]
        public void UpdateItem()
        {
            //Calling the businessLogic to insert
            item.itemId = iBusiness.InsertLogic(item);
           
            //Initializing the Updating the Test Data
            item.itemName = "Update Test";
            item.itemDescription = "Update descp";
            item.itemPrice = 200;
            //Calling update Logic
            iBusiness.UpdateItem(item);
            //Get item after update
            var updatedItem = iBusiness.GetItemDetailsByItemID(item.itemId);
            //Assertion of item and updated item
            Assert.NotNull(updatedItem);
            Assert.That(updatedItem.itemDescription, Is.EqualTo(item.itemDescription));
            Assert.That(updatedItem.itemName, Is.EqualTo(item.itemName));
            Assert.That(updatedItem.itemPrice, Is.EqualTo(item.itemPrice));
        }

        //deletind an Item Test
        [Test()]
        public void DeleteItem()
        {
            //Calling the businessLogic to insert
            item.itemId = iBusiness.InsertLogic(item);

            //Calling delete Logic
            iBusiness.DeleteItemByItemID(item.itemId);
            //Get item after update
            var deletedItem = iBusiness.GetItemDetailsByItemID(item.itemId);
            //Assertion of item and updated item
            Assert.Null(deletedItem);
        }
    }
}