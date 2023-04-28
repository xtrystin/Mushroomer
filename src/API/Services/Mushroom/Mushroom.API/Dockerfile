#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/API/Services/Mushroom/Mushroom.API/Mushroom.API.csproj", "src/API/Services/Mushroom/Mushroom.API/"]
COPY ["src/API/Services/Common/Common.csproj", "src/API/Services/Common/"]
RUN dotnet restore "src/API/Services/Mushroom/Mushroom.API/Mushroom.API.csproj"
COPY . .
WORKDIR "/src/src/API/Services/Mushroom/Mushroom.API"
RUN dotnet build "Mushroom.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mushroom.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mushroom.API.dll"]