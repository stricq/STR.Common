pipeline {
  agent any
  options {
    buildDiscarder logRotator(artifactNumToKeepStr: '15', numToKeepStr: '15')

    disableConcurrentBuilds()
  }
  environment {
    GIT_HASH = GIT_COMMIT.take(7)

    JDATE = new Date().format("yyDDDHHmm", TimeZone.getTimeZone('America/Denver'))

    BRANCH_VERSION = ''
  }
  stages {
    stage('Restore') {
      steps {
        powershell 'dotnet restore -s "https://api.nuget.org/v3/index.json"'
      }
    }
    stage('Unit Test') {
      steps {
        powershell 'dotnet clean --configuration Debug'
        powershell 'dotnet build --configuration Debug --no-restore'

        powershell 'dotnet test --configuration Debug --no-build --filter TestCategory=Unit'
      }
    }
    stage('Build') {
      when { anyOf { branch 'prerelease*'; branch 'release*' } }
      steps {
        script {
          def values = BRANCH_NAME.split('_')

          BRANCH  = values[0]
          VERSION = values[1]

          if (BRANCH == 'release') {
            BRANCH_VERSION = "%VERSION%+%GIT_HASH%"
          }
          else {
            BRANCH_VERSION = "%VERSION%-pre.%JDATE%+%GIT_HASH%"
          }
        }

        powershell 'dotnet clean --configuration Release'
        powershell 'dotnet build --configuration Release --no-restore -p:Version="%BRANCH_VERSION%" -p:PublishRepositoryUrl=true'
      }
    }
    stage('Package') {
      when { anyOf { branch 'prerelease*'; branch 'release*' } }
      steps {
        powershell 'Remove-Item -Recurse -Force "%WORKSPACE%\nuget"'

        powershell 'dotnet pack --configuration Release --no-build --include-symbols -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg -p:PackageVersion="%BRANCH_VERSION%" --output "%WORKSPACE%\nuget"'
      }
    }
    stage('Publish') {
      when { anyOf { branch 'prerelease*'; branch 'release*' } }
      environment {
        NUGET_API_KEY = credentials('nuget-api-key')
      }
//    steps {
//      powershell 'dotnet nuget push **\\nupkgs\\*.nupkg -k %NUGET_API_KEY% -s https://api.nuget.org/v3/index.json'
//    }
    }
  }
}
