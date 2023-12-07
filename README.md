## Weather API

#### Intro
This project is Java Spring Boot based project which uses [Open Weather Map](https://openweathermap.org/api/one-call-api) to create an API which tells us the weather.

### Endpoints 
This project contains two different endpoints which are available. Both of these endpoints are GET endpoints. They are:

http://localhost:5052/api/full-weather

http://localhost:5052/api/brief-weather

### Future Work
There is still some work to do in this project. I would like to implement some caching so that each time the endpoint is called we do not have to call the thrid party API. Potentially, if the calls are withing 15 minutes of each other we could use the last cached data. I also will need to add some more testing for better coverage and some Swagger documentation.
