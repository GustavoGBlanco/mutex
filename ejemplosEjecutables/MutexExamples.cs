
using System;
using System.IO;
using System.Threading;

public static class MutexExamples
{
    private static Mutex _mutex = new();
    private static Mutex _mutexGlobal = new(false, "Global\\MiApp_Mutex");
    private static Mutex _archivoMutex = new(false, "Global\\ArchivoLog");
    private static Mutex _mutexUnico = new();
    private static Mutex _mutexModulo = new(false, "Global\\ModuloShared");
    private static Mutex _mutexBD = new();
    private static Mutex _mutexSecuencia = new(false, "Global\\SecuenciaProcesos");
    private static Mutex _mutexRecursoCritico = new();

    public static void AccesoUnico()
    {
        _mutex.WaitOne();
        try
        {
            Console.WriteLine("Ejecutando sección crítica con Mutex.");
            Thread.Sleep(300);
        }
        finally { _mutex.ReleaseMutex(); }
    }

    public static void AccesoEntreProcesos()
    {
        _mutexGlobal.WaitOne();
        try
        {
            Console.WriteLine("Sección crítica protegida entre procesos.");
            Thread.Sleep(500);
        }
        finally { _mutexGlobal.ReleaseMutex(); }
    }

    public static void EscribirLog(string mensaje)
    {
        _archivoMutex.WaitOne();
        try
        {
            File.AppendAllText("log_global.txt", mensaje + Environment.NewLine);
        }
        finally { _archivoMutex.ReleaseMutex(); }
    }

    public static void SeccionProtegida(string nombre)
    {
        _mutex.WaitOne();
        try
        {
            Console.WriteLine($"{nombre} accedió a la sección crítica.");
            Thread.Sleep(300);
        }
        finally { _mutex.ReleaseMutex(); }
    }

    public static void IntentarAcceso(string nombre)
    {
        if (_mutex.WaitOne(500))
        {
            try
            {
                Console.WriteLine($"{nombre} accedió a tiempo.");
                Thread.Sleep(300);
            }
            finally { _mutex.ReleaseMutex(); }
        }
        else
        {
            Console.WriteLine($"{nombre} no pudo obtener el Mutex (timeout).");
        }
    }

    public static bool VerificarInstanciaUnica()
    {
        bool creadaNueva;
        _mutexUnico = new Mutex(true, "Global\\MiAplicacionUnica", out creadaNueva);
        return creadaNueva;
    }

    public static void AccederRecursoCompartido()
    {
        _mutexModulo.WaitOne();
        try
        {
            Console.WriteLine("Módulo accediendo a recurso compartido.");
            Thread.Sleep(300);
        }
        finally { _mutexModulo.ReleaseMutex(); }
    }

    public static void EscribirBaseDatos(string nombre)
    {
        _mutexBD.WaitOne();
        try
        {
            Console.WriteLine($"{nombre} escribiendo en BD...");
            Thread.Sleep(300);
        }
        finally { _mutexBD.ReleaseMutex(); }
    }

    public static void AccesoSecuencial()
    {
        _mutexSecuencia.WaitOne();
        try
        {
            Console.WriteLine("Proceso ejecutando su parte secuencial.");
            Thread.Sleep(400);
        }
        finally { _mutexSecuencia.ReleaseMutex(); }
    }

    public static void AccesoCritico()
    {
        _mutexRecursoCritico.WaitOne();
        try
        {
            Console.WriteLine("Accediendo a red y disco de forma segura.");
            Thread.Sleep(500);
        }
        finally { _mutexRecursoCritico.ReleaseMutex(); }
    }
}
