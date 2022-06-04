using System;

namespace SingletonPattern.One
{
    public class FileSystem
    {
        //A static variable which holds a reference to the single created instance
        private static FileSystem instance;

        //A public static means of getting the reference to the single created instance, creating one if necessary
        //Lazy Initialization
        public static FileSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FileSystem();
                }
                return instance;
            }
        }

        //A single constructor, which is private and parameterless (singletons are not allowed to have parameters)
        //This prevents other classes from instantiating it and it also prevents subclassing (which both are violating the pattern)
        //But some argue that you should be able to inherit from singletons...
        private FileSystem()
        {
        }

        //For testing
        public void TestSingleton()
        {
            Console.WriteLine("Hello this is Singleton");
        }

    }

}


