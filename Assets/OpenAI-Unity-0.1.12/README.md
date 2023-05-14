[![Twitter](https://img.shields.io/twitter/url/https/twitter.com/sgt3v.svg?style=social&label=Follow%20%40sgt3v)](https://twitter.com/sgt3v)


## OpenAI Unity Package
An unofficial Unity package that allows you to use the OpenAI API directly in the Unity game engine.

## How To Use

### Importing the Package
To import the package, follow these steps:
- Open Unity 2019 or later
- Go to `Window > Package Manager`
- Click the `+` button and select `Add package from git URL`
- Paste the repository URL https://github.com/srcnalt/OpenAI-Unity.git and click `Add`

### Setting Up Your OpenAI Account
To use the OpenAI API, you need to have an OpenAI account. Follow these steps to create an account and generate an API key:

- Go to https://openai.com/api and sign up for an account
- Once you have created an account, go to https://beta.openai.com/account/api-keys
- Create a new secret key and save it

### Saving Your Credentials
To make requests to the OpenAI API, you need to use your API key and organization name (if applicable). To avoid exposing your API key in your Unity project, you can save it in your device's local storage.

To do this, follow these steps:

- Create a folder called .openai in your home directory (e.g. `C:User\UserName\` for Windows or `~\` for Linux or Mac)
- Create a file called `auth.json` in the `.openai` folder
- Add an api_key field and a organization field (if applicable) to the auth.json file and save it
- Here is an example of what your auth.json file should look like:

```json
{
    "api_key": "sk-...W6yi",
    "organization": "org-...L7W"
}
```

You can also pass your API key into `OpenAIApi` ctor when creating an instance of it but again, this is not recommended!

```csharp
var openai = new OpenAIApi("sk-Me8...6yi");
```

**IMPORTANT:** Your API key is a secret. 
Do not share it with others or expose it in any client-side code (e.g. browsers, apps). 
If you are using OpenAI for production, make sure to run it on the server side, where your API key can be securely loaded from an environment variable or key management service.

### Making Requests to OpenAPI
You can use the `OpenAIApi` class to make async requests to the OpenAI API.

All methods are asynchronous and can be accessed directly from an instance of the `OpenAIApi` class.

Here is an example of how to make a request:

```csharp
private async void SendRequest()
{
    var openai = new OpenAIApi();
    var request = new CreateCompletionRequest{
        Model="text-davinci-003",
        Prompt="Say this is a test",
    };
    var response = await openai.CreateCompletion(request);
}
```

To make a stream request, you can use the `CreateCompletionAsync` and `CreateChatCompletionAsync` methods. 
These methods are going to set `Stream` property of the request to `true` and return responses through an onResponse callback.
In this case text responses are stored in `Delta` property of the `Choices` field.

```csharp
var req = new CreateCompletionRequest{
    Model = "text-davinci-003",
    Prompt = "Say this is a test.",
    MaxTokens = 7,
    Temperature = 0
};
    
openai.CreateCompletionAsync(req, 
    (responses) => {
        var result = string.Join("", responses.Select(response => response.Choices[0].Delta.Content));
        Debug.Log(result);
    }, 
    () => {
        Debug.Log("completed");
    }, 
    new CancellationTokenSource());
}
```

### Sample Projects
This package includes two sample scenes that you can import via the Package Manager:

- **ChatGPT sample:** A simple ChatGPT like chat example.
- **DallE sample:** A DALL.E text to image generation example.

### Known Issues
- **Can't See the Image Result in WebGL Builds:** Due to CORS policy of OpenAI image storage in local WebGL builds you will get the generated image's URL however it will not be
downloaded using UnityWebRequest until you run it out of localhost, on a server.

### Further Reading
For more information on how to use the various request parameters, 
please refer to the OpenAI documentation: https://beta.openai.com/docs/api-reference/introduction
