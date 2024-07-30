# Use uma imagem do .NET SDK como base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 8080

ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0
ENV DOTNET_SYSTEM_NET_SECURITY_ENABLELEGACYTLS=1
ENV DOTNET_SYSTEM_NET_SECURITY_SSLPROTOCOLS=Tls12

# Use uma imagem do .NET SDK como build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo csproj e restaura as dependências
COPY ["ApiOfficecom.csproj", "./"]
RUN dotnet restore "ApiOfficecom.csproj"

# Copia todo o restante do código
COPY . .

# Define o diretório de trabalho para o build
WORKDIR "/src"
RUN dotnet build "ApiOfficecom.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApiOfficecom.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiOfficecom.dll"]
