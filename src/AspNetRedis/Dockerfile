#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["AspNetRedis.csproj", "."]
RUN dotnet restore "./AspNetRedis.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "AspNetRedis.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetRedis.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copia o certificado autoassinado gerado anteriormente
#COPY aspnetapp /app/aspnetapp
#
# Define as vari�veis de ambiente para a conex�o com o Redis
#ENV REDIS_HOST=localhost
#ENV REDIS_PORT=6379

ENTRYPOINT ["dotnet", "AspNetRedis.dll"]