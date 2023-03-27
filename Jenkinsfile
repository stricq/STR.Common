pipeline {
  agent any
  options {
    buildDiscarder logRotator(artifactNumToKeepStr: '15', numToKeepStr: '15')

    disableConcurrentBuilds()
  }
  environment {
    GIT_HASH = GIT_COMMIT.take(7)

    JDATE = new Date().format("yyDDDHHmm", TimeZone.getTimeZone('America/Denver'))
  }
  stages {
    stage('Restore') {
      steps {
        dotnetRestore(sdk: '.Net 7', source: 'https://api.nuget.org/v3/index.json')
      }
    }
    stage('Unit Test') {
      steps {
        dotnetClean(configuration: 'Debug')
        dotnetBuild(configuration: 'Debug', noRestore: true)

        dotnetTest(configuration: 'Debug', noBuild: true, filter: 'TestCategory=Unit')

//      sh 'dotnet clean --configuration Debug'
//      sh 'dotnet build --configuration Debug --no-restore'

//      sh 'dotnet test --configuration Debug --no-build --filter TestCategory=Unit'
      }
    }
    stage('Build') {
      when { anyOf { branch 'prerelease*'; branch 'release*' } }
      steps {
        script {
          def values = env.BRANCH_NAME.split('_')

          env.BRANCH  = values[0]
          env.VERSION = values[1]

          if (env.BRANCH == 'release') {
            env.BRANCH_VERSION = "${env.VERSION}+${env.GIT_HASH}"
          }
          else {
            env.BRANCH_VERSION = "${env.VERSION}-pre.${env.JDATE}+${env.GIT_HASH}"
          }
        }

        sh "echo BRANCH_VERSION = ${env:BRANCH_VERSION}"

        dotnetClean(configuration: 'Release')
        dotnetBuild(configuration: 'Release', noRestore: true, version: "${env.BRANCH_VERSION}", publishRepositoryUrl: true)

//      sh 'dotnet clean --configuration Release'
//      sh 'dotnet build --configuration Release --no-restore -p:Version="$env:BRANCH_VERSION" -p:PublishRepositoryUrl=true'
      }
    }
    stage('Package') {
      when { anyOf { branch 'prerelease*'; branch 'release*' } }
      steps {
        sh "rm -rf '${env:WORKSPACE}/nuget'"

        dotnetPack(configuration: 'Release', noBuild: true, includeSymbols: true, symbolPackageFormat: 'snupkg', packageVersion: "${env.BRANCH_VERSION}", output: "${env.WORKSPACE}/nuget")

//      sh "dotnet pack --configuration Release --no-build --include-symbols -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg -p:PackageVersion='${env:BRANCH_VERSION}' --output '${env:WORKSPACE}/nuget'"
      }
    }
    stage('Publish') {
      when { anyOf { branch 'prerelease*'; branch 'release*' } }
      environment {
        NUGET_API_KEY = credentials('nuget-api-key')
      }
      steps {
        dotnetNugetPush(root: "${env:WORKSPACE}/nuget/*.nupkg", apiKeyId: "${env:NUGET_API_KEY}", source: 'https://api.nuget.org/v3/index.json')

//      sh "dotnet nuget push '${env:WORKSPACE}/nuget/*.nupkg' -k '${env:NUGET_API_KEY}' -s https://api.nuget.org/v3/index.json"
      }
    }
  }
  post {
    always {
      cleanWs(cleanWhenSuccess: false)
    }
  }
}
