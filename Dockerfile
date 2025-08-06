# Use official .NET SDK image to build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

COPY Src/LibraryService/LibraryService/LibraryService.csproj ./Src/LibraryService/
RUN dotnet restore ./Src/LibraryService/LibraryService.csproj

COPY Src/LibraryService/LibraryService/ ./Src/LibraryService/

RUN dotnet publish ./Src/LibraryService/LibraryService.csproj -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out ./

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "LibraryService.dll"]