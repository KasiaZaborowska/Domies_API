FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
 WORKDIR /app
 EXPOSE 7098
 FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
 WORKDIR /src
 COPY ["DomiesAPI.csproj", ""]
 RUN dotnet restore "./DomiesAPI.csproj"
 COPY . .
 WORKDIR "/src/."
 RUN dotnet build "DomiesAPI.csproj" -c Release -o /app/build
 FROM build AS publish
 RUN dotnet publish "DomiesAPI.csproj" -c Release -o /app/publish
 FROM base AS final
 WORKDIR /app
 COPY --from=publish /app/publish .
 ENTRYPOINT ["dotnet", "DomiesAPI.dll"]