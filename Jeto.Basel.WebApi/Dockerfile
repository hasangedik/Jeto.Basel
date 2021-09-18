FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Jeto.Basel.WebApi/Jeto.Basel.WebApi.csproj", "Jeto.Basel.WebApi/"]
RUN dotnet restore "Jeto.Basel.WebApi/Jeto.Basel.WebApi.csproj"
COPY . .
WORKDIR "/src/Jeto.Basel.WebApi"
RUN dotnet build "Jeto.Basel.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Jeto.Basel.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Jeto.Basel.WebApi.dll"]
