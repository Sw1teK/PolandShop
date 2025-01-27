using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Channels;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using LearningCode.Products;
using Dapper;

namespace LearningCode
{
    public class Program
    {
        static void Main(string[] strs)
        {
            string connectionString = "Server=DESKTOP-TF2SL2E;Database=PolandShop;Trusted_Connection=True; TrustServerCertificate=True;";
            var warehouse = new Warehouse(connectionString);
            Cart.Cart cart = new Cart.Cart(warehouse);
            RefactoringMainMenu refactoringMainMenu = new RefactoringMainMenu(warehouse, cart);
            RefactoringCatalogMenu refactoringCatalogMenu = new RefactoringCatalogMenu(warehouse);
            RefactoringCartMenu refactoringCartMenu = new RefactoringCartMenu(warehouse, cart);
            Menu menu = new Menu(warehouse, cart, refactoringMainMenu, refactoringCatalogMenu, refactoringCartMenu);
            menu.CaseMenu();
        }
    }
    








}