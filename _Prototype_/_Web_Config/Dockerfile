FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["_Web_Config/_Web_Config.csproj", "_Web_Config/"]

RUN dotnet restore "_Web_Config/_Web_Config.csproj"
COPY . .
WORKDIR "/src/_Web_Config"
RUN dotnet build "_Web_Config.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "_Web_Config.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "_Web_Config.dll"]