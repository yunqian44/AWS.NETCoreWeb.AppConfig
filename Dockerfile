#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 9020

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AWS.NETCoreWeb.AppConfig/AWS.NETCoreWeb.AppConfig.csproj", "AWS.NETCoreWeb.AppConfig/"]
RUN dotnet restore "AWS.NETCoreWeb.AppConfig/AWS.NETCoreWeb.AppConfig.csproj"
COPY . .
WORKDIR "/src/AWS.NETCoreWeb.AppConfig"
RUN dotnet build "AWS.NETCoreWeb.AppConfig.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AWS.NETCoreWeb.AppConfig.csproj" -c Release --self-contained --runtime linux-x64 -o /app/publish  

# --runtime alpine-x64 --self-contained true

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AWS.NETCoreWeb.AppConfig.dll"]