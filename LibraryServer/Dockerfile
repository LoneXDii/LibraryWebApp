FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY ["LibraryServer.sln", "./"]
COPY ["LibraryServer.API/LibraryServer.API.csproj", "LibraryServer.API/"]
COPY ["LibraryServer.Application/LibraryServer.Application.csproj", "LibraryServer.Application/"]
COPY ["LibraryServer.DataAccess/LibraryServer.DataAccess.csproj", "LibraryServer.DataAccess/"]
COPY ["LibraryServer.Domain/LibraryServer.Domain.csproj", "LibraryServer.Domain/"]

RUN dotnet restore "./LibraryServer.API/LibraryServer.API.csproj"

COPY . ./LibraryServer/
WORKDIR /App/LibraryServer
RUN dotnet publish ./LibraryServer.API/LibraryServer.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App/LibraryServer
COPY --from=build-env /App/LibraryServer/out .
EXPOSE 7001
COPY mycert.pfx /https/mycert.pfx 
ENTRYPOINT ["dotnet", "LibraryServer.API.dll"]