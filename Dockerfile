 

mcr.microsoft.com/dotnet/aspnet:10.0 AS build

WORKDIR /src

 
COPY src/ECommerce.Api/*.csproj src/ECommerce.Api/
COPY src/ECommerce.Application/*.csproj src/ECommerce.Application/
COPY src/ECommerce.Domain/*.csproj src/ECommerce.Domain/
COPY src/ECommerce.Infrastructure/*.csproj src/ECommerce.Infrastructure/
COPY src/ECommerce.Shared/*.csproj src/ECommerce.Shared/

 
RUN dotnet restore src/ECommerce.Api/ECommerce.Api.csproj

 
COPY . .


RUN dotnet publish src/ECommerce.Api/ECommerce.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore
	
mcr.microsoft.com/dotnet/sdk:10.0

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "ECommerce.Api.dll"]
