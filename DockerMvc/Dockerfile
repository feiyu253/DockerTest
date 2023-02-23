#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 14000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DockerMvc/DockerMvc.csproj", "DockerMvc/"]
COPY ["DockerDataModel/DockerDataModel.csproj", "DockerDataModel/"]
RUN dotnet restore "DockerMvc/DockerMvc.csproj"
COPY . .
WORKDIR "/src/DockerMvc"
RUN dotnet build "DockerMvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DockerMvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerMvc.dll"]