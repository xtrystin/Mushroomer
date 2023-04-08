#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/API/Services/Warning/Warning.API/Warning.API.csproj", "src/API/Services/Warning/Warning.API/"]
COPY ["src/API/Services/Common/Common.csproj", "src/API/Services/Common/"]
COPY ["src/API/Services/Warning/Application/Application.csproj", "src/API/Services/Warning/Application/"]
COPY ["src/API/Services/Warning/Domain/Domain.csproj", "src/API/Services/Warning/Domain/"]
COPY ["src/API/Services/Warning/Infrastructure/Infrastructure.csproj", "src/API/Services/Warning/Infrastructure/"]
RUN dotnet restore "src/API/Services/Warning/Warning.API/Warning.API.csproj"
COPY . .
WORKDIR "/src/src/API/Services/Warning/Warning.API"
RUN dotnet build "Warning.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Warning.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Warning.API.dll"]