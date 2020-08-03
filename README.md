###### Na raíz do projeto digitar o seguinte comando para usar segredos em Asp.NET Core:
```
dotnet user-secrets init
```

Comando para setar um segredo:
```
dotnet user-secrets set "email:senha" "123456"
```

Consultar Segredos definidos no projeto:
```
dotnet user-secrets list
```

Obs: Os segredos ficam salvos no computador local no seguinte  diretório (não criptografados):
```
 %APPDATA%/Microsoft/UserSecrets
```


Recuperar o valor de um segredo (Startup):
```
senhaEmail = Configuration["email:senha"];
```

Recuperar valor no Controller:


###### Startup.cs::
```
public class Startup
{        
  public static string senhaEmail{ get; set; }

  public void ConfigureServices(IServiceCollection services)
  {       
      senhaEmail = Configuration["email:senha"];                        
  }
}
```     

###### Controller:
```
public class EmailController: Controller
{
  private string _senhaEmail;
  public EmailController()
  {
     _senhaEmail = Startup.senhaEmail;
  }
}
```

Remover um segredo específico:
```
dotnet user-secrets remove "Movies:ConnectionString"
```

Remover todos os segredos do projeto:
```
dotnet user-secrets clear
```
