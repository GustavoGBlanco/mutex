# Módulo 3: `Mutex` en C#

## 🔐 ¿Qué es un `Mutex`?
Un `Mutex` (Mutual Exclusion) es un mecanismo de sincronización que **garantiza acceso exclusivo a un recurso**, incluso entre **procesos distintos**.

A diferencia de `lock`, que solo sirve dentro del mismo proceso, `Mutex` funciona a nivel del sistema operativo.

---

## 📊 Cuándo usar `Mutex`

| Situación | Usar `lock` | Usar `Mutex` |
|-----------|--------------|---------------|
| Varios hilos en una app | ✅ | ❌ |
| Varios procesos accediendo al mismo recurso | ❌ | ✅ |

---

## 🏠 Escenario práctico: **Simulador de acceso exclusivo a archivo**

Vamos a simular dos instancias de una aplicación que quieren acceder al mismo recurso (por ejemplo, escribir en un archivo). El `Mutex` garantiza que **solo una** lo haga a la vez.

### Archivos

#### `ExclusiveFileWriter.cs`
```csharp
using System;
using System.IO;
using System.Threading;

public class ExclusiveFileWriter
{
    private static readonly string MutexName = "Global\MiArchivoMutex";
    private static readonly string FilePath = "registro.txt";

    public static void Escribir(string mensaje)
    {
        using var mutex = new Mutex(false, MutexName);

        Console.WriteLine("Esperando acceso exclusivo...");
        mutex.WaitOne(); // Espera hasta obtener el mutex

        try
        {
            Console.WriteLine("Acceso garantizado. Escribiendo...");
            File.AppendAllText(FilePath, mensaje + Environment.NewLine);
            Thread.Sleep(2000); // Simula trabajo
            Console.WriteLine("Escritura terminada.");
        }
        finally
        {
            mutex.ReleaseMutex();
            Console.WriteLine("Acceso liberado.");
        }
    }
}
```

#### `Program.cs`
```csharp
using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Proceso iniciado.");
        ExclusiveFileWriter.Escribir($"Registro desde PID {Environment.ProcessId} a las {DateTime.Now}");
    }
}
```

---

## 🤔 Notas importantes

- El nombre del `Mutex` puede ser global (ej. `Global\Nombre`) para compartirlo entre procesos.
- `WaitOne()` bloquea el hilo hasta que pueda entrar.
- `ReleaseMutex()` libera el acceso. Siempre llamalo en un `finally`.

---

## 🧼 Buenas prácticas con `Mutex`

| Regla | Motivo |
|-------|--------|
| ✅ Usá nombres globales si lo compartís entre procesos | El sistema lo reconoce universalmente |
| ✅ Liberá siempre el `Mutex` con `finally` | Evita bloqueos si algo falla |
| ❌ No lo uses para sincronización interna de hilos | `lock` o `SemaphoreSlim` es mejor |

---
