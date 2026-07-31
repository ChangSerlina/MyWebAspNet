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
## 寫好model之後，使用 Scaffolding 工具來為模型產生 Controller 和 Create、Read、Update、Delete (CRUD) 頁面
eg. Models/PayAccount.cs

```sh
export PATH=$HOME/.dotnet/tools:$PATH
dotnet aspnet-codegenerator controller -name PayAccountController -m PayAccount -dc MyWebAspNet.Data.ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries --databaseProvider sqlite
```

## 建立資料庫
![ERD](ERD)

```sh
dotnet ef migrations add PayAccountCreate
# 如果要還原
dotnet ef migrations remove

dotnet ef database update
```
```sh
# 執行之後如果要還原到一開始的狀態
dotnet ef database update CreateIdentitySchema
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