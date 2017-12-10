# dockersample

There is an assumption that you have already installed the version of [Dotnet core](https://github.com/dotnet/core/releases/tag/v1.1.5) used by the sample.

## To test:
In the /dockersample/WebApi directory,
 - dotnet xunit

## To build: 
In the /dockersample directory,
- dotnet restore WebApi
- dotnet publish WebApi -c Release -o app
- sudo docker-compose build

## To start: 
In the /dockersample directory, this should start three containers for mongodb, webapi and mongo_seed. mongo_seed will shutdown after filling the database.
- sudo docker-compose up -d

## To run:
The following get requests should work
 - http://localhost:5000/products/price?min=250&max=350
 - http://localhost:5000/products/fantastic
 - http://localhost:5000/products/nonfantastic
 - http://localhost:5000/products/rating?min=3.5&max=5
