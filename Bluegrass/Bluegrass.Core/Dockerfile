FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Bluegrass.Core/Bluegrass.Core.csproj", "Bluegrass.Core/"]
RUN dotnet restore "Bluegrass.Core/Bluegrass.Core.csproj"
COPY . .
WORKDIR "/src/Bluegrass.Core"
RUN dotnet build "Bluegrass.Core.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bluegrass.Core.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bluegrass.Core.dll"]
