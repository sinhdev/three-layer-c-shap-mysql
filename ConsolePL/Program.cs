using System;
using System.Collections.Generic;
using Persistence;
using BL;

namespace ConsolePL
{
    class Program
    {
        static void Main(string[] args)
        {
            short mainChoose = 0, imChoose;
            string[] mainMenu = { "Item Management", "Add Customer (using stored procedure)", "Create Order (using Transaction)", "Exit" };
            string[] imMenu = { "Get By Item Id", "Get All Items", "Search By Item Name", "Exit" };
            ItemBL ibl = new ItemBL();
            CustomerBL cbl = new CustomerBL();
            OrderBL obl = new OrderBL();
            List<Item> lst;
            do
            {
                mainChoose = Menu(" Order Management System - OMS", mainMenu);
                switch (mainChoose)
                {
                    case 1:
                        do
                        {
                            // if (ibl == null) ibl = new ItemBL();
                            imChoose = Menu("Item Management", imMenu);
                            switch (imChoose)
                            {
                                case 1:
                                    Console.Write("\nInput Item Id: ");
                                    int itemId;
                                    if (Int32.TryParse(Console.ReadLine(), out itemId))
                                    {
                                        Item i = ibl.GetItemById(itemId);
                                        if (i != null)
                                        {
                                            //Console.WriteLine("Item ID: " + i.ItemId);
                                            Console.WriteLine("Item Name: " + i.ItemName);
                                            Console.WriteLine("Item Price: " + i.ItemPrice);
                                            Console.WriteLine("Amount: " + i.Amount);
                                            Console.WriteLine("Item Status: " + i.Status);
                                            Console.WriteLine("Item Description: " + i.Description);
                                        }
                                        else
                                        {
                                            Console.WriteLine("There is no item with id " + itemId);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Your Choose is wrong!");
                                    }
                                    Console.WriteLine("\n    Press Enter key to back menu...");
                                    Console.ReadLine();
                                    break;
                                case 2:
                                    lst = ibl.GetAll();
                                    Console.WriteLine("\nItem Count: " + lst.Count);
                                    break;
                                case 3:
                                    lst = ibl.GetByName("I");
                                    Console.WriteLine("\nItem Count By Name: " + lst.Count);
                                    break;
                            }
                        } while (imChoose != imMenu.Length);
                        break;
                    case 2:
                        Customer c = new Customer {CustomerName="Nguyen Thi Nhi", CustomerAddress="Ha Tay"};
                        Console.WriteLine("Customer ID: " + cbl.AddCustomer(c));
                        break;
                    case 3:
                        Order order = new Order();
                        //new customer
                        //order.OrderCustomer = new Customer { CustmerId = null, CustomerName = "Nguyen Xuan Sinh", CustomerAddress = "Hanoi" };
                        
                        //exists customer
                        order.OrderCustomer = new Customer { CustmerId = 1, CustomerName = "Nguyen Xuan Sinh", CustomerAddress = "Hanoi" };
                        
                        order.ItemsList.Add(ibl.GetItemById(2));
                        order.ItemsList[0].Amount = 1;
                        order.ItemsList.Add(ibl.GetItemById(3));
                        order.ItemsList[1].Amount = 2;
                        Console.WriteLine("Create Order: " + (obl.CreateOrder(order) ? "completed!" : "not complete!"));
                        break;
                }
            } while (mainChoose != mainMenu.Length);
        }

        private static short Menu(string title, string[] menuItems)
        {
            short choose = 0;
            string line = "========================================";
            Console.WriteLine(line);
            Console.WriteLine(" " + title);
            Console.WriteLine(line);
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine(" " + (i + 1) + ". " + menuItems[i]);
            }
            Console.WriteLine(line);
            do
            {
                Console.Write("Your choice: ");
                try
                {
                    choose = Int16.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Your Choose is wrong!");
                    continue;
                }
            } while (choose <= 0 || choose > menuItems.Length);
            return choose;
        }
    }
}