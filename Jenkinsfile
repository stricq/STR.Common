pipeline {
  agent any
  stages {
    stage('Checkout') {
      steps {
        git credentialsId: 'aef071d1-3896-4ef0-869c-8e107a5707f5', url: 'https://github.com/stricq/STR.Common', branch: 'master'
      }
    }
    stage('Restore Packages') {
      steps {
        bat 'dotnet restore --verbosity n'
      }
    }
    stage('Clean') {
      steps {
        bat 'dotnet clean'
      }
    }
    stage('Build') {
      steps {
        bat 'dotnet build --configuration Debug'
      }
    }
  }
}
