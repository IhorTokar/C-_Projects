public class Programm{
    public static void Main(){
        Console.Write("Введіть N: ");
int n = int.Parse(Console.ReadLine());

Console.Write("Введіть M (кількість потоків): ");
int m = int.Parse(Console.ReadLine());

Task1.Run(n, m);
    }
}
