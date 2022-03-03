[![NuGet](https://img.shields.io/nuget/v/ErrorHandlerExtensions.RestSharp.svg)](https://www.nuget.org/packages/ErrorHandlerExtensions.RestSharp)

# ErrorHandlerExtensions.RestSharp

RestSharp extension where Rest calls throws exception when HttpStatusCode is not Success(200*)

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

