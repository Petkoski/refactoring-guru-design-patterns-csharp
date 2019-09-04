using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Source: https://refactoring.guru/design-patterns/singleton
// Intent: Singleton is a creational design pattern that lets you ensure 
// that a class has only one instance, while providing a global 
// access point to this instance.

// All implementations of the Singleton have these two steps in common:
// - Make the default constructor private, to prevent other objects from 
// using the new operator with the Singleton class.
// - Create a static creation method that acts as a constructor. Under the 
// hood, this method calls the private constructor to create an object and 
// saves it in a static field.All following calls to this method return
// the cached object.
// If your code has access to the Singleton class, then it's able to call 
// the Singleton's static method.So whenever that method is called, the 
// same object is always returned.

namespace Singleton.NonThreadSafe
{
    // The Singleton class defines the `GetInstance` method that serves as
    // an alternative to constructor and lets clients access the same instance
    // of this class over and over.
    class Singleton
    {
        // The Singleton's constructor SHOULD ALWAYS be private to prevent
        // direct construction calls with the `new` operator.
        private Singleton() {
        }

        // The Singleton's instance is stored in a static field. There there
        // are multiple ways to initialize this field, all of them have various
        // pros and cons. In this example we'll show the simplest of these ways,
        // which, however, doesn't work really well in multithreaded program.
        private static Singleton _instance;

        // This is the static method that controls the access to the
        // singleton instance. On the first run, it creates a singleton object
        // and places it into the static field. On subsequent runs, it returns
        // the client existing object stored in the static field.
        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }

        // Finally, any singleton should define some business logic, which
        // can be executed on its instance.
        public static void SomeBusinessLogic()
        {
            // ...
            Console.WriteLine("Jovan's business logic goes here");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code.
            Singleton s1 = Singleton.GetInstance();
            Singleton s2 = Singleton.GetInstance();

            if (s1 == s2)
            {
                Console.WriteLine("Singleton works, both variables contain the same instance.");
            }
            else
            {
                Console.WriteLine("Singleton failed, variables contain different instances.");
            }

            Singleton.SomeBusinessLogic();

            Console.Read();
        }
    }
}
