# InvoiceTransformation API

API REST que permite transformar documentos XML a formato JSON, desarrollada con .NET 8 y C#.

## Descripción

Este proyecto implementa una API que transforma documentos XML (codificados en Base64) a su representación equivalente en formato JSON. La API está protegida mediante OAuth 2.0 y sigue los principios RESTful.

## Características Principales

- Transformación de XML a JSON
- Autenticación OAuth 2.0
- Validación de XML contra esquema XSD
- Documentación OpenAPI
- Manejo estandarizado de errores (RFC 9457)

## Requisitos Previos

- .NET SDK 8.0 o superior
- IDE (Visual Studio 2022 recomendado)
- Postman (para pruebas de autenticación)

## Estructura del Proyecto

```
InvoiceTransformation/
??? InvoiceTransformation/
?   ??? Program.cs
??? InvoiceTransformation.Application/
?   ??? Interfaces/
?   ??? Services/
??? InvoiceTransformation.Domain/
??? InvoiceTransformation.Infrastructure/
??? InvoiceTransformation.Presentation/
?   ??? Controllers/
??? InvoiceTransformation.Tests/
```

## Configuración y Despliegue

1. Clonar el repositorio
```bash
git clone [URL-del-repositorio]
```

2. Restaurar dependencias
```bash
dotnet restore
```

3. Compilar el proyecto
```bash
dotnet build
```

4. Ejecutar los tests
```bash
dotnet test
```

5. Ejecutar la aplicación
```bash
dotnet run --project InvoiceTransformation.Presentation
```

## Uso de la API

### Endpoint de Transformación

```http
POST /api/transform
Content-Type: application/json
Authorization: Bearer [AccessToken]

{
    "xml": "Base64-encoded XML"
}
```

### Configuración OAuth 2.0 en Postman

```
Grant Type: Authorization Code
Auth URL: https://dev-otrwvksw.us.auth0.com/authorize?audience=https://invoice-transformation.cti.com
Access Token URL: https://dev-otrwvksw.us.auth0.com/oauth/token
Client ID: epBAbL2TSzmaJ9BL7nSGkL0MT3a42vfY
Client Secret: [Proporcionado en la documentación]
```

### Ejemplo de Respuesta Exitosa

```json
{
    "Emisor": {},
    "Receptor": {},
    "Conceptos": [],
    "Impuestos": {}
}
```

## Manejo de Errores

La API implementa el estándar Problem Details (RFC 9457) para el manejo de errores:

```json
{
    "status": "400",
    "title": "Bad Request",
    "detail": "Invalid XML structure",
    "errors": []
}
```

## Tecnologías Utilizadas

- .NET 8
- C#
- Auth0 (OAuth 2.0)
- xUnit (Testing)

## Contribución

Este proyecto es parte de un reto técnico y no acepta contribuciones externas.

## Mejoras Potenciales

El proyecto actual cumple con los requerimientos básicos del reto técnico, pero hay varias áreas donde se podría mejorar y expandir:

### Dominio y Entidades
- Implementar una capa de dominio más robusta con el modelo `Comprobante` como entidad principal
- Agregar value objects para conceptos como importes, identificadores fiscales, etc.
- Implementar validaciones de dominio específicas del negocio

### Arquitectura y Escalabilidad
- Distribuir la lógica de transformación en componentes más pequeños y especializados
- Implementar el patrón Repository para abstraer el acceso a datos
- Implementar un sistema de logging más completo

### Validaciones y Seguridad
- Implementar validaciones más exhaustivas del XML
- Agregar rate limiting para proteger la API
- Implementar políticas de retry para mejorar la resiliencia
- Agregar más pruebas unitarias y de integración
- Implementar validación de esquema XSD más robusta


Estas mejoras no fueron implementadas inicialmente debido a las restricciones de tiempo del reto técnico, pero representan un camino claro para evolucionar la aplicación hacia una solución empresarial más robusta.
