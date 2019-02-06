From microsoft/aspnetcore-build:2.0.0 AS build

WORKDIR /code

COPY . .

RUN dotnet restore

RUN dotnet publish --output /out/ --configuration Release 

FROM microsoft/aspnetcore:2.0.0

copy --from=build /out /app/

WORKDIR /app

ENTRYPOINT ["dotnet","Products.dll"]
