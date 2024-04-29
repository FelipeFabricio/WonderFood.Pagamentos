#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Wonderfood.Worker/Wonderfood.Worker.csproj", "src/Wonderfood.Worker/"]
COPY ["src/Wonderfood.Core/Wonderfood.Core.csproj", "src/Wonderfood.Core/"]
COPY ["src/Wonderfood.Repository/Wonderfood.Repository.csproj", "src/Wonderfood.Repository/"]
COPY ["src/Wonderfood.Service/Wonderfood.Service.csproj", "src/Wonderfood.Service/"]
RUN dotnet restore "src/Wonderfood.Worker/Wonderfood.Worker.csproj"
COPY . .
WORKDIR "/src/src/Wonderfood.Worker"
RUN dotnet build "Wonderfood.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wonderfood.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wonderfood.Worker.dll"]