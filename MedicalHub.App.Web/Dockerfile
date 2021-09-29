#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MedicalHub.App.Web/MedicalHub.App.Web.csproj", "MedicalHub.App.Web/"]
RUN dotnet restore "MedicalHub.App.Web/MedicalHub.App.Web.csproj"
COPY . .
WORKDIR "/src/MedicalHub.App.Web"
RUN dotnet build "MedicalHub.App.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MedicalHub.App.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MedicalHub.App.Web.dll"]