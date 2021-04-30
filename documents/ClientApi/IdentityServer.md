## Nuget Install

    Install-Package IdentityServer4 -Version 4.1.2

    Install-Package  Microsoft.AspNetCore.Authentication.JwtBearer -Version  4.1.2

    Install-Package  Microsoft.AspNetCore.Authentication.OpenIdConnect -Version  4.1.2

    Install-Package IdentityServer4.EntityFramework -Version 4.1.2

    Install-Package IdentityServer4.EntityFramework.Storage -Version  4.1.2

    install-package  IdentityServer4.AccessTokenValidation -Version  3.0.1

    install-package IdentityServer4.AspNetIdentity -Version 4.1.2


#### Database

    Install-Package Microsoft.EntityFrameworkCore.Design -Version 3.1.7
    Install-Package Microsoft.EntityFrameworkCore.Tools -Version 3.1.7

    Install-Package Pomelo.EntityFrameworkCore.MySql -Version 3.1.1

#### Sqlserver
    Install-package Microsoft.EntityFrameworkCore.SqlServer -Version 3.1.7

#### Sqlite 
    Install-package Microsoft.EntityFrameworkCore.Sqlite -Version 3.1.7


## Db Migration

#### sqlite
    add-migration InitPersistedGrant -Context PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb

    add-migration InitConfiguration -Context ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb

    add-migration InitPTAuthenticationDbContext -Context PTAuthenticationDbContext -o Data/Migrations/IdentityServer/AuthenticationDb

    update-database -Context PersistedGrantDbContext
    update-database -Context ConfigurationDbContext
    update-database -Context PTAuthenticationDbContext


#### Mysql
    add-migration InitialIdentityServerPersistedGrantDbMigration -Context PersistedGrantDbContext -o Data/Mysql/Migrations/IdentityServer/PersistedGrantDb

    add-migration InitialIdentityServerConfigurationDbMigration -Context ConfigurationDbContext -o Data/Mysql/Migrations/IdentityServer/ConfigurationDb

    add-migration InitialIdentityServerSaasAuthenticationDbMigration -Context PTAuthenticationDbContext -o Data/Mysql/Migrations/IdentityServer/AuthenticationDb

    update-database -Context PersistedGrantDbContext
    update-database -Context ConfigurationDbContext
    update-database -Context PTAuthenticationDbContext

#### cli mysql

    dotnet ef migrations add InitPersistedGrant -Context PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb

    dotnet ef migrations add InitConfiguration  -Context ConfigurationDbContext  -o Migrations/IdentityServer/ConfigurationDb

    dotnet ef migrations add InitPTAuthenticationDbContext  -Context PTAuthenticationDbContext  -o Migrations/IdentityServer/AuthenticationDb

    dotnet ef database update -Context PersistedGrantDbContext
    dotnet ef database update -Context ConfigurationDbContext
    dotnet ef database update -Context PTAuthenticationDbContext