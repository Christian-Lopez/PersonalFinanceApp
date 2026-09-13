# Use the official .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy the solution file and project files
COPY *.slnx ./
COPY PersonalFinanceApp.Api/*.csproj ./PersonalFinanceApp.Api/
COPY PersonalFinanceApp.Application/*.csproj ./PersonalFinanceApp.Application/
COPY PersonalFinanceApp.Domain/*.csproj ./PersonalFinanceApp.Domain/
COPY PersonalFinanceApp.Infrastructure/*.csproj ./PersonalFinanceApp.Infrastructure/

# Restore dependencies
RUN dotnet restore PersonalFinanceApp.Api/PersonalFinanceApp.Api.csproj

# Copy the rest of the code and build
COPY . ./
WORKDIR /app/PersonalFinanceApp.Api
RUN dotnet publish -c Release -o /app/out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/out .

# Expose port 8080 (Render's default port)
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "PersonalFinanceApp.Api.dll"]
