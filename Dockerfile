# Stage 1: Build the frontend
FROM node:lts AS node
WORKDIR /frontend
COPY frontend/package.json .
COPY frontend/package-lock.json .
RUN node --version
RUN npm i --legacy-peer-deps --silent
COPY frontend .
RUN npm run build

# Stage 2: Build the backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY backend/src .
RUN ls
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet restore "WebUI/WebUI.csproj"
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish "WebUI/WebUI.csproj" -c Release -o /app/publish --no-restore

# Stage 3: Publish the backend
COPY --from=node /frontend/dist/universys/browser /app/publish/wwwroot

# Stage 4: Final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "UniverSys.WebUI.dll"]
