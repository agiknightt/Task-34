using System;
using System.Collections.Generic;

namespace Task_34
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] product = { new Product("Морковь", 10), new Product("Лук", 12), new Product("Яйца", 15), new Product("Бургер", 30), new Product("Вафли", 9) };

            Console.WriteLine("Сколько человек в очереди ?");
            Shop shop = new Shop(Convert.ToInt32(Console.ReadLine()));

            Console.Clear();

            shop.ServiceClients(product);
        }
    }
    class Client
    {
        private int _money;
        private int _price;
        private List<Product> _products = new List<Product>();
        public Client(int money)
        {
            _money = money;          
        }
        public void BuyProducts()
        {
            ComputeAllSumProducts();

            if (_money > _price)
            {
                Console.WriteLine("Клиент покупает все продукты.");
            }

            while (_products.Count > 0)
            {
                if (_money >= _products[0].Price)
                {                    
                    _money -= _products[0].Price;
                    _products.RemoveAt(0);
                }
                else
                {
                    TakeAwayProduct();                    
                }
            }            
        }
        public void AddProducts(Product[] products)
        {
            Random rand = new Random();

            for (int i = 0; i < rand.Next(1, 5); i++)
            {
                int numberProduct = rand.Next(0, products.Length);
                _products.Add(new Product(products[numberProduct].Name, products[numberProduct].Price));
            }
        }

        public void ShowProducts()
        {
            Console.Write("В корзине у клиента : ");

            for (int i = 0; i < _products.Count; i++)
            {
                Console.Write($" {_products[i].Name}. ");
            }            
        }
        private void TakeAwayProduct()
        {
            Random rand = new Random();

            Console.WriteLine("Клиенту не хватает денег для покупки.");
            int numberProduct = rand.Next(0, _products.Count);
            Console.WriteLine($"Клиент убирает из корзины {_products[numberProduct].Name}.");
            _products.RemoveAt(numberProduct);
        }
        private void ComputeAllSumProducts()
        {
            for (int i = 0; i < _products.Count; i++)
            {
                _price += _products[i].Price;
            }
            Console.WriteLine($"\nИтоговая сумма покупки {_price}, у клента {_money} денег.\n");
        }        
    }
    class Product 
    {
        private string _name;
        private int _price;

        public int Price 
        { 
            get 
            {
                return _price;
            } 
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }
        public Product(string name, int price)
        {
            _name = name;
            _price = price;
        }        
    }
    class Shop
    {
        private Queue<Client> _clients = new Queue<Client>();
        public Shop(int countClient)
        {
            Random rand = new Random();

            for (int i = 0; i < countClient; i++)
            {                
                _clients.Enqueue(new Client(rand.Next(10, 30)));                
            }
        }
        public void ServiceClients(Product[] products)
        {
            while (_clients.Count > 0)
            {
                _clients.Peek().AddProducts(products);
                _clients.Peek().ShowProducts();
                _clients.Dequeue().BuyProducts();
                Console.WriteLine("Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
            Console.WriteLine("Вы обслужили всех клиентов.\nДля выхода нажмите любую клавишу");
            Console.ReadKey();
        }
    }
}
