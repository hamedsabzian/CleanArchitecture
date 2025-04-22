FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Todo.Api/Todo.Api.csproj", "Todo.Api/"]
COPY ["Todo.Application/Todo.Application.csproj", "Todo.Application/"]
COPY ["Todo.Application.Abstraction/Todo.Application.Abstraction.csproj", "Todo.Application.Abstraction/"]
COPY ["Todo.Domain/Todo.Domain.csproj", "Todo.Domain/"]
COPY ["Todo.Infrastructure/Todo.Infrastructure.csproj", "Todo.Infrastructure/"]
RUN dotnet restore "Todo.Api/Todo.Api.csproj"
COPY . .
WORKDIR "/src/Todo.Api"
RUN dotnet build "Todo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Todo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todo.Api.dll"]
