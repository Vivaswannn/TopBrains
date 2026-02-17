using System;
public class Program{
    public double? Averagenonull(double?[] values){
        double sum=0;
        int count=0;

        foreach(double? v in values){
            if(v.HasValue){
                sum+=v.Value;
                count++;
            }
        }
        if(count==0) return null;

        double avg=sum/count;
        return Math.Round(avg,2,MidpointRounding.AwayFromZero);
    }
    
    public static void Main(string[] args){
        Program p = new Program();
        double?[] values = { 1.5, 2.5, 3.5, null, 4.5 };
        double? result = p.Averagenonull(values);
        Console.WriteLine($"Average: {result}");
    }
}