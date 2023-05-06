# Cro Weather

## Important notes regarding the database.
Connection string is defined in SQLExpressInfo. Logging to server as user 'admin' that has dbcreator role.
Database is created automatically if it doesn't exist.

## CroWeatherUpdateService 
Service can be installed with admin developer console opened from visual studio.
It can be started manually. Service project can also be tested in console with argument --test that simulates one api call and saving of weather data (if installed service is not running)
Data is fetched and saved every 8 minutes. OpenWeatherMap updates data with variable frequency depending on time of the day, smetimes every 10 minutes, later in the evening every 1 hour or so from my testing experience.

## CroWeatherApi
The cache is deleted after 5 minutes. I had problems with SqlDependency notifications implementation so I couldn't improve it even more.
