# TextileSystem/Dockerfile
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY TextileWarehouseERP/TextileWarehouseERP.csproj ./TextileWarehouseERP/
RUN dotnet restore TextileWarehouseERP/TextileWarehouseERP.csproj

COPY TextileWarehouseERP/. ./TextileWarehouseERP/
WORKDIR /src/TextileWarehouseERP
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/runtime:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "TextileWarehouseERP.dll"]