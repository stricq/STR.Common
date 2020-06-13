pipeline {
  agent any
  environment {
    NEXT_VER = '3.1.0'

    GIT_HASH = GIT_COMMIT.take(7)
  }
  stages {
  	stage('Environment') {
  		steps {
        script {
          env.JDATE = new Date().format("yyDDDHHmm")
        }
  		}
  	}
    stage('Build Debug') {
      when { not { branch 'release' } }
      steps {
        bat 'dotnet clean --configuration Debug'
        bat 'dotnet build --configuration Debug -p:Version="%NEXT_VER%-%GIT_BRANCH%.%JDATE%+%GIT_HASH%" -p:PublishRepositoryUrl=true'
      }
    }
    stage('Build Release') {
      when { branch 'release' }
      steps {
        bat 'dotnet clean --configuration Release'
        bat 'dotnet build --configuration Release -p:Version="%NEXT_VER%+%GIT_HASH%"'
      }
    }
    stage('Backup') {
      when { not { anyOf { branch 'release'; branch 'master'; branch 'PR*' } } }
      steps {
        bat '''move /Y nupkgs\\* "t:\\Nuget Packages"
        exit 0'''
      }
    }
    stage('Pack Debug') {
      when { not { anyOf { branch 'release'; branch 'master'; branch 'PR*' } } }
      steps {
        bat 'dotnet pack --configuration Debug --no-build --include-symbols -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg -p:PackageVersion="%NEXT_VER%-%GIT_BRANCH%.%JDATE%+%GIT_HASH%" --output nupkgs'
      }
    }
    stage('Pack Release') {
      when { branch 'release' }
      steps {
        bat 'dotnet pack --configuration Release --no-build --include-symbols -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg -p:PackageVersion="%NEXT_VER%+%GIT_HASH%" --output nupkgs'
      }
    }
    stage('Publish') {
      when { not { anyOf { branch 'master'; branch 'PR*' } } }
      environment {
        NUGET_API_KEY = credentials('nuget-api-key')
      }
      steps {
        bat 'dotnet nuget push **\\nupkgs\\*.nupkg -k %NUGET_API_KEY% -s https://api.nuget.org/v3/index.json'
      }
    }
  }
}
