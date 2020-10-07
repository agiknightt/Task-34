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

            shop.ServiceClient(product);
        }
    }
    class Client
    {
        private int _money;
        private List<Product> _product = new List<Product>();
        public Client(int money)
        {
            _money = money;          
        }
        public void BuyProducts()
        {
            ComputeAllSumProducts();

            while(_product.Count > 0)
            {
                if (_money >= _product[0].Price)
                {                    
                    _money -= _product[0].Price;
                    _product.RemoveAt(0);
                }
                else
                {
                    TakeAwayProduct();                    
                }
            }            
        }
        public void AddAndShowProducts(Product[] products)
        {
            Random rand = new Random();

            Console.Write("В корзине у клиента : ");

            for (int i = 0; i < rand.Next(1, 5); i++)
            {
                int numberProduct = rand.Next(0, products.Length);                
                _product.Add(new Product(products[numberProduct].Name, products[numberProduct].Price));
                Console.Write($" {_product[i].Name}. ");
            }
        }
        public void TakeAwayProduct()
        {
            Random rand = new Random();

            Console.WriteLine("Клиенту не хватает денег для покупки.");
            int numberProduct = rand.Next(0, _product.Count);
            Console.WriteLine($"Клиент убирает из корзины {_product[numberProduct].Name}.");
            _product.RemoveAt(numberProduct);
        }
        public void ComputeAllSumProducts()
        {
            int prices = 0;
            for (int i = 0; i < _product.Count; i++)
            {
                prices += _product[i].Price;
            }            
            Console.WriteLine($"\nИтоговая сумма покупки {prices}, у клента {_money} денег.\n");

            if (_money > prices)
            {
                Console.WriteLine("Клиент покупает все продукты.");
            }
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
        public void ServiceClient(Product[] products)
        {
            while (_clients.Count > 0)
            {
                _clients.Peek().AddAndShowProducts(products);
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
