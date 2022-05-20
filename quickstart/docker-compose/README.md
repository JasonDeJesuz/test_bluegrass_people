![Bluegrass Logo](/Docs/github_assets/bluegrass_logo.jpeg)

# Quickstart
This directory contains a docker-compose setup that includes the services with all their supporting infrastructure and configuration to allow the Directory API services to be worked with locally.

## Quickstart Requirements
- HomeBrew
- Docker
- Docker-Compose
- JQ
- coreutils 

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

##Installing CoreUtils
```
brew install coreutils
```

## Cloning the repository
```
https://github.com/JasonDeJesuz/test_bluegrass_people
```

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
### Windows
```
./run-quickstart.sh
```

### Mac
```
sudo sh run-quickstart.sh
```

This will start the Bluegrass directory service

|Locally Mappped port | Service |
|----------|-------|
|localhost:7195| Bluegrass People Directory |

---

<a href="https://guywithagopro.com"><img src="../../Docs/github_assets/j_logo.png" alt="jason logo" width=200 /></a>