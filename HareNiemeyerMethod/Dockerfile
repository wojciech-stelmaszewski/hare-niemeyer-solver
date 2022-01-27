FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HareNiemeyerMethod/HareNiemeyerMethod.csproj", "HareNiemeyerMethod/"]
RUN dotnet restore "HareNiemeyerMethod/HareNiemeyerMethod.csproj"
COPY . .
WORKDIR "/src/HareNiemeyerMethod"
RUN dotnet build "HareNiemeyerMethod.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HareNiemeyerMethod.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HareNiemeyerMethod.dll"]
