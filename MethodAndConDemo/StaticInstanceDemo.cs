using System;
class StaticInstanceDemo{
    //instance variable
    public int var=0;

    public static int var1=5; // static variable

    public void Count(){//instance method
        var++;
    }

    public static int Display(){//static method
        return var1;
    }
}