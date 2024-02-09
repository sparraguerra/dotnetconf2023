#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/nightly/sdk:8.0-cbl-mariner2.0-aot AS build
 
ARG BUILD_CONFIGURATION=Release
ARG BUILD_PLATFORM=linux-x64

WORKDIR /src
COPY ["src/webapiaot/DotNetConf2023AOT/nuget.config", "src/webapiaot/DotNetConf2023AOT/"]
COPY ["src/webapiaot/DotNetConf2023AOT/DotNetConf2023AOT.csproj", "src/webapiaot/DotNetConf2023AOT/"]
RUN dotnet restore "./src/webapiaot/DotNetConf2023AOT/./DotNetConf2023AOT.csproj" -r $BUILD_PLATFORM

COPY . .
WORKDIR "/src/src/webapiaot/DotNetConf2023AOT"
RUN dotnet publish  "./DotNetConf2023AOT.csproj" --no-restore -c $BUILD_CONFIGURATION -r $BUILD_PLATFORM -o /app/publish

FROM mcr.microsoft.com/dotnet/nightly/runtime-deps:8.0-cbl-mariner2.0-distroless-aot AS final
WORKDIR /app
ENV ASPNETCORE_URLS=http://*:80/;http://*:8080/
EXPOSE 8080
COPY --from=build /app/publish .
USER $APP_UID
ENTRYPOINT ["./DotNetConf2023AOT"]