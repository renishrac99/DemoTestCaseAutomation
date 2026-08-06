# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy the entire solution
COPY . .

# Restore API project (this restores referenced projects too)
RUN dotnet restore DemoTestCaseAutomation.Api/DemoTestCaseAutomation.Api.csproj

# Publish API
RUN dotnet publish DemoTestCaseAutomation.Api/DemoTestCaseAutomation.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet","DemoTestCaseAutomation.Api.dll"]