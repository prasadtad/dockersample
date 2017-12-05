# dockersample

## To test:
In the /dockersample/WebApi directory,
 - dotnet xunit

## To build: 
In the /dockersample directory,
- dotnet publish WebApi -c Release -o app
- sudo docker-compose build

## To start: 
In the /dockersample directory,
- sudo docker-compose up -d
This should start three containers for mongodb, webapi and mongo_seed. mongo_seed will shutdown after filling the database.

## To run:
The following get requests should work
 - http://localhost:5000/products/price?min=250&max=350
 - http://localhost:5000/products/fantastic
 - http://localhost:5000/products/nonfantastic
 - http://localhost:5000/products/rating?min=3.5&max=5
