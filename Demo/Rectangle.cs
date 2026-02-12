[DeBugInfo(45, "Zara Ali", "12/8/2012",
    Message = "Return type mismatch")]


class Rectangle
{
    protected double length;
    protected double width;
    public Rectangle(double l, double w)
    {
        this.length = l;
        this.width = w;
    }
    [DeBugInfo(55, "Zara Ali", "19/10/2012", Message = "Return type mismatch")]

    public double GetArea()
    {
        return length * width;
    }
    [DeBugInfo(56, "Zara Ali", "19/10/2012")]

    public void Display()
    {
        Console.WriteLine("Length: {0}, Width: {1}", length, width);
        Console.WriteLine("Area: {0}", GetArea());
        
    }
}