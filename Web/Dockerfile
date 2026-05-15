FROM node:20-alpine AS client-build
WORKDIR /src/Web/ClientApp
COPY Web/ClientApp/package*.json ./
RUN npm install --no-audit --no-fund
COPY Web/ClientApp/ ./
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Web/Web.csproj Web/
COPY Business/Business.csproj Business/
COPY Data/Data.csproj Data/
RUN dotnet restore Web/Web.csproj
COPY . .
COPY --from=client-build /src/Web/wwwroot Web/wwwroot
RUN dotnet publish Web/Web.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "Web.dll"]
