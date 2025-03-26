
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

public static class MutexSeccionCritica
{
    private static readonly Mutex _mutex = new();

    public static string Ejecutar()
    {
        _mutex.WaitOne();
        try { return "Acceso concedido a sección crítica"; }
        finally { _mutex.ReleaseMutex(); }
    }
}

public static class MutexContadorSeguro
{
    private static readonly Mutex _mutex = new();
    private static int _contador = 0;

    public static void Incrementar()
    {
        _mutex.WaitOne();
        try { _contador++; }
        finally { _mutex.ReleaseMutex(); }
    }

    public static int Obtener()
    {
        _mutex.WaitOne();
        try { return _contador; }
        finally { _mutex.ReleaseMutex(); }
    }
}

public static class MutexAccesoRecurso
{
    private static readonly Mutex _mutex = new();

    public static string Acceder(string usuario)
    {
        _mutex.WaitOne();
        try { return $"{usuario} accedió al recurso"; }
        finally { _mutex.ReleaseMutex(); }
    }
}

public static class MutexArchivo
{
    private static readonly Mutex _mutex = new();

    public static void Escribir(string mensaje)
    {
        _mutex.WaitOne();
        try
        {
            File.AppendAllText("registro.txt", mensaje + Environment.NewLine);
        }
        finally { _mutex.ReleaseMutex(); }
    }
}

public static class MutexCola
{
    private static readonly Mutex _mutex = new();
    private static Queue<string> _cola = new();

    public static void Encolar(string dato)
    {
        _mutex.WaitOne();
        try { _cola.Enqueue(dato); }
        finally { _mutex.ReleaseMutex(); }
    }

    public static string Desencolar()
    {
        _mutex.WaitOne();
        try
        {
            return _cola.Count > 0 ? _cola.Dequeue() : "Cola vacía";
        }
        finally { _mutex.ReleaseMutex(); }
    }
}

public static class MutexLogger
{
    private static readonly Mutex _mutex = new();

    public static void Log(string mensaje)
    {
        _mutex.WaitOne();
        try { Console.WriteLine($"[Log] {DateTime.Now}: {mensaje}"); }
        finally { _mutex.ReleaseMutex(); }
    }
}

public static class MutexConTimeout
{
    private static readonly Mutex _mutex = new();

    public static string Intentar()
    {
        if (_mutex.WaitOne(500))
        {
            try { return "Entró con éxito"; }
            finally { _mutex.ReleaseMutex(); }
        }
        return "Timeout al esperar Mutex";
    }
}

public static class MutexStock
{
    private static readonly Mutex _mutex = new();
    private static int _stock = 3;

    public static string Comprar(string usuario)
    {
        _mutex.WaitOne();
        try
        {
            if (_stock > 0)
            {
                _stock--;
                return $"{usuario} compró. Stock: {_stock}";
            }
            return $"{usuario} no pudo comprar. Sin stock.";
        }
        finally { _mutex.ReleaseMutex(); }
    }
}

public static class MutexProductorConsumidor
{
    private static readonly Mutex _mutex = new();
    private static Queue<int> _cola = new();

    public static void Producir(int valor)
    {
        _mutex.WaitOne();
        try { _cola.Enqueue(valor); }
        finally { _mutex.ReleaseMutex(); }
    }

    public static string Consumir()
    {
        _mutex.WaitOne();
        try
        {
            return _cola.Count > 0 ? $"Consumido: {_cola.Dequeue()}" : "Cola vacía";
        }
        finally { _mutex.ReleaseMutex(); }
    }
}

public static class MutexReinicio
{
    private static readonly Mutex _mutex = new();
    private static int _valor = 100;

    public static string Reiniciar()
    {
        _mutex.WaitOne();
        try
        {
            _valor = 0;
            return $"Recurso reiniciado. Valor actual: {_valor}";
        }
        finally { _mutex.ReleaseMutex(); }
    }
}
