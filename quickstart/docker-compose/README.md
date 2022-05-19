# Quickstart
This directory contains a docker-compose setup that includes the services with all their supporting infrastructure and configuration to allow the Directory API services to be worked with locally.

## Quickstart Requirements
- Docker
- Docker-Compose
- JQ

## Installing Docker

### Mac

### Windows

## Installing Docker-Compose

### Mac

### Windows

## Installing JQ

### Mac
First, make sure HomeBrew is installed.
Then: 
```
brew install jq
```

### Windows

## Starting the Services
After cloning the repository:
```
git clone git@github.com
cd bluegrass
```

Change to the quickstart/docker-compose directory:
```
cd quickstart/docker-compose
```

Start the local docker-compose based environment:
```
./run-quickstart.sh
```

This will start the Bluegrass directory service

|Locally Mappped port | Service |
|----------|-------|
|localhost:7195| Bluegrass People Directory |

