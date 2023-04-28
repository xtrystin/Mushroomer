#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/API/Services/User/User.API/User.API.csproj", "src/API/Services/User/User.API/"]
COPY ["src/API/Services/User/User.Application/User.Application.csproj", "src/API/Services/User/User.Application/"]
COPY ["src/API/Services/Common/Common.csproj", "src/API/Services/Common/"]
COPY ["src/API/Services/User/User.Domain/User.Domain.csproj", "src/API/Services/User/User.Domain/"]
COPY ["src/API/Services/User/User.Infrastructure/User.Infrastructure.csproj", "src/API/Services/User/User.Infrastructure/"]
RUN dotnet restore "src/API/Services/User/User.API/User.API.csproj"
COPY . .
WORKDIR "/src/src/API/Services/User/User.API"
RUN dotnet build "User.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.API.dll"]