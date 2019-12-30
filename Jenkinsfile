pipeline {
  agent any
  environment {
    MASTER_VER  = '2.1.1'
    RELEASE_VER = '2.1.0'
  }
  stages {
    stage('Build Debug') {
      when { not { anyOf { branch 'master'; branch 'release' } } }
      steps {
        bat 'for /f %%i in ('git rev-parse HEAD') do set GIT_HASH=%%i'
        bat 'echo %GIT_HASH%'
        bat 'dotnet clean --configuration Debug'
        bat 'dotnet build --configuration Debug'
      }
    }
    stage('Build Release') {
      when { anyOf { branch 'master'; branch 'release' } }
      steps {
        bat 'dotnet clean --configuration Release'
        bat 'dotnet build --configuration Release'
      }
    }
    stage('Backup') {
      when { anyOf { branch 'master'; branch 'release' } }
      steps {
        bat '''move /Y nupkgs\\*.nupkg "t:\\Nuget Packages"
        exit 0'''
      }
    }
    stage('Pack Master') {
      when { branch 'master' }
      steps {
        script {
          env.JDATE = new Date().format("yyDDD.HHmm")
        }
        bat 'dotnet pack --no-build --no-restore --configuration Release -p:PackageVersion="%MASTER_VER%-beta.%JDATE%+%GIT_COMMIT%" -p:Version="%MASTER_VER%-beta.%JDATE%+%GIT_COMMIT%" --output nupkgs'
      }
    }
    stage('Pack Release') {
      when { branch 'release' }
      steps {
        bat 'dotnet pack --no-build --no-restore --configuration Release -p:PackageVersion="%RELEASE_VER%+%GIT_COMMIT%" -p:Version="%RELEASE_VER%+%GIT_COMMIT%" --output nupkgs'
      }
    }
    stage('Publish') {
      when { anyOf { branch 'master'; branch 'release' } }
      environment {
        NUGET_API_KEY = credentials('nuget-api-key')
      }
      steps {
        bat 'dotnet nuget push **\\nupkgs\\*.nupkg -k %NUGET_API_KEY% -s https://api.nuget.org/v3/index.json --no-symbols true'
      }
    }
  }
}
