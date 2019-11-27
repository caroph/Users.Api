# Users.Api
Api for users CRUD

## Development
- Run postgres on docker:  
`docker run --name postgres -e POSTGRES_PASSWORD=userpass -e POSTGRES_USER=root -e POSTGRES_DB=users -p 5432:5432 -d postgres`

- Build app on docker  
`docker build -t usersapi .`

- Run the app  
`docker run --name usersapi -e  APPSETTINGS__POSTGRESCONNECTION="User ID=root;Password=userpass;Host={YOURIPADRESS};Port=5432;Database=users;Pooling=true;" -p 5000:80 -d usersapi`

> PS.: Replace {YOURIPADRESS} with your ip address. Does not work with localhost or 127.0.0.1.

- Open on browser  
[click here](http://localhost:5000)

## Testing
- Run command  
`dotnet test`