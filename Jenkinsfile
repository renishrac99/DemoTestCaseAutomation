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

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --configuration Release --no-restore'
            }
        }

        stage('Test') {
            steps {
                bat 'dotnet test --configuration Release --no-build'
            }
        }

        stage('Publish') {
            steps {
                bat 'dotnet publish --configuration Release -o publish'
            }
        }

        stage('Docker Build') {
            steps {
                bat 'docker build -t demotestcaseautomation .'
            }
        }

        stage('Deploy') {
            steps {
                bat '''
                docker stop demotestcaseautomation || exit 0
                docker rm demotestcaseautomation || exit 0
                docker run -d ^
                    --name demotestcaseautomation ^
                    -p 5000:80 ^
                    demotestcaseautomation
                '''
            }
        }
    }

    post {
        success {
            echo 'Deployment Successful'
        }

        failure {
            echo 'Deployment Failed'
        }
    }
}