FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Bluegrass/Bluegrass.csproj", "Bluegrass/"]
RUN dotnet restore "Bluegrass/Bluegrass.csproj"
COPY . .
WORKDIR "/src/Bluegrass"
RUN dotnet build "Bluegrass.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bluegrass.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bluegrass.dll"]
