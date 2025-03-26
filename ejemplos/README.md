# Ejemplos de `Mutex` en C# (detalle por ejemplo)

Este documento presenta 10 ejemplos progresivos y prácticos del uso de `Mutex` en C#, enfocados en la sincronización entre hilos dentro de un mismo proceso. Todos los ejemplos están diseñados para ser ejecutados en escenarios multihilo reales utilizando `Thread`.

---

## 🧪 Ejemplo 1: Uso básico de Mutex

```csharp
private static readonly Mutex _mutex = new();

public static string SeccionCritica()
{
    _mutex.WaitOne();
    try { return "Acceso concedido a sección crítica"; }
    finally { _mutex.ReleaseMutex(); }
}
```

🔍 Sincroniza el acceso a una sección crítica entre hilos.

✅ **¿Por qué `Mutex`?**  
A diferencia de `lock`, `Mutex` puede ser usado también entre procesos, aunque en este caso solo se usa en un hilo.

---

## 🧪 Ejemplo 2: Incremento de contador seguro

```csharp
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
```

🔍 Controla acceso concurrente a un entero compartido.

✅ **¿Por qué `Mutex`?**  
Ideal cuando se quiere explícitamente controlar adquisición y liberación.

---

## 🧪 Ejemplo 3: Control de acceso a recurso simulado

```csharp
private static readonly Mutex _mutex = new();

public static string Acceder(string usuario)
{
    _mutex.WaitOne();
    try { return $"{usuario} accedió al recurso"; }
    finally { _mutex.ReleaseMutex(); }
}
```

🔍 Muestra cómo varios hilos compiten por entrar a una misma sección.

---

## 🧪 Ejemplo 4: Lectura y escritura en archivo

```csharp
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
```

🔍 Protege acceso a disco evitando condiciones de carrera.

---

## 🧪 Ejemplo 5: Cola compartida

```csharp
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
```

🔍 Modelo FIFO con sincronización de acceso.

---

## 🧪 Ejemplo 6: Logs simultáneos desde múltiples hilos

```csharp
private static readonly Mutex _mutex = new();

public static void Log(string mensaje)
{
    _mutex.WaitOne();
    try
    {
        Console.WriteLine($"[Log] {DateTime.Now}: {mensaje}");
    }
    finally { _mutex.ReleaseMutex(); }
}
```

🔍 Evita entrelazado de logs cuando múltiples hilos imprimen simultáneamente.

---

## 🧪 Ejemplo 7: Espera con timeout

```csharp
private static readonly Mutex _mutex = new();

public static string IntentarEntrar()
{
    if (_mutex.WaitOne(500))
    {
        try { return "Entró con éxito"; }
        finally { _mutex.ReleaseMutex(); }
    }
    return "Timeout al esperar Mutex";
}
```

🔍 Controla si el hilo no puede obtener el mutex dentro de un tiempo límite.

---

## 🧪 Ejemplo 8: Recurso compartido con lógica condicional

```csharp
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
```

🔍 Simula control concurrente de stock limitado.

---

## 🧪 Ejemplo 9: Productor-consumidor simple

```csharp
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
```

🔍 Implementación simple sin condiciones, centrado en el control de acceso.

---

## 🧪 Ejemplo 10: Reinicio seguro de recurso

```csharp
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
```

🔍 Permite operaciones críticas sin interrupciones.

---

