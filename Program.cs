using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;


namespace Reflection

{
    static class Reflector
    {
        static public void AllClassContent(object obj)
        {
            string writePath = @"D:\SomeDir\12lab.txt";
            StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default);


            Type m = obj.GetType();
            MemberInfo[] members = m.GetMembers();
            foreach (MemberInfo item in members)
            {
                sw.WriteLine($"{item.DeclaringType} {item.MemberType} {item.Name}");
            }
            sw.Close();
        }
        static public void PublicMethods(object obj)
        {
            Type m = obj.GetType();
            MethodInfo[] pubMethods = m.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            Console.WriteLine("Только публичные методы:");
            foreach (MethodInfo item in pubMethods)
            {
                Console.WriteLine(item.ReturnType.Name + " " + item.Name);
            }

        }
        static public void FieldsAndProperties(object obj)
        {
            Type m = obj.GetType();
            Console.WriteLine("Поля:");
            FieldInfo[] fields = m.GetFields();

            foreach (FieldInfo item in fields)
            {
                Console.WriteLine(item.FieldType + " " + item.Name);

            }
            Console.WriteLine("Свойства:");
            PropertyInfo[] properties = m.GetProperties();
            foreach (PropertyInfo item in properties)
            {
                Console.WriteLine($"{item.PropertyType} {item.Name}");
            }

        }
        static public void Interfaces(object obj)
        {
            Type m = obj.GetType();
            Console.WriteLine("Реализованные интерфейсы:");
            foreach (Type item in m.GetInterfaces())
            {
                Console.WriteLine(item.Name);
            }

        }
        static public void MethodsWithParams(object obj)
        {
            Console.WriteLine("Введите название типа для параметров:");
            string findType = Console.ReadLine();

            Type m = obj.GetType();
            MethodInfo[] methods = m.GetMethods();
            foreach (MethodInfo item in methods)
            {
                ParameterInfo[] p = item.GetParameters();

                foreach (ParameterInfo param in p)
                {
                    if (param.ParameterType.Name == findType)
                    {
                        Console.WriteLine("Метод:" + item.ReturnType.Name + " " + item.Name);
                    }
                }
            }
        }

        public static void LastTask(string Class, string MethodName)
        {
            StreamReader reader = new StreamReader(@"D:\SomeDir\12lab.txt", Encoding.Default);
            string param1, param2, param3;
            param1 = reader.ReadLine();
            param2 = reader.ReadLine();
            param3 = reader.ReadLine();
            reader.Close();

            Type m = Type.GetType(Class, false);

            object st = Activator.CreateInstance(m, null);
            MethodInfo method = m.GetMethod(MethodName);
           // method.Invoke(st, new object[] { param1, char.Parse(param2), int.Parse(param3) });
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Technics Iron = new Technics(15);
            Scanner Scaner = new Scanner(2);
            Computer Asus = new Computer(4);
            PrintingDevice Printer = new PrintingDevice(21);

            Reflector.AllClassContent(Iron);
            Reflector.FieldsAndProperties(Scaner);
            Reflector.Interfaces(Scaner);
            Reflector.PublicMethods(Asus);
            Reflector.MethodsWithParams(Asus);

            Reflector.LastTask("Reflection.TestParams", "showParams");
            Console.ReadKey();
        }
    }
}

namespace Reflection
{
    interface IProduct
    {
        void Use();
    }

    interface IDevice
    {
        void Work();
    }

    public abstract class Product : IProduct, IComparable<Product>, IComparer<Product>
    {
        public int oplimit;
        public int sumlimit;
        public int listlimit;

        public int CompareTo(Product obj)
        {
            return this.Oplimit.CompareTo(obj.Oplimit);
        }
        public int Compare(Product obj1, Product obj2)
        {
            if (obj1.Sumlimit > obj2.Sumlimit)
                return 1;
            else if (obj1.Sumlimit < obj2.Sumlimit)
                return -1;
            else
                return 0;
        }

        public int Oplimit
        {
            get { return oplimit; }
            set { oplimit = value; }
        }

        public int Sumlimit
        {
            get { return sumlimit; }
            set { sumlimit = value; }
        }

        public int Listlimit
        {
            get { return listlimit; }
            set { listlimit = value; }
        }

        public override string ToString()
        {
            Console.WriteLine(Oplimit + " " + Sumlimit + " " + Listlimit);
            return " type " + base.ToString();
        }

        virtual public bool ChangeWeapon(string newWeapon)
        {
            return true;
        }
        public abstract void Use();
    }

    class Technics : Product, IProduct
    {
        static int limit = 2000;
        public Technics(int sum)
        {
            sumlimit = sum;
        }
        public override void Use()
        {
            if (sumlimit > limit)
            {
                Console.WriteLine("Рассрочка на товар будет оформлена через 20 минут");
            }
            else
            {
                Console.WriteLine("Товары на данную сумму не продаются в рассрочку");
            }
        }
        public override string ToString()
        {
            return $"Рассрочка на товары с {limit}";
        }

    }

    class Computer : Product, IProduct
    {
        static int limit = 8;
        public Computer(int op)
        {
            oplimit = op;
        }
        public override void Use()
        {
            if (oplimit > limit)
            {
                Console.WriteLine("Это отличный компьютер");
            }
            else
            {
                Console.WriteLine("Лучше выбрать компьютер с большим количеством оперативной памяти");
            }
        }
        public override string ToString()
        {
            return $"Компьютеры для данных целей должны содержать как минимум {limit}гб оперативки";
        }
    }

    class PrintingDevice : Product, IProduct
    {
        static int limit = 50;
        public PrintingDevice(int list)
        {
            listlimit = list;
        }
        public override string ToString()
        {
            return $"Ограничение печатуемых листов: {limit}";
        }
        public override void Use()
        {
            if (listlimit > limit)
            {
                Console.WriteLine("Превышенное количество листов");
            }
            else
            {
                Console.WriteLine("Печатает...");
            }
        }
    }

    class Scanner : Product, IProduct
    {
        static int limit = 3;
        public Scanner(int list)
        {
            listlimit = list;
        }
        public override string ToString()
        {
            return $"Ограничение размера листа для сканированея А{limit}";
        }
        public override void Use()
        {
            if (listlimit > limit)
            {
                Console.WriteLine("Сканирует...");
            }
            else
            {
                Console.WriteLine("Размер листа слишком большой");
            }
        }
    }


    public class Printer
    {
        public void IAmPrinting(object obj)
        {
            obj.GetType();
            Console.WriteLine(obj.ToString());
        }
    }

    class TestParams
    {
        public static void showParams(string str, char symbol, int number)
        {
            Console.WriteLine($"{str} {symbol} {number}");
        }
    }
}