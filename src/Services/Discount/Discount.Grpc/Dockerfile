﻿FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
#USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Discount/Discount.Grpc/Discount.Grpc.csproj", "Services/Discount/Discount.Grpc/"]
RUN dotnet restore "Services/Discount/Discount.Grpc/Discount.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Discount/Discount.Grpc"
RUN dotnet build "./Discount.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Discount.Grpc.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

RUN mkdir -p /app/datadb && chmod 777 /app/datadb

#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "Discount.Grpc.dll"]

COPY --from=build /src/Services/Discount/Discount.Grpc/entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh

ENTRYPOINT ["/app/entrypoint.sh"]
