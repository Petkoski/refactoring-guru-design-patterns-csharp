using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Source: https://refactoring.guru/design-patterns/prototype
// Intent: Prototype is a creational design pattern that lets you copy 
// existing objects without making your code dependent on their classes.

// Usage examples: The Prototype pattern is available in C# out of the 
// box with a Cloneable interface.

// Identification: The prototype can be easily recognized by a clone()
// or copy() methods, etc.

namespace Prototype
{
    public class Person
    {
        public IdInfo Id;
        public string Name;
        public DateTime BirthDate;
        public int Age;

        // Shallow Copy: Creating a new object and then copying the value type 
        // fields of the current object to the new object. But when the data is 
        // reference type, then the only reference is copied but not the referred
        // object itself. Therefore the original and clone refer to the same object.

        // The method Object.MemberwiseClone() is a protected method of Object,
        // so it is automatically inherited by every single reference type.
        // It creates a shallow copy of the object.
        public Person ShallowCopy()
        {
            return (Person)MemberwiseClone();
        }

        // Deep Copy: Recursive copying of all of an object's members and the 
        // construction of shiny new counterpart objects, each initialized with 
        // identical data.
        public Person DeepCopy()
        {
            Person clone = (Person)MemberwiseClone();
            clone.Id = new IdInfo(Id.IdNumber);
            clone.Name = string.Copy(Name);
            return clone;
        }
    }

    public class IdInfo
    {
        public int IdNumber;

        public IdInfo(int idNumber)
        {
            IdNumber = idNumber;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person();
            p1.Age = 24;
            p1.BirthDate = Convert.ToDateTime("1995-01-20");
            p1.Name = "Jovan Petkoski";
            p1.Id = new IdInfo(23);

            // Perform a shallow copy of p1 and assign it to p2.
            Person p2 = p1.ShallowCopy();

            // Make a deep copy of p1 and assign it to p3.
            Person p3 = p1.DeepCopy();

            // Display values of p1, p2 and p3.
            Console.WriteLine("Original values of p1, p2, p3:");
            Console.WriteLine("   p1 instance values: ");
            DisplayValues(p1);
            Console.WriteLine("   p2 instance values:");
            DisplayValues(p2);
            Console.WriteLine("   p3 instance values:");
            DisplayValues(p3);

            // Change the value of p1 properties and display the values of p1, p2 and p3.
            p1.Age = 34;
            p1.BirthDate = Convert.ToDateTime("2005-02-22");
            p1.Name = "Frank";
            p1.Id.IdNumber = 33;
            Console.WriteLine("\nValues of p1, p2 and p3 after changes to p1:");
            Console.WriteLine("   p1 instance values: ");
            DisplayValues(p1);
            Console.WriteLine("   p2 instance values (reference values have changed):");
            DisplayValues(p2);
            Console.WriteLine("   p3 instance values (everything was kept the same):");
            DisplayValues(p3);

            // Shallow copy issue:
            Console.WriteLine();
            Console.WriteLine();

            var john = new Person {
                Id = new IdInfo(1),
                Name = "John Smith",
                Age = 18,
                BirthDate = new DateTime(2000, 05, 25)
            };

            var jane = john.ShallowCopy();
            jane.Name = "Jane Smith"; //John's name DID NOT change (good)
            jane.Age = 28; //Same here (good)
            jane.BirthDate = new DateTime(2005, 06, 26); //Same here (good)
            jane.Id.IdNumber = 2; //John's Id changed (bad)

            DisplayValues(john);
            DisplayValues(jane);

            Console.Read();
        }

        private static void DisplayValues(Person p)
        {
            Console.WriteLine("      Name: {0:s}, Age: {1:d}, BirthDate: {2:MM/dd/yy}", p.Name, p.Age, p.BirthDate);
            Console.WriteLine("      ID#: {0:d}", p.Id.IdNumber);
        }
    }
}