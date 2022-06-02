#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["spider3auth/spider3auth.csproj", "spider3auth/"]
RUN dotnet restore "spider3auth/spider3auth.csproj"
COPY . .
WORKDIR "/src/spider3auth"
RUN dotnet build "spider3auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "spider3auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "spider3auth.dll"]