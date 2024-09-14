#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PoC.Payments.API/PoC.Payments.API.csproj", "PoC.Payments.API/"]
RUN dotnet restore "PoC.Payments.API/PoC.Payments.API.csproj"
COPY . .
WORKDIR "/src/PoC.Payments.API"
RUN dotnet build "PoC.Payments.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PoC.Payments.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PoC.Payments.API.dll"]