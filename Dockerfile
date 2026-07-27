FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:5259

EXPOSE 5259

#專案名稱.dll
ENTRYPOINT ["dotnet","MyWebAspNet.dll"]