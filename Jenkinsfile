pipeline {

    agent any

    environment {
        IMAGE_NAME = "demotestcaseautomation"
        CONTAINER_NAME = "demotestcaseautomation"
    }

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Check Docker') {
            steps {
                sh '''
                    docker --version
                '''
            }
        }

        stage('Restore') {
            steps {
                sh '''
                    docker run --rm \
                    -v "$PWD:/src" \
                    -w /src \
                    mcr.microsoft.com/dotnet/sdk:9.0 \
                    dotnet restore
                '''
            }
        }

        stage('Build') {
            steps {
                sh '''
                    docker run --rm \
                    -v "$PWD:/src" \
                    -w /src \
                    mcr.microsoft.com/dotnet/sdk:9.0 \
                    dotnet build --configuration Release --no-restore
                '''
            }
        }

        stage('Test') {
            steps {
                sh '''
                    docker run --rm \
                    -v "$PWD:/src" \
                    -w /src \
                    mcr.microsoft.com/dotnet/sdk:9.0 \
                    dotnet test --configuration Release --no-build
                '''
            }
        }

        stage('Publish') {
            steps {
                sh '''
                    docker run --rm \
                    -v "$PWD:/src" \
                    -w /src \
                    mcr.microsoft.com/dotnet/sdk:9.0 \
                    dotnet publish \
                    --configuration Release \
                    -o publish
                '''
            }
        }


        stage('Docker Build') {
            steps {
                sh '''
                    docker build \
                    -t ${IMAGE_NAME}:latest .
                '''
            }
        }


        stage('Deploy') {
            steps {
                sh '''
                    docker stop ${CONTAINER_NAME} || true
                    docker rm ${CONTAINER_NAME} || true


                    docker run -d \
                    --name ${CONTAINER_NAME} \
                    -p 5000:8080 \
                    ${IMAGE_NAME}:latest
                '''
            }
        }
    }


    post {

        success {
            echo '✅ Deployment Successful'
        }

        failure {
            echo '❌ Deployment Failed'
        }

        always {
            cleanWs()
        }
    }
}