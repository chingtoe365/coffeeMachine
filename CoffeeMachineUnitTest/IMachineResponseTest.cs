/*
 * Interface for test class
 * 
 * */

namespace CoffeeMachineUnitTest
{
    interface IMachineResponseTest
    {
        void InsertOrder_CorrectFormat_ReturnCreatedStatusCode();
        void InsertOrder_WrongDrinkFormat_ReturnBadRequest();
        void InsertOrder_WrongSugarAmountFormat_ReturnBadRequest();
        void InsertOrder_WrongOwnMugFormat_ReturnBadRequest();
        void GetLastOrder_CompareMockInsert_ReturnTrue();
    }
}
