# Backend .NET

## Configuração do projeto
```
dotnet restore
```

## Configurar o appsettings.Development.json
```
  "MongoDB": {
    "ConnectionString": "SUA_CONNECTIONSTRING",
    "Database": "ecommerceprivacy"
  },

  "RabbitMQ": {
    "Servidor": "SEU_HOST",
    "Usuario": "SEU_USER",
    "Senha": "SUA_PASSWORD",
    "VirtualHost": "SEU_VHOST"
  },
```

### Compilar o projeto
```
dotnet build
```

### Rodar o projeto
```
dotnet run
```
