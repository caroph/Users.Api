FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Users.Api/Users.Api.csproj", "Users.Api/"]
COPY ["Users.Domain/Users.Domain.csproj", "Users.Domain/"]
COPY ["Users.Infra/Users.Infra.csproj", "Users.Infra/"]
COPY ["Users.Data/Users.Data.csproj", "Users.Data/"]
RUN dotnet restore "Users.Api/Users.Api.csproj"
COPY . .
WORKDIR "/src/Users.Api"
RUN dotnet build "Users.Api.csproj" -c Release -o /app
RUN dotnet publish "Users.Api.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim
ENV TZ=America/Sao_Paulo
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
EXPOSE 80
EXPOSE 443
ENV APPSETTINGS__POSTGRESCONNECTION="User ID=root;Password=userpass;Host=127.0.0.1;Port=5432;Database=users;Pooling=true;"
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Users.Api.dll"]