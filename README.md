# maze
C# WebAPI + React microservice example

### Run instructions

The following commands should set up a running instance of the system, accessible from port 80:

- `cd /path/to/repo`
- `cd MazeAPI`
- `docker build -t mazeapi .`
- `cd ../MazeSolverAPI`
- `docker build -t mazesolverapi .`
- `cd ../maze-client`
- `docker build -t mazeclient .`
- `cd ..`
- `docker stack deploy -c docker-compose.yml maze`
