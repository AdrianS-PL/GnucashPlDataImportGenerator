//Multibranch pipeline
pipeline {
	agent { label 'windows' }
	environment {
        ConnectionStrings = ''	
    }
	stages {		
		stage('Create artifacts') {
			when { branch 'master' }
			stages {
				stage('Build solution') {
					steps {
						bat 'dotnet restore'
						bat 'dotnet build -c Release'
						recordIssues(tools: [msBuild()])
					}
				}
			}
		}
	}
}