## Secret para Desenvolvimento

Importante observar que o uso desta ferramenta é para ambiente de desenvolvimento. Você terá problemas se tentar utilizar em ambiente de produção.
<https://docs.microsoft.com/pt-br/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows>

###### Na raíz do projeto digitar o seguinte comando para usar segredos em Asp.NET Core:
```
dotnet user-secrets init
```

Comando para setar um segredo:
```
dotnet user-secrets set "DatabasePassword" "123456"
```

Consultar Segredos definidos no projeto:
```
dotnet user-secrets list
```

Obs: Os segredos ficam salvos no computador local no seguinte  diretório (não criptografados):
```
 %APPDATA%/Microsoft/UserSecrets
```

#### Recuperar um Segredo

Vamos usar como exemplo a conexão com banco de dados. 

Em aplicações .NET Core, a ConnectionString é estabelecida no arquivo appsettings.json, localizado na raíz do projeto:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },  
  "AllowedHosts": "*",
  
  "MyDataBaseConn": {
    "ConnectionStrings": "Server=localhost;Database=MyDataBase;user=admin"
  }
}
```

Perceba que não expomos a senha do banco de dados no código da aplicação. 
Vamos recuperá-la através do Segredo definido anteriormente:

###### Startup.cs::
```
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }       
}
```     

###### Controller:
```
public class HomeController: Controller
{
    private IConfiguration _configuration { get; }
    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string ConnectionStrings()
    {
        var builder = new SqlConnectionStringBuilder(
                _configuration.GetSection("MyDataBaseConn").GetSection("ConnectionStrings").Value);

        builder.Password = _configuration["DatabasePassword"];  // => Recuperando o segredo 
        string connection = builder.ConnectionString;
        return connection;
    }

}
```

#### Outros comandos

Remover um segredo específico:
```
dotnet user-secrets remove "Movies:ConnectionString"
```

Remover todos os segredos do projeto:
```
dotnet user-secrets clear
```
