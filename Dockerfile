#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["car-rental-converter/car-rental-converter.csproj", "car-rental-converter/"]
RUN dotnet restore "car-rental-converter/car-rental-converter.csproj"
COPY . .
WORKDIR "/src/car-rental-converter"
RUN dotnet build "car-rental-converter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "car-rental-converter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "car-rental-converter.dll"]

