#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["GoogleBooks.Api/GoogleBooks.Api.csproj", "GoogleBooks.Api/"]
COPY ["GoogleBooks.Client/GoogleBooks.Client.csproj", "GoogleBooks.Client/"]
RUN dotnet restore "GoogleBooks.Api/GoogleBooks.Api.csproj"
COPY . .
WORKDIR "/src/GoogleBooks.Api"
RUN dotnet build "GoogleBooks.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoogleBooks.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoogleBooks.Api.dll"]