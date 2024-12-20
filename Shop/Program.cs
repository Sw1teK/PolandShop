using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using LearningCode.Products;

namespace LearningCode
{
    public class Program
    {
        static void Main(string[] strs)
        {
            Warehouse warehouse = new Warehouse();
            Cart.Cart cart = new Cart.Cart(warehouse);
            RefactoringMainMenu refactoringMainMenu = new RefactoringMainMenu(warehouse, cart);
            RefactoringCatalogMenu refactoringCatalogMenu = new RefactoringCatalogMenu(warehouse);
            RefactoringCartMenu refactoringCartMenu = new RefactoringCartMenu(warehouse, cart);
            Menu menu = new Menu(warehouse, cart, refactoringMainMenu, refactoringCatalogMenu, refactoringCartMenu);
            menu.CaseMenu();
        }
    }
    








}