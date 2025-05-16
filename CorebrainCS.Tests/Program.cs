using CorebrainCS;

Console.WriteLine("Hello, World!");

// For now it only works on windows
var corebrain = new CorebrainCS.CorebrainCS("../.venv/Scripts/python.exe", "../corebrain/corebrain/cli", false);

// Console.WriteLine(corebrain.Version());

try
{
    string token = "your_api_token";
    string result = corebrain.Token(token);
    Console.WriteLine("Result with token: " + result);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

try
{
    string apiKey = "your_api_key";
    string result = corebrain.ApiKey(apiKey);
    Console.WriteLine("Result with API Key: " + result);
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

