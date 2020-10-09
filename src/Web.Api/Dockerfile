FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src/Web.Api
COPY ["Web.Api.csproj", "src/Web.Api/"]
RUN dotnet restore "src/Web.Api/Web.Api.csproj"
COPY . .
WORKDIR /src/Web.Api
RUN dotnet build "Web.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Api.dll"]
