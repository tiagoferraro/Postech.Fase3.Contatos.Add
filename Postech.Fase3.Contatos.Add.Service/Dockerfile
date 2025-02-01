# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Postech.Fase3.Contatos.Add.Service/Postech.Fase3.Contatos.Add.Service.csproj", "Postech.Fase3.Contatos.Add.Service/"]
COPY ["Postech.Fase3.Contatos.Add.Application/Postech.Fase3.Contatos.Add.Application.csproj", "Postech.Fase3.Contatos.Add.Application/"]
COPY ["Postech.Fase3.Contatos.Add.Infra/Postech.Fase3.Contatos.Add.Infra.csproj", "Postech.Fase3.Contatos.Add.Infra/"]
COPY ["Postech.Fase3.Contatos.Add.Domain/Postech.Fase3.Contatos.Add.Domain.csproj", "Postech.Fase3.Contatos.Add.Domain/"]
COPY ["Postech.Fase3.Contatos.Add.Infra.CrossCuting/Postech.Fase3.Contatos.Add.Infra.CrossCuting.csproj", "Postech.Fase3.Contatos.Add.Infra.CrossCuting/"]
COPY ["Postech.Fase3.Contatos.Add.Infra.Ioc/Postech.Fase3.Contatos.Add.Infra.Ioc.csproj", "Postech.Fase3.Contatos.Add.Infra.Ioc/"]
RUN dotnet restore "./Postech.Fase3.Contatos.Add.Service/Postech.Fase3.Contatos.Add.Service.csproj"
COPY . .
WORKDIR "/src/Postech.Fase3.Contatos.Add.Service"
RUN dotnet build "./Postech.Fase3.Contatos.Add.Service.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Postech.Fase3.Contatos.Add.Service.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Postech.Fase3.Contatos.Add.Service.dll"]