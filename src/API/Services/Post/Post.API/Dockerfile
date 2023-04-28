#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/API/Services/Post/Post.API/Post.API.csproj", "src/API/Services/Post/Post.API/"]
COPY ["src/API/Services/Post/Post.Application/Post.Application.csproj", "src/API/Services/Post/Post.Application/"]
COPY ["src/API/Services/Common/Common.csproj", "src/API/Services/Common/"]
COPY ["src/API/Services/Post/Post.Domain/Post.Domain.csproj", "src/API/Services/Post/Post.Domain/"]
COPY ["src/API/Services/Post/Post.Infrastructure/Post.Infrastructure.csproj", "src/API/Services/Post/Post.Infrastructure/"]
RUN dotnet restore "src/API/Services/Post/Post.API/Post.API.csproj"
COPY . .
WORKDIR "/src/src/API/Services/Post/Post.API"
RUN dotnet build "Post.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Post.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Post.API.dll"]