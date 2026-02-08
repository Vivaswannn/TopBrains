using System;
using System.IO;

public class SerializeExample{
    public static void Main(string[] args){
        FileStream stream = new FileStream("student.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        BinaryFormatter formatter = new BinaryFormatter(); // to serialize and deserialize the object for binary format


    }   
}