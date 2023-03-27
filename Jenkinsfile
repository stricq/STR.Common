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
        dotnetClean(sdk: '.Net 7', configuration: 'Debug')
        dotnetBuild(sdk: '.Net 7', configuration: 'Debug', noRestore: true)

        dotnetTest(sdk: '.Net 7', configuration: 'Debug', noBuild: true, filter: 'TestCategory=Unit')
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

        dotnetClean(sdk: '.Net 7', configuration: 'Release')
        dotnetBuild(sdk: '.Net 7', configuration: 'Release', noRestore: true, optionsString: "-p:Version=${env.BRANCH_VERSION} -p:PublishRepositoryUrl=true")

//      sh 'dotnet clean --configuration Release'
//      sh 'dotnet build --configuration Release --no-restore -p:Version="$env:BRANCH_VERSION" -p:PublishRepositoryUrl=true'
      }
    }
    stage('Package') {
      when { anyOf { branch 'prerelease*'; branch 'release*' } }
      steps {
        sh "rm -rf '${env:WORKSPACE}/nuget'"

        dotnetPack(sdk: '.Net 7', configuration: 'Release', noBuild: true, includeSymbols: true, optionsString: "-p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg -p:PackageVersion='${env:BRANCH_VERSION}'", outputDirectory: "${env.WORKSPACE}/nuget")

//      sh "dotnet pack --configuration Release --no-build --include-symbols -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg -p:PackageVersion='${env:BRANCH_VERSION}' --output '${env:WORKSPACE}/nuget'"
      }
    }
    stage('Publish') {
      when { anyOf { branch 'prerelease*'; branch 'release*' } }
      environment {
        NUGET_API_KEY = credentials('nuget-api-key')
      }
      steps {
        dotnetNugetPush(sdk: '.Net 7', root: "${env:WORKSPACE}/nuget/*.nupkg", apiKeyId: "${env:NUGET_API_KEY}", source: 'https://api.nuget.org/v3/index.json')

//      sh "dotnet nuget push '${env:WORKSPACE}/nuget/*.nupkg' -k '${env:NUGET_API_KEY}' -s https://api.nuget.org/v3/index.json"
      }
    }
  }
}
