## For macOS on VScode use docker
- docker compose up -d
- docker compose down
- docker compose restart

## Dotnet常見指令
```sh
dotnet --version
dotnet restore
dotnet build
dotnet watch run
dotnet clean
```

## 建立具備身份驗證功能的 ASP.NET Core 專案
```sh
dotnet new mvc -au Individual -o MyWebAspNet
```

## 建立資料庫
```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
# 如果要還原
dotnet ef migrations remove
# 刪除資料庫
dotnet ef database drop
```

## 如果本機快取掛了
- 全部清除，重build
```sh
dotnet nuget locals all --clear
rm -rf bin obj
dotnet restore
dotnet build
```