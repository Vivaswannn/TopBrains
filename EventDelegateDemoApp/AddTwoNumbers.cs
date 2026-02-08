using System;
class AddTwoNumbers
{

    public delegate void dg_OddNumber();  //declare delegate
    public event dg_OddNumber ev_OddNumber; //declare event
    public void Add()
    {
        int result;
        result = 5+4;
        Console.WriteLine(result.ToString());

        if (result % 2 != 0 && (ev_OddNumber!= null)) //check for odd number
        {
            ev_OddNumber(); //raise event
        }
    }
}