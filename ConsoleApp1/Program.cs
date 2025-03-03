using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Модуль 7.ООП.Продолжение

            *************************
                7.7. Итоги 
            *************************
            *
            *

            По результатам последних двух модулей вы познакомились с парадигмой ООП и научились применять её на практике. Вы изучили огромное количество 
            инструментов, механизмов и правил языка C#.
            Вы уже могли и подзабыть что-то из того, что было пройдено, поэтому данный материал будет для вас хорошей подсказкой.

            В работе над различного рода системами принципы и инструменты ООП могут не быть так хорошо заметны, но именно благодаря им система получается слаженной, 
            а связи между объектами работают как нужно.

            Для того чтобы вы попробовали использовать на практике большую часть пройденного материала, предлагаю выполнить итоговое задание.

            --------------------------------------
            Итоговое задание
            --------------------------------------

            Итоговое задание является креативным и трудным, оно призвано дать вам шанс построить свою маленькую систему классов, руководствуясь 
            принципами и инструментами, которые вы изучили.

            В данном модуле мы уже работали с одним хорошим примером — заказами в интернет магазине, и создали небольшую систему классов.

            Код:

            Вашей задачей будет развить и продолжить эту систему классов, чтобы она больше напоминала систему в реальном мире.

            Здесь нет жестких рамок и все зависит от того, насколько вы хорошо поняли принципы ООП, и насколько хорошо сможете спроецировать реальные объекты в классы программы.

            В качестве отправной точки, хотелось бы поделиться некоторыми советами по построению системы:

            В качестве отправной точки всей системы вы можете оставить заказ (Order) или же сделать какой-то надкласс (MyOrders, OrderCollection или какой-то другой).
            Систему стоит развивать вглубь, а не в ширину. То есть идти «внутрь» заказа и создавать связанные с ним сущности, а не уходить в «Настройки», «Личный кабинет» или что-то такое.
            Заказ может содержать класс Product для описания товара. Либо же он может содержать несколько товаров, например, в массиве.
            Вы можете создать какие-то общие используемые классы, которые облегчат работу, например, для адреса или мобильного телефона компании и прочего.
            Для развития систем доставки следует обратить внимание на описание доставок в модуле:
               
                □ HomeDelivery — доставка на дом. Этот тип будет подразумевать наличие курьера или передачу курьерской компании, в нем будет располагаться своя, отдельная 
                  от прочих типов доставки логика.

                □ PickPointDelivery — доставка в пункт выдачи. Здесь будет храниться какая-то ещё логика, необходимая для процесса доставки в пункт выдачи, например, хранение компании и точки выдачи, а также какой-то ещё информации.

                □ ShopDelivery — доставка в розничный магазин. Эта доставка может выполняться внутренними средствами компании и совсем не требует работы с «внешними&raquo; элементами.

            Можете не реализовывать методы досконально, если вы хотите сделать метод, который добавляет значение в поле, — пожалуйста, но будьте осторожны 
            с большими методами, чтобы не запутаться.
            
            Следите за модификаторами доступа.

            Используйте принципы ООП, если это возможно. Если класс можно сделать абстрактным — сделайте, если метод подразумевает переопределение — переопределяйте его и т.д.
            За выполнение критериев каждого уровня вы получаете 1 балл. Вы можете выполнить только базовый уровень и получить 1 балл, либо выполнить базовый + продвинутый 
            уровень за 2 балла. Если вы сможете выполнить все уровни, то получите 3 балла, а мы поймем, что вы поняли тему на отлично.

            */

        }
    

        abstract class Delivery                             //Абстрактный класс Delivery
        {
            //public string Address;
            public string Address { get; set; }             //конструктор абстрактного класса
            public abstract void DisplayDeliveryInfo();     // // Абстрактный метод, который должен быть реализован в наследниках
        }

        class HomeDelivery : Delivery                       // Класс для доставки на дом - Наследник класса Delivery
        {
            public string CourierName { get; set; }         //конструктор класса
            public override void DisplayDeliveryInfo()      //переопределение метода 
            {
                Console.WriteLine($"Home Delivery to {Address} by {CourierName}");  //вывод данных на консоль
            }
        }

        class PickPointDelivery : Delivery                   //// Класс для доставки в пункт выдачи - Наследник класса Delivery      
        {
            public string PickPointId { get; set; }          //конструктор класса
            public override void DisplayDeliveryInfo()       //переопределение метода  
            {
                Console.WriteLine($"Pick Point Delivery to {Address}, Pick Point ID: {PickPointId}");  //ввывод данных на консоль
            }
        }


        class ShopDelivery : Delivery                         //// Класс для доставки в магазин - Наследник класса Delivery 
        {
            public string ShopName { get; set; }             //конструктор класса
            public override void DisplayDeliveryInfo()       //переопределение метода 
            {
                Console.WriteLine($"Shop Delivery to {ShopName} at {Address}");  //вывод данных на консоль
            }

            class Product                                     // Класс для товара
            {
                public int Id { get; set; }                  // Id товара
                public string Name { get; set; }             // Наименование товара

                // Логика в свойстве: цена не может быть отрицательной
                private decimal _price;
                public decimal Price
                {
                    get { return _price; }
                    set { _price = value >= 0 ? value : throw new ArgumentException("Price cannot be negative"); }
                }
            }




            class Order<TDelivery> where TDelivery : Delivery   // Класс для заказа
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


            static class OrderManager                                                           // Статический класс для управления заказами
            {
                private static List<Order<Delivery>> _orders = new List<Order<Delivery>>();

                // Статический метод для добавления заказа
                public static void AddOrder(Order<Delivery> order)
                {
                    _orders.Add(order);
                    Console.WriteLine($"Order {order.Number} added.");
                }


                public static void DisplayAllOrders()                                           // Статический метод для отображения всех заказов
                {
                    foreach (var order in _orders)
                    {
                        order.DisplayAddress();
                    }
                }
            }


            class Customer                                                                         // Класс для клиента
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
    
}
