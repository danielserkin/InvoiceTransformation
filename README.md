# InvoiceTransformation API

API REST que permite transformar documentos XML a formato JSON, desarrollada con .NET 8 y C#.

## Descripci�n

Este proyecto implementa una API que transforma documentos XML (codificados en Base64) a su representaci�n equivalente en formato JSON. La API est� protegida mediante OAuth 2.0 y sigue los principios RESTful.

## Caracter�sticas Principales

- Transformaci�n de XML a JSON
- Autenticaci�n OAuth 2.0
- Validaci�n de XML contra esquema XSD
- Documentaci�n OpenAPI
- Manejo estandarizado de errores (RFC 9457)

## Requisitos Previos

- .NET SDK 8.0 o superior
- IDE (Visual Studio 2022 recomendado)
- Postman (para pruebas de autenticaci�n)

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

## Configuraci�n y Despliegue

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

5. Ejecutar la aplicaci�n
```bash
dotnet run --project InvoiceTransformation.Presentation
```

## Uso de la API

### Endpoint de Transformaci�n

```http
POST /api/transform
Content-Type: application/json
Authorization: Bearer [AccessToken]

{
    "xml": "Base64-encoded XML"
}
```

### Configuraci�n OAuth 2.0 en Postman

```
Grant Type: Authorization Code
Auth URL: https://dev-otrwvksw.us.auth0.com/authorize?audience=https://invoice-transformation.cti.com
Access Token URL: https://dev-otrwvksw.us.auth0.com/oauth/token
Client ID: epBAbL2TSzmaJ9BL7nSGkL0MT3a42vfY
Client Secret: [Proporcionado en la documentaci�n]
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

La API implementa el est�ndar Problem Details (RFC 9457) para el manejo de errores:

```json
{
    "status": "400",
    "title": "Bad Request",
    "detail": "Invalid XML structure",
    "errors": []
}
```

## Tecnolog�as Utilizadas

- .NET 8
- C#
- Auth0 (OAuth 2.0)
- xUnit (Testing)

## Contribuci�n

Este proyecto es parte de un reto t�cnico y no acepta contribuciones externas.

## Mejoras Potenciales

El proyecto actual cumple con los requerimientos b�sicos del reto t�cnico, pero hay varias �reas donde se podr�a mejorar y expandir:

### Dominio y Entidades
- Implementar una capa de dominio m�s robusta con el modelo `Comprobante` como entidad principal
- Agregar value objects para conceptos como importes, identificadores fiscales, etc.
- Implementar validaciones de dominio espec�ficas del negocio

### Arquitectura y Escalabilidad
- Distribuir la l�gica de transformaci�n en componentes m�s peque�os y especializados
- Implementar el patr�n Repository para abstraer el acceso a datos
- Implementar un sistema de logging m�s completo

### Validaciones y Seguridad
- Implementar validaciones m�s exhaustivas del XML
- Agregar rate limiting para proteger la API
- Implementar pol�ticas de retry para mejorar la resiliencia
- Agregar m�s pruebas unitarias y de integraci�n
- Implementar validaci�n de esquema XSD m�s robusta


Estas mejoras no fueron implementadas inicialmente debido a las restricciones de tiempo del reto t�cnico, pero representan un camino claro para evolucionar la aplicaci�n hacia una soluci�n empresarial m�s robusta.
