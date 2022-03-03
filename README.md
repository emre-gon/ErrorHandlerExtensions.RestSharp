[![NuGet](https://img.shields.io/nuget/v/ErrorHandlerExtensions.RestSharp.svg)](https://www.nuget.org/packages/ErrorHandlerExtensions.RestSharp)

# ErrorHandlerExtensions.RestSharp

NHibernate AutoMapping configurations with custom attributes and json column support.

It uses NHibernate's AutoMapping conventions for reading custom attributes over classes and properties and does database mapping, without the need of xml and and fluent mapping.



## Installation

```
dotnet add package ErrorHandlerExtensions.RestSharp
```

## Usage


### With Default Json Api Error Model

ApiException contains an ApiErrorModel which is the same model AspNetCore WebApi uses after unsuccessful Model Validation.

```cs

    RestClient client = new RestClient("https://localhost:44366/");

    var request1 = new RestRequest("Resource");

    try{
        var resource = client.GetApiAsync<int>(request1);
    }
    catch(ApiException exc){
        
        Console.WriteLine($"Status Code: {exc.Status } is returned  during Rest Call. Exception Message: {exc.Title}");
    }
    

```


### Custom Json Error Model


```cs

    RestClient client = new RestClient("https://localhost:44366/");

    var request1 = new RestRequest("Resource");

    try{
        var resource = client.GetApiAsync<int, MyCustomModel>(request1);
    }
    catch(RestException<MyCustomModel> exc){
        
        Console.WriteLine($"Status Code: {exc.Status } is returned  during Rest Call. Data: {exc.ErrorData.MyCustomModelField}");
    }
    

```

