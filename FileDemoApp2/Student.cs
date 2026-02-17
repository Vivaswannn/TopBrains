using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; // Required for binary serialization

[Serializable]
class Student{
    int rollno;
    string name;
    public Student(int r, string n){
        this.rollno = r;
        this.name = n;
    }
}