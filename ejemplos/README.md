# Ejemplos prácticos y profesionales de `Mutex` en C#

Este documento presenta 10 ejemplos realistas y técnicamente justificados del uso de `Mutex` en C#, todos diseñados con hilos (`Thread`) para ilustrar cómo `Mutex` permite sincronizar secciones críticas incluso entre procesos si es necesario. Se explican las diferencias con `lock`, `Monitor`, `SemaphoreSlim`, y otros mecanismos.

---

## 🧪 Ejemplo 1: Exclusión mutua básica

```csharp
private static Mutex _mutex = new();

public static void AccesoUnico()
{
    _mutex.WaitOne();
    try
    {
        Console.WriteLine("Ejecutando sección crítica con Mutex.");
        Thread.Sleep(300);
    }
    finally
    {
        _mutex.ReleaseMutex();
    }
}
```

🔍 Protege una sección crítica para que solo un hilo la ejecute a la vez.

✅ **¿Por qué `Mutex`?**  
A diferencia de `lock`, permite sincronización entre procesos (si se usa un nombre). También útil para visibilidad externa.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: más rápido, pero solo funciona dentro del proceso.
- 🔄 `Monitor`: similar, pero sin soporte entre procesos.
- 📉 `Semaphore(Slim)`: permite acceso concurrente, no exclusividad total.
- 🔗 `Barrier`: no aplica.

---

## 🧪 Ejemplo 2: Mutex con nombre global (entre procesos)

```csharp
private static Mutex _mutexGlobal = new(false, "Global\\MiApp_Mutex");

public static void AccesoEntreProcesos()
{
    _mutexGlobal.WaitOne();
    try
    {
        Console.WriteLine("Sección crítica protegida entre procesos.");
        Thread.Sleep(500);
    }
    finally
    {
        _mutexGlobal.ReleaseMutex();
    }
}
```

🔍 Permite coordinar acceso entre diferentes aplicaciones.

✅ **¿Por qué `Mutex`?**  
Es el único mecanismo que soporta sincronización entre procesos usando un nombre global.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`, `Monitor`: no funcionan entre procesos.
- 📉 `Semaphore(Slim)`: también puede ser global, pero permite acceso concurrente.
- 🔄 `Barrier`: no aplica.

---

## 🧪 Ejemplo 3: Escritura a archivo exclusiva entre procesos

```csharp
private static Mutex _archivoMutex = new(false, "Global\\ArchivoLog");

public static void EscribirLog(string mensaje)
{
    _archivoMutex.WaitOne();
    try
    {
        File.AppendAllText("log_global.txt", mensaje + Environment.NewLine);
    }
    finally
    {
        _archivoMutex.ReleaseMutex();
    }
}
```

🔍 Garantiza que dos procesos distintos no escriban al archivo al mismo tiempo.

✅ **¿Por qué `Mutex`?**  
Necesario cuando varios ejecutables acceden a un mismo recurso (ej. archivo compartido).

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: solo útil dentro del proceso.
- 📉 `Semaphore`: permite más de un acceso, lo cual no queremos aquí.

---

## 🧪 Ejemplo 4: Control exclusivo en entorno multiproceso

```csharp
private static Mutex _mutex = new();

public static void SeccionProtegida(string nombre)
{
    _mutex.WaitOne();
    try
    {
        Console.WriteLine($"{nombre} accedió a la sección crítica.");
        Thread.Sleep(300);
    }
    finally
    {
        _mutex.ReleaseMutex();
    }
}
```

🔍 Ideal cuando varios hilos o instancias intentan ejecutar lo mismo.

✅ **¿Por qué `Mutex`?**  
Si se desea permitir ejecución exclusiva a nivel más amplio (por diseño, plugins, o recursos externos).

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: más rápido, pero limitado.
- 🔄 `Monitor`: requiere más control manual.
- 📉 `Semaphore`: no aplica.

---

## 🧪 Ejemplo 5: Mutex con timeout

```csharp
private static Mutex _mutex = new();

public static void IntentarAcceso(string nombre)
{
    if (_mutex.WaitOne(500))
    {
        try
        {
            Console.WriteLine($"{nombre} accedió a tiempo.");
            Thread.Sleep(300);
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }
    else
    {
        Console.WriteLine($"{nombre} no pudo obtener el Mutex (timeout).");
    }
}
```

🔍 Permite intentar entrar sin bloquear indefinidamente.

✅ **¿Por qué `Mutex`?**  
`Mutex.WaitOne(timeout)` es más claro que `Monitor.TryEnter`.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: no admite timeout.
- 🔄 `Monitor`: también lo permite, pero es más detallado.
- 📉 `Semaphore`: útil si varios pueden acceder, pero no es el caso.

---

## 🧪 Ejemplo 6: Mutex para evitar doble ejecución de aplicación

```csharp
private static Mutex _mutexUnico = new();

public static bool VerificarInstanciaUnica()
{
    bool creadaNueva;
    _mutexUnico = new Mutex(true, "Global\\MiAplicacionUnica", out creadaNueva);
    return creadaNueva;
}
```

🔍 Se asegura de que solo una instancia de la aplicación esté corriendo.

✅ **¿Por qué `Mutex`?**  
Es el único mecanismo que funciona a nivel de sistema operativo para evitar múltiples instancias.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`, `Monitor`: no aplican fuera del proceso.
- 📉 `Semaphore`: también posible, pero `Mutex` es más común para esta necesidad.

---

## 🧪 Ejemplo 7: Mutex entre librerías externas o módulos independientes

```csharp
private static Mutex _mutexModulo = new(false, "Global\\ModuloShared");

public static void AccederRecursoCompartido()
{
    _mutexModulo.WaitOne();
    try
    {
        Console.WriteLine("Módulo accediendo a recurso compartido.");
        Thread.Sleep(300);
    }
    finally
    {
        _mutexModulo.ReleaseMutex();
    }
}
```

🔍 Útil para coordinar el uso entre librerías desacopladas.

✅ **¿Por qué `Mutex`?**  
Soporta control explícito desde distintos ensamblados o componentes externos.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: solo funciona dentro del mismo ensamblado.
- 📉 `Semaphore`: permite múltiples accesos si no se configura apropiadamente.

---

## 🧪 Ejemplo 8: Escritura protegida en base de datos simulada

```csharp
private static Mutex _mutexBD = new();

public static void EscribirBaseDatos(string nombre)
{
    _mutexBD.WaitOne();
    try
    {
        Console.WriteLine($"{nombre} escribiendo en BD...");
        Thread.Sleep(300);
    }
    finally
    {
        _mutexBD.ReleaseMutex();
    }
}
```

🔍 Simula acceso exclusivo a una base de datos o recurso externo.

✅ **¿Por qué `Mutex`?**  
Si varias partes del sistema escriben al mismo recurso, `Mutex` garantiza acceso uno a la vez.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`, `Monitor`: solo protegen en memoria.
- 📉 `Semaphore`: permitiría concurrencia, lo cual no queremos aquí.

---

## 🧪 Ejemplo 9: Acceso secuencial entre procesos independientes

```csharp
private static Mutex _mutexSecuencia = new(false, "Global\\SecuenciaProcesos");

public static void AccesoSecuencial()
{
    _mutexSecuencia.WaitOne();
    try
    {
        Console.WriteLine("Proceso ejecutando su parte secuencial.");
        Thread.Sleep(400);
    }
    finally
    {
        _mutexSecuencia.ReleaseMutex();
    }
}
```

🔍 Cada proceso espera a que el anterior libere el recurso.

✅ **¿Por qué `Mutex`?**  
Soporta sincronización entre ejecutables distintos.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`, `Monitor`: inútiles entre procesos.
- 📉 `Semaphore`: podría dejar que varios pasen al mismo tiempo.

---

## 🧪 Ejemplo 10: Mutex para recurso crítico combinado (red + disco)

```csharp
private static Mutex _mutexRecursoCritico = new();

public static void AccesoCritico()
{
    _mutexRecursoCritico.WaitOne();
    try
    {
        Console.WriteLine("Accediendo a red y disco de forma segura.");
        Thread.Sleep(500);
    }
    finally
    {
        _mutexRecursoCritico.ReleaseMutex();
    }
}
```

🔍 Protege una sección que involucra múltiples recursos dependientes.

✅ **¿Por qué `Mutex`?**  
Asegura que solo un hilo use ambos recursos al mismo tiempo.

📊 **Comparación con otros mecanismos:**
- 🔐 `lock`: funcionaría, pero solo en memoria.
- 📉 `Semaphore`: permitiría concurrencia, lo cual no es seguro en este caso.

---

