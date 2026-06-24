# Jovanne.Jwks.Server

Pacote para aplicacoes ASP.NET Core que emitem tokens e precisam gerenciar chaves JWKS persistidas no banco de dados.

Este pacote concentra as dependencias de servidor: ASP.NET Core Identity, Entity Framework Core e o armazenamento de chaves JWKS.

## Instalacao

```bash
dotnet add package Jovanne.Jwks.Server
```

## Configuracao

Registre o servidor JWKS no `Program.cs`:

```csharp
builder.Services.AddJovanneJwksServer<ApplicationUser, IdentityRole, ApplicationDbContext>();
```

O metodo registra:

- `AddJwksManager` com assinatura `RsaSsaPssSha256`.
- Persistencia das chaves JWKS via Entity Framework Core.
- ASP.NET Core Identity com roles, stores do EF Core e token providers padrao.

## Requisitos dos tipos genericos

```csharp
where TRole : IdentityRole
where TUser : IdentityUser
where TDbContext : DbContext, ISecurityKeyContext
```

O `ApplicationDbContext` precisa herdar de `DbContext` e implementar `ISecurityKeyContext`.

## Exemplo de DbContext

```csharp
public class ApplicationDbContext :
    IdentityDbContext<ApplicationUser, IdentityRole, string>,
    ISecurityKeyContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<KeyMaterial> SecurityKeys { get; set; }
}
```

Consulte a documentacao do `NetDevPack.Security.Jwt.Store.EntityFrameworkCore` para os detalhes exatos exigidos por `ISecurityKeyContext`.

## Dependencias

- `Microsoft.AspNetCore.App`
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `NetDevPack.Security.Jwt.AspNetCore`
- `NetDevPack.Security.Jwt.Store.EntityFrameworkCore`
