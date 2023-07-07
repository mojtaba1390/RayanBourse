FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./RayanBourse/RayanBourse.csproj" --disable-parallel
RUN dotnet publish "./RayanBourse/RayanBourse.csproj" -c realease -o /app --no-restore

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal 
WORKDIR /app
COPY --from=build /app ./

EXPOSE 5000

ENTRYPOINT ["dotnet","RayanBourse.dll"]
