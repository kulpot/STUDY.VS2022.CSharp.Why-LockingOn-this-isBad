using System;

//ref link:https://www.youtube.com/watch?v=N_GS9eSfibk&list=PLRwVmtr-pp06KcX24ycbC-KkmAISAFKV5&index=23
//ctrl+l -- remove blank line
//ctrl+m+o -- collapse all
//ctrl+k+d -- align all
// object baton = new object();  // private lock -- recommended lock
// lock (stall1)   // public lock --- lock(this) -- causes disruption be wary

class BathroomStall
{
    //object baton = new object();  // private lock -- recommended lock
    public void BeUsed()
    {
        //lock(baton)
        lock (this)     // public lock --- lock(this) -- 
        {
            Console.WriteLine("Doing our thing...");
        }
    }
    public void FlushToilet()
    {
        //lock (baton)
        lock (this)
        {
            Console.WriteLine("Flushing the toilet...");
        }
    }
}

class PublicRestroom
{
    BathroomStall stall1 = new BathroomStall();
    BathroomStall stall2 = new BathroomStall();
    public void UseStall1()
    {
        lock (stall1)   // public lock --- lock(this) -- causes disruption be wary
        {
            stall1.BeUsed();
            // when our code is between DANGER ZONE
            stall1.FlushToilet();
        }
    }
    public void UseStall2()
    {
        stall2.BeUsed();
        stall2.FlushToilet();
    }
    public void CleanTheSink()
    {
        lock (stall1)
            Console.WriteLine("Clean the sink...");
    }
}

class MainClass
{
    static void Main()
    {
        var restroom = new PublicRestroom();
        new Thread(restroom.UseStall1).Start();
        new Thread(restroom.UseStall2).Start();
        new Thread(restroom.UseStall1).Start();
        new Thread(restroom.UseStall2).Start();
    }
}