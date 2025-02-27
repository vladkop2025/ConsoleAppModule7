using System;
using System.Collections.Generic;
using System.Linq;
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
            * 7.3. Абстрактные классы
            *************************        

            Помимо обычных классов, в C# есть абстрактные классы.

            Абстрактный класс — это класс, экземпляр которого не может быть инициализирован. Абстрактный класс служит только базовым классом для других классов.

            Для того чтобы объявить абстрактный класс, в его объявлении следует добавить модификатор abstract:

                abstract class AbstractClass 
                {
                    public string Name;
                }
            По определению объекты абстрактного класса не могут быть инициализированы конструктором, то есть такая запись:

                AbstractClass obj = new AbstractClass();
                Приведет к ошибке на этапе компиляции:

            Для того чтобы понять, когда нужно использовать абстрактный класс, рассмотрим следующую схему классов для гостиницы:

abstract class Person 
{
  public string Name;

  public Person(string name) 
  {
    Name = name;
  }

  public void DisplayName() 
  {
    Console.WriteLine(Name);
  }
}

class Employee: Person 
{
  // Булевый флаг, сообщающий, находится ли сотрудник на смене
  public bool IsOnShift;

  public Employee(string name, bool isOnShift) : base(name) {
    IsOnShift = isOnShift;
  }
}

class Guest: Person 
{
  // Дата и время прибытия гостя
  public DateTime ArrivalDate;

  public Guest(string name, DateTime arrivalDate) : base(name) {
    ArrivalDate = arrivalDate;
  }
}

            В схеме присутствуют классы Employee — сотрудник гостиницы, и Guest — гость. И сотрудник, и гость — люди, поэтому они имеют схожий функционал, 
            который можно унаследовать от другого класса. В нашем случае это поле Name (имя) и метод для его отображения — DisplayName().

            Так как все объекты в нашей системе будут представлены либо сотрудниками, либо гостями, то нам нет нужды создавать объекты класса Person напрямую. 
            Поэтому имеет смысл сделать его абстрактным.

            Таким образом, мы можем создавать объекты следующим образом:

                 Employee employee = new Employee("Николай", true);
                 Guest guest = new Guest("Андрей", new DateTime(2020, 11, 05));

            Также мы можем создавать объекты данных типов и обращаться к ним как объектам типа Person:

                Person person = employee;
                person = guest;

            Конструктор в абстрактных классах играет важную роль, поскольку служит для инициализации общих для производных классов полей и свойств, 
            как это сделано выше в случае с полем Name. И хотя в примере выше конструктор класса Person не вызывается напрямую, производные классы 
            Employee и Guest обращаются к нему через ключевое слово base.

            Помимо классов, ключевое слово abstract может быть добавлено и к членам абстрактного класса. Таким как:

                методы;
                свойства;
                индексаторы;
                события.
                Абстрактный

            Абстрактный элемент класса — это такой элемент абстрактного класса, который не имеет реализации в базовом классе и должен быть реализован в классе-наследнике.

            Абстрактные элементы классов не должны иметь модификатор private.

            --------------------------------------
            Абстрактные методы
            --------------------------------------

            В C# абстрактные методы помечаются ключевым словом abstract. Они не имеют тела в фигурных скобках, вместо этого после параметров метода идет точка с запятой:

                abstract class AbstractClass 
                {
                     public string Name;

                     public abstract void Display();
                }

            Такие методы позволяют не определять реализацию для базового класса, но обязывают производные классы реализовывать метод с помощью override:

abstract class FourLeggedAnimal 
{
  public abstract void Describe();
}

class Dog: FourLeggedAnimal 
{
  public override void Describe() 
  {
    Console.WriteLine("Это животное - собака");
  }
}

class Cat: FourLeggedAnimal 
{
  public override void Describe() 
  {
    Console.WriteLine("Это животное - кошка");
  }
}

            В данном примере абстрактным классом является FourLeggedAnimal (животное с 4 лапами), очевидно, что мы не хотим создавать такое животное, и класс должен быть абстрактным.

            В классах Dog и Cat мы с помощью override определяем реализацию метода Describe().Если же этого не делать, это вызовет ошибку:

            В отличие от виртуальных методов, абстрактные методы требуют реализации в производных классах, таким образом, вы перекладываете контроль за логической 
            связанностью программы на компилятор.

            Например, если бы из примера выше метод Describe() был виртуальным, могло произойти следующее:

            abstract class FourLeggedAnimal 
{
  public virtual void Describe() 
  {
    Console.WriteLine("Это неизвестное животное");
  }
}

class Dog: FourLeggedAnimal 
{
  public override void Describe() 
  {
    Console.WriteLine("Это животное - собака");
  }
}

class Cat: FourLeggedAnimal 
{
}

            Мы забыли переопределить метод Describe() в классе Cat, таким образом нарушив работу программы и создав себе ошибку, которую не сразу заметим. 
            При вызове метода Describe() для экземпляров Dog и Cat мы получим следующий вывод:

Dog dog = new Dog();
Cat cat = new Cat();

dog.Describe(); // Собака
cat.Describe(); // Неизвестное животное
FourLeggedAnimal animal = dog;
animal.Describe();

animal = cat; // Собака
animal.Describe(); // Неизвестное животное

                    Это животное собака
                    Это неизвестное животное
                    Это животное собака
                    Это неизвестное животное
            
            Задание 7.3.1
            Что такое абстрактный класс?

                Это класс, от которого нельзя наследоваться
                Это класс, экземпляр которого не может быть инициализирован                                         X
                Это класс, в котором можно только объявлять методы и свойства, но нельзя писать их реализацию
                Это класс, который не имеет конструкторов

            Задание 7.3.3
            Создайте классы для следующих объектов компьютера: процессор (Processor), материнская карта (MotherBoard), 
            видеокарта (GraphicCard). Унаследуйте их от класса ComputerPart.

            Добавьте в класс ComputerPart абстрактный метод Work без параметров и с типом void.

            Ответ:

abstract class ComputerPart 
{
  public abstract void Work();
}

class Processor: ComputerPart 
{
  public override void Work() {}
}

class MotherBoard: ComputerPart 
{
  public override void Work() {}
}

class GraphicCard: ComputerPart 
{
  public override void Work() {}
}

            --------------------------------------
            Абстрактные свойства
            --------------------------------------

            Как было заявлено выше, свойства тоже могут быть абстрактными. Для этого они помечаются ключевым словом abstract, а методы get и set 
            не имеют тела (аналогично абстрактным методам). В таком виде объявление абстрактных свойств напоминает автосвойства:

abstract class AbstractClass 
{
  public abstract string Name 
  {
    get;
    set;
  }

  public abstract void Display();
}


            Для определения реализации свойства в производном класса также следует использовать override. Причем свойство можно определить как полное (в классе Dog), 
            так и как автоматическое (в классе Cat):

abstract class FourLeggedAnimal 
{
  public abstract string Name 
  {
    get;
    set;
  }
}

class Dog: FourLeggedAnimal 
{
  private string name;
  public override string Name
  {
    get 
    {
      return name;
    }
    set 
    {
      name = value;
    }
  }
}

class Cat: FourLeggedAnimal 
{
  public override string Name 
  {
    get;
    set;
  }
}
            
            --------------------------------------
            Отказ от реализации абстрактных членов
            --------------------------------------
            Производный класс обязан реализовать все абстрактные члены базового класса. Однако мы можем отказаться от реализации, но в этом случае производный класс 
            также должен быть определен как абстрактный: Например:

abstract class AbstractClass 
{
  public abstract string Name 
  {
    get;
    set;
  }
}

abstract class DerivedAbstractClass: AbstractClass 
{
  public abstract void Display();
}

            Таким образом, в классе DerivedAbstractClass мы можем не определять реализацию поля Name, но в производных от этого класса классах всё равно будем обязаны это сделать.

            Рассмотрим работу принципов абстракции на примере системы классов: Transport (транспортное средство), Car (автомобиль), HybridCar (гибридный автомобиль) и Boat (катер), 
            которые соответствуют следующей схеме:

                Transport -> Boat
                          -> Car -> HybridCar

abstract class Transport
{
	public abstract void Move();
}

class Boat : Transport
{
	public override void Move()
	{
		// ...
	}
}

class Car : Transport
{
	public double Fuel;

	public int Mileage;

	public Car()
	{
		Fuel = 50;
		Mileage = 0;
	}
	public override void Move()
	{
		Mileage++;
		Fuel -= 0.5;
	}

	public void FillTheCar()
	{
		Fuel = 50;
	}
}

enum FuelType
{
	Gas = 0,
	Electricity
}

class HybridCar : Car
{
	public FuelType FuelType;

	public void ChangeFuelType(FuelType type)
	{
		FuelType = type;
	}
}

            Задание 7.3.4
            Выберите вариант с верным объявлением абстрактного свойства:

                public abstract string Name { get { } set { } }
                public abstract string Name;
                public abstract string Name { get; set; }               X

            */
        }
    }
}
