# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /src

# RUN dotnet tool install --global dotnet-ef
# ENV PATH="$PATH:/root/.dotnet/tools"

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "ResearchesUFU.API.dll"]
