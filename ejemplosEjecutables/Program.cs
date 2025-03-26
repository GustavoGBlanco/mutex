
using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("🧪 Ejecutando ejemplos de Mutex en C#...");

        Console.WriteLine("----------Ejemplo 1----------");
        new Thread(MutexExamples.AccesoUnico).Start();
        Thread.Sleep(500);

        Console.WriteLine("----------Ejemplo 2----------");
        new Thread(MutexExamples.AccesoEntreProcesos).Start();
        Thread.Sleep(700);

        Console.WriteLine("----------Ejemplo 3----------");
        new Thread(() => MutexExamples.EscribirLog("Log desde el ejemplo 3")).Start();
        Thread.Sleep(500);

        Console.WriteLine("----------Ejemplo 4----------");
        new Thread(() => MutexExamples.SeccionProtegida("Hilo 1")).Start();
        Thread.Sleep(500);

        Console.WriteLine("----------Ejemplo 5----------");
        new Thread(() => MutexExamples.IntentarAcceso("Hilo 2")).Start();
        Thread.Sleep(600);

        Console.WriteLine("----------Ejemplo 6----------");
        bool unica = MutexExamples.VerificarInstanciaUnica();
        Console.WriteLine(unica ? "Instancia única: OK" : "Ya hay una instancia corriendo");
        Thread.Sleep(500);

        Console.WriteLine("----------Ejemplo 7----------");
        new Thread(MutexExamples.AccederRecursoCompartido).Start();
        Thread.Sleep(500);

        Console.WriteLine("----------Ejemplo 8----------");
        new Thread(() => MutexExamples.EscribirBaseDatos("Proceso A")).Start();
        Thread.Sleep(500);

        Console.WriteLine("----------Ejemplo 9----------");
        new Thread(MutexExamples.AccesoSecuencial).Start();
        Thread.Sleep(600);

        Console.WriteLine("----------Ejemplo 10----------");
        new Thread(MutexExamples.AccesoCritico).Start();
        Thread.Sleep(600);

        Console.WriteLine("✅ Fin de los ejemplos.");
    }
}
