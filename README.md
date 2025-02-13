# TodoApp - API de Gestín de Tareas (To-Do List)

Este proyecto es una API RESTful construida con .NET 8 siguiendo una arquitectura en capas (Domain, Application, Infrastructure, TodoApp). Permite la gestión de tareas con operaciones CRUD y un endpoint para marcar una tarea como completada.

## Requisitos Previos

Asegúrate de tener instalado lo siguiente:
- .NET 8 SDK
- Docker (Opcional)
- Postman o cURL (Opcional) para probar la API

## Instalación y Configuración

### Clonar el repositorio
```
git clone https://github.com/ArnedoAn/TodoApp.git
cd TodoApp
```

### Restaurar dependencias y compilar el proyecto
```
dotnet restore
dotnet build
```

## Ejecución de la API

### Opción 1: Ejecutar con .NET CLI
```
cd TodoApp
dotnet run
```

### Opción 2: Ejecutar con Docker
```
docker build -t todoapp .
docker run -p 3000:8080 --name todoapp todoapp
```
Accede a la API en `http://localhost:8080/api/todo`.

## Endpoints Disponibles

|   Método   | Endpoint                  | Descripción                      |
|------------|---------------------------|----------------------------------|
| **GET**    | `/api/todo`               | Obtener todas las tareas         |
| **GET**    | `/api/todo/{id}`          | Obtener una tarea por ID         |
| **POST**   | `/api/todo`               | Crear una nueva tarea            |
| **PUT**    | `/api/todo`               | Actualizar una tarea             |
| **PATCH**  | `/api/todo/{id}/complete` | Marcar una tarea como completada |
| **DELETE** | `/api/todo/{id}`          | Eliminar una tarea               |

## Ejemplos de Solicitudes

### Obtener todas las tareas
```
curl -X GET http://localhost:{port}/api/todo
```

### Obtener una tarea por ID
```
curl -X GET http://localhost:{port}/api/todo/1
```

### Crear una nueva tarea
```
curl -X POST http://localhost:{port}/api/todo \
     -H "Content-Type: application/json" \
     -d '{"title": "Mi nueva tarea", "isComplete": false}'
```

### Actualizar una tarea existente
```
curl -X PUT http://localhost:{port}/api/todo \
     -H "Content-Type: application/json" \
     -d '{"id": 1, "title": "Tarea actualizada", "isComplete": true}'
```

### Marcar una tarea como completada
```
curl -X PATCH http://localhost:{port}/api/todo/1/complete
```

### Eliminar una tarea
```
curl -X DELETE http://localhost:{port}/api/todo/1
```

## Pruebas Unitarias

Para ejecutar las pruebas unitarias, usa el siguiente comando:
```
dotnet test
```

Las pruebas incluidas validan:
- Obtener una tarea por ID.
- Crear una nueva tarea.
- Marcar una tarea como completada.

## Swagger (Documentación de la API)

La documentación Swagger está disponible en `http://localhost:{port}/swagger/index.html`.
Si no aparece, revisa que en `Program.cs` esté habilitado:
```
builder.Services.AddSwaggerGen();
app.UseSwagger();
app.UseSwaggerUI();
```

## Autor
Andrés Arnedo.
Proyecto desarrollado en .NET 8 con Clean Architecture.