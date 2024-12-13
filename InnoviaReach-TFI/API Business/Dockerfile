#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["API Business/API Business.csproj", "API Business/"]
COPY ["4. Infraestructure/Infraestructure.csproj", "4. Infraestructure/"]
COPY ["Core.Contracts/Core.Contracts.csproj", "Core.Contracts/"]
COPY ["Core.Domain/Core.Domain.csproj", "Core.Domain/"]
COPY ["Transversal.Helpers/Transversal.Helpers.csproj", "Transversal.Helpers/"]
COPY ["Transversal.Extensions/Transversal.Extensions.csproj", "Transversal.Extensions/"]
COPY ["IoC.Resolver/IoC.Resolver.csproj", "IoC.Resolver/"]
COPY ["3. Core/Core.Business.csproj", "3. Core/"]
COPY ["Transversal.EmailService/Transversal.EmailService.csproj", "Transversal.EmailService/"]
COPY ["Transversal.StorageService/Transversal.StorageService.csproj", "Transversal.StorageService/"]
RUN dotnet restore "API Business/API Business.csproj"
COPY . .
WORKDIR "/src/API Business"
RUN dotnet build "API Business.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API Business.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API Business.dll"]