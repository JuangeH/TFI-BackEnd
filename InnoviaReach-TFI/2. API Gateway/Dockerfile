#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["2. API Gateway/API Gateway.csproj", "2. API Gateway/"]
COPY ["3. Core/Core.Business.csproj", "3. Core/"]
COPY ["Core.Contracts/Core.Contracts.csproj", "Core.Contracts/"]
COPY ["Core.Domain/Core.Domain.csproj", "Core.Domain/"]
COPY ["Transversal.Helpers/Transversal.Helpers.csproj", "Transversal.Helpers/"]
COPY ["Transversal.EmailService/Transversal.EmailService.csproj", "Transversal.EmailService/"]
COPY ["Transversal.Extensions/Transversal.Extensions.csproj", "Transversal.Extensions/"]
COPY ["4. Infraestructure/Infraestructure.csproj", "4. Infraestructure/"]
COPY ["IoC.Resolver/IoC.Resolver.csproj", "IoC.Resolver/"]
COPY ["Transversal.StorageService/Transversal.StorageService.csproj", "Transversal.StorageService/"]
RUN dotnet restore "2. API Gateway/API Gateway.csproj"
COPY . .
WORKDIR "/src/2. API Gateway"
RUN dotnet build "API Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API Gateway.dll"]