﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// Source: https://refactoring.guru/design-patterns/singleton

namespace Singleton.ThreadSafe
{
    // This Singleton implementation is called "double check lock". It is
    // safe in multithreaded environments and provides lazy initialization for
    // the Singleton object.
    class Singleton
    {
        private Singleton() { }

        private static Singleton _instance;

        // We now have a lock object that will be used to synchronize
        // threads during first access to the Singleton.
        private static readonly object _lock = new object();

        public static Singleton GetInstance(string value)
        {
            // This conditional is needed to prevent threads stumbling over
            // the lock once the instance is ready.
            if (_instance == null)
            {
                // Now, imagine that the program has just been launched.
                // Since there's no Singleton instance yet, multiple threads can
                // simultaneously pass the previous conditional and reach this
                // point almost at the same time. The first of them will acquire
                // lock and will proceed further, while the rest will wait here.
                lock (_lock)
                {
                    // The lock statement acquires the mutual-exclusion lock for 
                    // a given object, executes a statement block, and then releases 
                    // the lock. While a lock is held, the thread that holds the 
                    // lock can again acquire and release the lock. Any other thread 
                    // is blocked from acquiring the lock and waits until the lock 
                    // is released.

                    // The first thread to acquire the lock, reaches this
                    // conditional, goes inside and creates the Singleton
                    // instance. Once it leaves the lock block, a thread that
                    // might have been waiting for the lock release may then
                    // enter this section. But since the Singleton field is
                    // already initialized, the thread won't create a new
                    // object.
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                        _instance.Value = value;
                    }
                }
            }
            return _instance;
        }

        // We'll use this property to prove that our Singleton really works.
        public string Value { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The client code.
            Console.WriteLine(
                "{0}\n{1}\n\n{2}\n",
                "If you see the same value, then singleton was reused (yay!)",
                "If you see different values, then 2 singletons were created (booo!!)",
                "RESULT:"
            );

            Thread process1 = new Thread(() =>
            {
                TestSingleton("FOO");
            });
            Thread process2 = new Thread(() =>
            {
                TestSingleton("BAR");
            });

            process1.Start();
            process2.Start();

            process1.Join();
            process2.Join();

            Console.Read();
        }

        public static void TestSingleton(string value)
        {
            Singleton singleton = Singleton.GetInstance(value);
            Console.WriteLine(singleton.Value);
        }
    }
}
