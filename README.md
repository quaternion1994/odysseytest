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

API documentating will be avalable by https://localhost:44385/swagger/index.html or https://odysseyserverapitest.azurewebsites.net/swagger/index.html addresses

# Setup admin
After previus setting up we need to connect admin to our API server. To do that go to OdysseyServer.Admin.Client project and select Program.cs. Change server address in this string
```
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://odysseyserverapitest.azurewebsites.net/api/") });

```