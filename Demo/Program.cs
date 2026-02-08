// Rectangle rectangle = new Rectangle(4.5, 7.5);
// rectangle.GetArea();
// rectangle.Display();





using System;
using System.Reflection;

Assembly executing = Assembly.GetExecutingAssembly();
Type[] types = executing.GetTypes();
foreach(var item in types)
{
    //display each type name
    Console.WriteLine("Type: {0}", item.Name);

    //array to store all method information
    MethodInfo[] methodInfos = item.GetMethods();
    foreach(var method in methodInfos)
    {
        //display each method name
        Console.WriteLine("\tMethod: {0}", method.Name);

        //get custom attributes of each method
        ParameterInfo[] parameters = method.GetParameters();
        foreach(var param in parameters)
        {
            Console.WriteLine("\t\tParameter: {0} Type {1}", param.Name, param.ParameterType);
        }
    }
}

Console.WriteLine("--------------------------------------------------");

//initialize t as typeof string
Type t = typeof(string);

//Use reflection to find about any sort of data related to t

Console.WriteLine("Name: {0}", t.Name);
Console.WriteLine("Full Name: {0}", t.FullName);
Console.WriteLine("Namespace: {0}", t.Namespace);
Console.WriteLine("Base Type: {0}", t.BaseType);