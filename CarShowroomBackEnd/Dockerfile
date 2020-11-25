FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /src

COPY CarShowroomApp.sln ./
COPY CarShowroom.Application/*.csproj ./CarShowroom.Application/
COPY CarShowroom.Domain/*.csproj ./CarShowroom.Domain/
COPY CarShowroom.Infra.Data/*.csproj ./CarShowroom.Infra.Data/
COPY CarShowroom.Infra.IoC/*.csproj ./CarShowroom.Infra.IoC/
COPY CarShowroomApp.UI/*.csproj ./CarShowroomApp.UI/
COPY CarShowroomApp.Tests/*.csproj ./CarShowroomApp.Tests/
COPY CarShowroom.Domain/Logging/*.config ./app

RUN dotnet restore
COPY . .
WORKDIR /src/CarShowroom.Domain
RUN dotnet build -c Release -o /app

WORKDIR /src/CarShowroom.Infra.Data
RUN dotnet build -c Release -o /app

WORKDIR /src/CarShowroom.Infra.IoC
RUN dotnet build -c Release -o /app

WORKDIR /src/CarShowroom.Application
RUN dotnet build -c Release -o /app

WORKDIR /src/CarShowroomApp.UI
RUN dotnet build -c Release -o /app

WORKDIR /src/CarShowroomApp.Tests
RUN dotnet build -c Release -o /app

FROM build-env AS publish
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CarShowroom.UI.dll"]