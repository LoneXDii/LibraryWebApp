FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

COPY ["LibraryIdentityServer.sln", "./"]
COPY ["LibraryIdentityServer.API/LibraryIdentityServer.API.csproj", "LibraryIdentityServer.API/"]
COPY ["LibraryIdentityServer.Application/LibraryIdentityServer.Application.csproj", "LibraryIdentityServer.Application/"]
COPY ["LibraryIdentityServer.DataAccess/LibraryIdentityServer.DataAccess.csproj", "LibraryIdentityServer.DataAccess/"]
COPY ["LibraryIdentityServer.Domain/LibraryIdentityServer.Domain.csproj", "LibraryIdentityServer.Domain/"]

RUN dotnet restore "./LibraryIdentityServer.API/LibraryIdentityServer.API.csproj"

COPY . ./LibraryIdentityServer/
WORKDIR /App/LibraryIdentityServer
RUN dotnet publish ./LibraryIdentityServer.API/LibraryIdentityServer.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App/LibraryIdentityServer
RUN apt-get update 
RUN apt-get --yes install curl
COPY --from=build-env /App/LibraryIdentityServer/out .
EXPOSE 7002
COPY mycert.pfx /https/mycert.pfx 
ENTRYPOINT ["dotnet", "LibraryIdentityServer.API.dll"]