FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build


# WORKDIR "/Bluegrass"
# COPY ["/Bluegrass", "Bluegrass/"]

# WORKDIR "/Bluegrass.Core"
# COPY ["/Bluegrass.Core", "Bluegrass/"]

# WORKDIR "/Bluegrass.Data"
# COPY ["/Bluegrass.Data", "Bluegrass/"]

# WORKDIR "/Bluegrass.Shared"
# COPY ["/Bluegrass.Shared", "Bluegrass/"]

COPY [".", "Bluegrass/"]

WORKDIR "."
COPY ["Bluegrass.sln", "Bluegrass/"]
RUN dotnet restore "/Bluegrass/Bluegrass.sln"
COPY . .
# WORKDIR "/Bluegrass"
RUN dotnet build "/Bluegrass/Bluegrass.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/Bluegrass/Bluegrass.sln" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bluegrass.dll"]
