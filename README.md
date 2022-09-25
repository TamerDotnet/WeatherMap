# Weather Map
Solution has been developed using .NET 6.0. Some nuget packages has been used to support solution. Mainly the AspNetCoreRateLimit package has been used to validate the number of request per Api Key. Required configuratiuon which is 5 requests per hour has been setup within the Startup of WeatherMap.API Project.

ApiKeyRequired Attribute has been added in order to make sure API does not get called if the Key is not provided.

#Testing
Integration Tests used the following packages which are
NUnit
NSubstitute
FluentAssertions

Extracted Json data has been saved under TestData Folder within the WeatherMap.Tests Project.

Following animated screen shot showing testing steps 

https://onedrive.live.com/?authkey=%21ADs4lOCrS%2DdHj6U&cid=F68B66E7C8162F47&id=F68B66E7C8162F47%212684&parId=F68B66E7C8162F47%212683&o=OneUp




