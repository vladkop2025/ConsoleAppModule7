using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            // Создаем 1 клиента
            var customer = new Customer { Name = "Daniel", ContactInfo = "my@mail.ru" };

            // Пример 1 использования класса HomeDelivery
            var homeDelivery = new HomeDelivery { Address = "123456 Moscow", CourierName = "First Client" };
            var order = new Order<HomeDelivery>(homeDelivery)
            {
                Number = 1,
            };

            // Создаем 1 товар
            var product = new Product { Id = 1, Name = "Computer", Price = 10000 };

            // Добавляем 1 товар в заказ
            order.Products.Add(product);

            //Заказ в коллекцию заказов
            OrderManager.AddOrder(order);

            // Пример 2 использования класса HomeDelivery
            homeDelivery = new HomeDelivery { Address = "123456 London", CourierName = "Second Client" };
            order = new Order<HomeDelivery>(homeDelivery)
            {
                Number = 2,
            };

            // Создаем 2 товар
            product = new Product { Id = 1, Name = "Table", Price = 5000 };

            // Добавляем 2 товар в заказ
            order.Products.Add(product);

            //Заказ в коллекцию заказов
            OrderManager.AddOrder(order);

            //Вывод всех заказов в консоль
            OrderManager.DisplayAllOrders();

        }

        abstract class Delivery // Абстрактный класс Delivery
        {
            public string Address { get; set; } // Адрес доставки
            public abstract void DisplayDeliveryInfo(); // Абстрактный метод, который должен быть реализован в наследниках
        }

        class HomeDelivery : Delivery // Класс для доставки на дом
        {
            public string CourierName { get; set; } // Имя курьера
            public override void DisplayDeliveryInfo() // Переопределение метода
            {
                Console.WriteLine($"Home Delivery to {Address} by {CourierName}");
            }
        }

        class PickPointDelivery : Delivery // Класс для доставки в пункт выдачи
        {
            public string PickPointId { get; set; } // ID пункта выдачи
            public override void DisplayDeliveryInfo() // Переопределение метода
            {
                Console.WriteLine($"Pick Point Delivery to {Address}, Pick Point ID: {PickPointId}");
            }
        }

        class ShopDelivery : Delivery // Класс для доставки в магазин
        {
            public string ShopName { get; set; } // Название магазина
            public override void DisplayDeliveryInfo() // Переопределение метода
            {
                Console.WriteLine($"Shop Delivery to {ShopName} at {Address}");
            }
        }

        class Product // Класс для товара
        {
            public int Id { get; set; } // ID товара
            public string Name { get; set; } // Название товара

            // Логика в свойстве: цена не может быть отрицательной
            private decimal _price;
            public decimal Price
            {
                get { return _price; }
                set { _price = value >= 0 ? value : throw new ArgumentException("Price cannot be negative"); }
            }
        }

        class Order<TDelivery> where TDelivery : Delivery // Класс для заказа
        {
            public TDelivery Delivery { get; set; }
            public int Number { get; set; }
            public List<Product> Products { get; set; }

            public Order(TDelivery delivery)
            {
                Delivery = delivery;
                Products = new List<Product>();
            }

            // Метод для отображения адреса доставки
            public void DisplayAddress()
            {
                Delivery.DisplayDeliveryInfo();
            }

            // Обобщенный метод для поиска товара по ID
            public Product FindProductById(int id)
            {
                return Products.Find(p => p.Id == id);
            }
        }

        static class OrderManager // Статический класс для управления заказами
        {
            private static List<Order<Delivery>> _orders = new List<Order<Delivery>>();

            // Статический метод для добавления заказа
            public static void AddOrder<TDelivery>(Order<TDelivery> order) where TDelivery : Delivery
            {
                // Приведение типа Order<TDelivery> к Order<Delivery>
                var baseOrder = new Order<Delivery>(order.Delivery)
                {
                    Number = order.Number,
                    Products = order.Products
                };
                _orders.Add(baseOrder);
                Console.WriteLine($"Order {order.Number} added.");
            }

            // Статический метод для отображения всех заказов
            public static void DisplayAllOrders()
            {
                foreach (var order in _orders)
                {
                    order.DisplayAddress();
                }
            }
        }

        class Customer // Класс для клиента
        {
            // Логика в свойстве: имя не может быть пустым
            private string _name;
            public string Name
            {
                get { return _name; }
                set { _name = !string.IsNullOrEmpty(value) ? value : throw new ArgumentException("Name cannot be empty"); }
            }

            public string ContactInfo { get; set; }
        }
    }
}