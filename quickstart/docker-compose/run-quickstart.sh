#!/bin/bash -i

export TIME_TO_START=90

shopt -s expand_aliases
# exit when any command fails
set -e

#define bold and normal for pretty output
bold=$(tput bold)
normal=$(tput sgr0)

#output in bold
echoBold(){
   echo "${bold}$1${normal}"
}



#helper function to wait for a service to output something
#in its log outputs before a timeout
wait_for_service () {
    SERVICE=$1
    TIMEOUT=$2
    SEARCH_PATTERN=$3

    echo "Giving ${SERVICE} ${TIMEOUT} seconds to start:"
    timeout --signal=SIGINT $TIMEOUT ./wait-for-logline.sh $SERVICE   "${SEARCH_PATTERN}"
    RESULT=$?


    if [ $RESULT != 0 ]; then
        echo "Failed to start service ${SERVICE}"
        echo "Pattern '${SEARCH_PATTERN}' not found in ${TIMEOUT} seconds."
        return 1
    else
        return 0
    fi
}


#helper function to wait for a service to output something
#in its log outputs before a timeout
check_service_result () {
    SERVICE=$1

    echo "Checking ${SERVICE} result:"
    SERVICE_STATUS=$(docker inspect $(docker-compose ps -q ${SERVICE}) | jq '.[0].State.ExitCode')


    if [ $SERVICE_STATUS != 0 ]; then
        echo "Failed to execute service ${SERVICE}"
        echo
        echo "Service Status"
        docker inspect $(docker-compose ps -q ${SERVICE}) | jq '.[0].State'
        echo
        echo "Service Logs"
        docker logs $(docker-compose ps -q ${SERVICE}) 
        return 1
    else
        echo "Service ${SERVICE} ran successfully"
        return 0
    fi
}


service_controlled_start () {
    SERVICE=$1
    TIMEOUT=$2
    SEARCH_PATTERN=$3

    echo 
    echoBold "Starting ${SERVICE}:"
    docker-compose up -d "${SERVICE}"
    if ! wait_for_service "${SERVICE}" "${TIMEOUT}" "${SEARCH_PATTERN}"
    then
        echoBold "${SERVICE} failed to start!"
        return 1
    else
        echoBold "${SERVICE} started."
        echo
        return 0
    fi
}

echo
echoBold "CHECKING DEPENDENCIES"

echo
echo "Using docker-compose to spin up environment"
if ! which docker-compose; then
    echo "ERROR: This quickstart uses docker-compose, but docker-compose is not installed!";
    echo "see: https://docs.docker.com/compose/install/ for installation instructions"
    echo
    exit 1
fi
docker-compose version

# echo
# echo "Using jq to read JSON results"
# if ! which jq; then
#     echo "ERROR: This quickstart uses jq, but jq is not installed!";
#     echo "see: https://stedolan.github.io/jq/download/ for installation instructions"
#     echo "(on ubuntu: sudo apt-get install jq)"
#     echo "(on mac, using HomeBrew: brew install sed)"
#     echo
#     exit 1
# fi
# jq --version

echo
echo "Using sed to edit appsetting templates"
if ! which sed; then
    echo "ERROR: This quickstart uses sed, but sed is not installed!";
    echo "see: https://www.gnu.org/software/sed/"
    echo "(on ubuntu: sudo apt-get install sed)"
    echo
    exit 1
fi
sed --version | head -2

echo 
echoBold "Making sure all images are pulled before we start."
docker-compose pull

echo
echoBold "Making sure the quickstart is stopped."
docker-compose down
echoBold "Deleting files generated from templates."
rm appsettings-templates/appsettings.docker.json || true

echo
echoBold "Bluegrass People Directory:"
echo "Bluegrass People Directory is the core."
echo "Has a database, and the core system."

service_controlled_start 'bluegrass-sql' $TIME_TO_START 'database system is ready to accept connections'
service_controlled_start 'bluegrass' $TIME_TO_START 'Now listening on\:'

# echo
# echoBold "Testing Bluegrass API:"
# echoBold "Fetch PING Results."
# export TOKEN=`curl -s -X GET \
#   https://localhost:8000/api/ping \
#   -d 'jq'
# echo "Done"

echo
echoBold "Forcing a build of the Bluegrass Service:"
docker-compose build bluegrass-app

echo 
echoBold "Service listing:"
docker-compose ps