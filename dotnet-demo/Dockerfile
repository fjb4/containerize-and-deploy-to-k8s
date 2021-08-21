# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY ./*.csproj ./dotnet-demo/
RUN dotnet restore ./dotnet-demo

# copy everything else and build app
COPY . ./dotnet-demo/
WORKDIR /source/dotnet-demo
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "dotnet-demo.dll"]
