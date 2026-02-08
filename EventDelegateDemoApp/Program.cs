using System;
class Program{
    public static void Main(string[] args)
    {
    //     AddTwoNumbers obj = new AddTwoNumbers();
    //     //subscribe to event
    //     obj.ev_OddNumber += new AddTwoNumbers.dg_OddNumber(EventMessage);
    //     // invoke the method to produce output
    //     obj.Add();
    // }
    // //delegate calls this method when event raised

    // static void EventMessage(){
    //     Console.WriteLine("event executed: odd number found");
    // }

    Predicate<string> CheckApple - IsApple;
    bool result= IsApple("I phone x");
    if(result){
        Console.WriteLine("It's an apple");
    }
    }
}