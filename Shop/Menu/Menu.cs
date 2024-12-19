using System;
using System.Net.Http.Headers;
using System.Threading.Channels;
using LearningCode.Products;

namespace LearningCode
{
    public class Menu
    {
        private readonly Warehouse _warehouse;
        private readonly Cart.Cart _cart;
        private readonly RefactoringMainMenu _refactoringMainMenu;
        private readonly RefactoringCatalogMenu _refactoringCatalogMenu;
        private readonly RefactoringCartMenu _refactoringCartMenu;
        public Menu(Warehouse warehouse, Cart.Cart cart, RefactoringMainMenu refactoringMainMenu, RefactoringCatalogMenu refactoringCatalogMenu, RefactoringCartMenu refactoringCartMenu)
        {
            _warehouse = warehouse;
            _cart = cart;
            _refactoringMainMenu = refactoringMainMenu;
            _refactoringCatalogMenu = refactoringCatalogMenu;
            _refactoringCartMenu = refactoringCartMenu;
        }
        public void CaseMenu()
        {
            bool menu = true;
            while (menu)
            {
                var menuNumber = _refactoringMainMenu.MainMenu();
                Console.Clear();
                switch (menuNumber)
                {
                    case 1:
                        _warehouse.ShowProducts();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 2:
                        var catalogNumber = _refactoringCatalogMenu.CatalogMenu();
                        switch (catalogNumber)
                        {
                            case 1:
                                _refactoringCatalogMenu.AddProdutToCatalogMenu();
                                break;
                            case 2 :
                                _refactoringCatalogMenu.RemoveProductFromCatalogMenu();
                                break;
                        }
                        break;
                    case 3:
                        var cartNumber = _refactoringCartMenu.CartMenu();
                        switch (cartNumber)
                        {
                            case 1:
                                _refactoringCartMenu.AddProductToCartMenu();
                                break;
                            case 2:
                                _refactoringCartMenu.RemoveProductFromCartMenu();
                                break;
                            case 3:
                                _cart.ViewCart();
                                Console.ReadLine();
                                Console.Clear();
                                break;
                        }
                        break;
                    case 4:
                        _cart.Checkout();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 5:
                        _cart.ViewOrderHistory();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 6:
                        menu = false;
                        break;
                }
            }
        }
    }
}