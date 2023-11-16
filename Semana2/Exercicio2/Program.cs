// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
int fib1, fib2, fib;

fib1 = 0;
fib2 = 1;
fib = fib1 + fib2;
Console.WriteLine(fib1);
Console.WriteLine(fib2);
Console.WriteLine(fib);

while(fib < 100){
   
    fib1= fib2;
    fib2= fib;
    fib = fib1 + fib2;
    
    Console.WriteLine(fib);
}

