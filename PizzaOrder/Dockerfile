FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PizzaOrder/PizzaOrder.csproj", "PizzaOrder/"]
RUN dotnet restore "PizzaOrder/PizzaOrder.csproj"
COPY . .
WORKDIR "/src/PizzaOrder"
RUN dotnet build "PizzaOrder.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PizzaOrder.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PizzaOrder.dll"]