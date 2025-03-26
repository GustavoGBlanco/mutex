
using System;
using System.Threading;

class MutexExamplesApp
{
    static void Main()
    {
        Console.WriteLine("----------Ejemplo 1----------");
        Console.WriteLine(MutexSeccionCritica.Ejecutar());
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 2----------");
        for (int i = 0; i < 1000; i++) new Thread(MutexContadorSeguro.Incrementar).Start();
        Thread.Sleep(500);
        Console.WriteLine("Contador final: " + MutexContadorSeguro.Obtener());
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 3----------");
        for (int i = 1; i <= 5; i++)
        {
            string usuario = $"Usuario{i}";
            new Thread(() => Console.WriteLine(MutexAccesoRecurso.Acceder(usuario))).Start();
        }
        Thread.Sleep(500);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 4----------");
        MutexArchivo.Escribir("Línea escrita por Mutex");
        Console.WriteLine("Texto escrito en archivo 'registro.txt'");
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 5----------");
        MutexCola.Encolar("A");
        MutexCola.Encolar("B");
        Console.WriteLine("Desencolado: " + MutexCola.Desencolar());
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 6----------");
        new Thread(() => MutexLogger.Log("Desde hilo 1")).Start();
        new Thread(() => MutexLogger.Log("Desde hilo 2")).Start();
        Thread.Sleep(500);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 7----------");
        Console.WriteLine(MutexConTimeout.Intentar());
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 8----------");
        for (int i = 1; i <= 5; i++)
        {
            string usuario = $"Cliente{i}";
            new Thread(() => Console.WriteLine(MutexStock.Comprar(usuario))).Start();
        }
        Thread.Sleep(500);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 9----------");
        new Thread(() => Console.WriteLine(MutexProductorConsumidor.Consumir())).Start();
        Thread.Sleep(200);
        Console.WriteLine("Produciendo...");
        MutexProductorConsumidor.Producir(123);
        Thread.Sleep(500);
        Console.WriteLine();

        Console.WriteLine("----------Ejemplo 10----------");
        Console.WriteLine(MutexReinicio.Reiniciar());
        Console.WriteLine();
    }
}
