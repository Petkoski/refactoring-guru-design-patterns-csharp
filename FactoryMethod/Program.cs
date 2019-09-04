using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Source: https://refactoring.guru/design-patterns/factory-method
// Intent: Factory Method is a creational design pattern that provides 
// an interface for creating objects in a superclass, but allows 
// subclasses to alter the type of objects that will be created.

namespace FactoryMethod
{
    // The Creator class declares the factory method that is supposed to
    // return an object of a Product class. The Creator's subclasses usually
    // provide the implementation of this method.
    abstract class Creator
    {
        // Note that the Creator may also provide some default
        // implementation of the factory method.
        public abstract IProduct FactoryMethod();

        // Also note that, despite its name, the Creator's primary
        // responsibility is not creating products. Usually, it contains some
        // core business logic that relies on Product objects, returned by the
        // factory method. Subclasses can indirectly change that business logic
        // by overriding the factory method and returning a different type of
        // product from it.
        public string SomeOperation()
        {
            // Call the factory method to create a Product object.
            var product = FactoryMethod();

            // Now, use the product.
            var result = "Creator: The same creator's code has just worked with " + product.Operation();

            return result;
        }
    }

    // Concrete Creators override the factory method in order to change the
    // resulting product's type.
    class ConcreteCreator1 : Creator
    {
        // Note that the signature of the method still uses the abstract
        // product type, even though the concrete product is actually returned
        // from the method. This way the Creator can stay independent of
        // concrete product classes.
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct1();
        }
    }

    class ConcreteCreator2 : Creator
    {
        public override IProduct FactoryMethod()
        {
            return new ConcreteProduct2();
        }
    }

    // The Product interface declares the operations that all concrete
    // products must implement.
    public interface IProduct
    {
        string Operation();
    }

    // Concrete Products provide various implementations of the Product
    // interface.
    class ConcreteProduct1 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct1}";
        }
    }

    class ConcreteProduct2 : IProduct
    {
        public string Operation()
        {
            return "{Result of ConcreteProduct2}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App: Launched with the ConcreteCreator1.");
            ClientCode(new ConcreteCreator1());

            Console.WriteLine();

            Console.WriteLine("App: Launched with the ConcreteCreator2.");
            ClientCode(new ConcreteCreator2());

            /**
             * Example 2 (simpler, from Dmitri Nesteruk - Design Patterns in .NET book):
             */
            Console.WriteLine();
            var point = Point.NewPolarPoint(5, Math.PI / 4);
            var point2 = Point.NewCartesianPoint(50, 45);
            Console.WriteLine(point.ToString());
            Console.WriteLine(point2.ToString());

            Console.WriteLine();
            PointWithPublicConstructor p3 = PointFactory.NewPolarPoint(5, Math.PI / 4);
            PointWithPublicConstructor p4 = PointFactory.NewCartesianPoint(50, 45);
            Console.WriteLine(p3.ToString());
            Console.WriteLine(p4.ToString());

            Console.WriteLine();
            Point2 p5 = Point2.Point2Factory.NewPolarPoint(5, Math.PI / 4);
            Point2 p6 = Point2.Point2Factory.NewCartesianPoint(50, 45);
            Console.WriteLine(p5.ToString());
            Console.WriteLine(p6.ToString());

            Console.Read();
        }

        private static void ClientCode(Creator creator)
        {
            // ...
            Console.WriteLine("Client: I'm not aware of the creator's class, " + "but it still works.\n" + creator.SomeOperation());
            // ...
        }
    }
}
