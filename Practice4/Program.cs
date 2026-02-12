using System;
using System.Collections;
using System.Text;

class Album{
    public string Title{get;set;}
    public string Artist{get;set;}
}

class Program{
    static void Main(string[] args){
        ArrayList albums= new ArrayList();
        
    while(true){
        Console.WriteLine("Enter album title (or 'quit' to finish):");
        string title=Console.ReadLine();
        if(title=="quit"){
            break;
        }

        Console.WriteLine("enter artist name:");
        string artist=Console.ReadLine();

        if(IsValidInput(title) && IsValidInput(artist)){
            Album album = new Album();
            album.Title=title;
            album.Artist=artist;
            albums.Add(album);
        }
    }
    DisplayAlbums(albums);
    }

    private static bool IsValidInput(string input){
        return !string.IsNullOrWhiteSpace(input);
    }
    private static void DisplayAlbums(ArrayList albums){
        Console.WriteLine("Albums entered:");
        foreach(Album album in albums){
            Console.WriteLine("Title: "+album.Title+", Artist: "+album.Artist);
        }
    }
}