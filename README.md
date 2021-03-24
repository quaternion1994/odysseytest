# Setup API project
The server uses MS SQL server and Redis. Before staring we have to some setups. In OdysseyServer.Api project select appsettings.json. We need to change "DbConfiguration"> "ConnectionStrings" and "CacheConfiguration" > "Host" properties values.

By default we see settings for test azure services
```
  "DbConfiguration": {
    "ConnectionStrings": "Data Source=odysseyserverapidbserver.database.windows.net;Initial Catalog=OdysseyServer.Api_db;User ID=test;Password=A12121212#;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
  },
  "CacheConfiguration": {
    "Host": "odysseytest.redis.cache.windows.net:6379"
  },
```
You can use one of cases for caching. First is fast Redis or more slow but cheap SQL database.
If you want SQL DB then remove  CacheConfiguration section from configuration and add connection string. For example

```
	"CacheConnectionString": "Data Source=odysseyserverapidbserver.database.windows.net;Initial Catalog=OdysseyServer.Cache_db;User ID=test;Password=A12121212#;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
```
Before using you must initialize caching database using console command

```dotnet sql-cache create "Data Source=localhost;Initial Catalog=cachedb;Integrated Security=True" dbo odysseydb```
 where "odysseydb" is table name and "cachedb" is database name.
 
API documentating will be avalable by https://localhost:44385/swagger/index.html or https://odysseyserverapitest.azurewebsites.net/swagger/index.html addresses
All data models are avalable in Protobuf format in common "OdysseyServer.ApiClient" project => "Messages.proto" file. 
This is common and indepenedent project that can be connected by any clients. It also contains HttpClient extensions to use Protobuf with REST.

# Setup admin
After previus setting up we need to connect admin to our API server. To do that go to OdysseyServer.Admin.Client project and select Program.cs. Change server address in this string
```
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://odysseyserverapitest.azurewebsites.net/api/") });

```

Test project is on https://odysseyserveradminclient20210323164951.azurewebsites.net/