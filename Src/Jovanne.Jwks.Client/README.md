# Jovanne.Jwks.Client

Pacote para APIs ASP.NET Core que precisam validar tokens JWT usando a chave publica exposta por um endpoint JWKS.

Este pacote nao depende de `DbContext`, ASP.NET Core Identity ou Entity Framework Core. Ele e indicado para servicos consumidores de tokens.

## Instalacao

```bash
dotnet add package Jovanne.Jwks.Client
```

## Configuracao

Registre a autenticacao JWT no `Program.cs`:

```csharp
builder.Services.AddJovanneJwksClient(
    builder.Configuration,
    builder.Environment.IsDevelopment());
```

Configure a secao `JwtSettings`:

```json
{
  "JwtSettings": {
    "AutenticacaoJwksUrl": "https://auth.example.com",
    "Issuer": "https://auth.example.com"
  }
}
```

`AutenticacaoJwksUrl` e obrigatorio. O pacote consulta as chaves em `{AutenticacaoJwksUrl}/jwks`.

`Issuer` e opcional. Quando nao informado, o valor de `AutenticacaoJwksUrl` e usado como emissor.

## Recursos incluidos

- Configuracao de `JwtBearer`.
- Validacao de assinatura por JWKS.
- Respostas JSON padronizadas para `401 Unauthorized` e `403 Forbidden`.
- `ClaimsAuthorizeAttribute` para proteger endpoints por claim.
- `IUser` e `AspNetUser` para acessar dados do usuario autenticado.
- Extensoes para obter id e e-mail a partir de `ClaimsPrincipal`.

## Exemplo de autorizacao por claim

```csharp
[ClaimsAuthorize("orders:read")]
public IActionResult GetOrders()
{
    return Ok();
}
```

Por padrao, o nome da claim e `permission`. Para usar outro nome:

```csharp
[ClaimsAuthorize("admin", "role")]
public IActionResult Admin()
{
    return Ok();
}
```

## Dependencias

- `Microsoft.AspNetCore.App`
- `NetDevPack.Security.JwtExtensions`
