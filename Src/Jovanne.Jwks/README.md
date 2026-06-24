# Jovanne.Jwks

Pacote agregador para aplicacoes ASP.NET Core que precisam configurar client e server JWKS no mesmo processo.

Use este pacote quando a mesma aplicacao valida tokens JWT e tambem hospeda o servidor de autenticacao/JWKS. Para cenarios separados, prefira instalar `Jovanne.Jwks.Client` ou `Jovanne.Jwks.Server` diretamente.

## Instalacao

```bash
dotnet add package Jovanne.Jwks
```

## Configuracao

```csharp
builder.Services.AddJovanneJwksFull<ApplicationUser, IdentityRole, ApplicationDbContext>(
    builder.Configuration,
    builder.Environment.IsDevelopment());
```

Esse metodo chama:

- `AddJovanneJwksServer<TUser, TRole, TDbContext>()`
- `AddJovanneJwksClient(configuration, isDevelopment)`

## Requisitos

Configure a secao `JwtSettings`:

```json
{
  "JwtSettings": {
    "AutenticacaoJwksUrl": "https://auth.example.com",
    "Issuer": "https://auth.example.com"
  }
}
```

O `TDbContext` deve herdar de `DbContext` e implementar `ISecurityKeyContext`.

## Dependencias

Este pacote referencia os dois pacotes especializados:

- `Jovanne.Jwks.Client`
- `Jovanne.Jwks.Server`

Por isso, ele tambem carrega as dependencias de client e server. Se a aplicacao so consome tokens, use `Jovanne.Jwks.Client` para evitar dependencias de EF Core e Identity.
