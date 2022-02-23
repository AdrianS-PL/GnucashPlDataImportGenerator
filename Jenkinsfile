//Multibranch pipeline
pipeline {
	agent { label 'windows' }
	environment {
        ConnectionStrings = ''	
    }
	stages {
		stage('PR') {
			when { changeRequest() }
			environment {
				ConnectionStrings = ''	
			}
			stages {
				stage('Build PR') {
					steps {
						sh 'dotnet restore'
						sh 'dotnet build'
						recordIssues(tools: [msBuild()])
					}
				}
				stage('Test PR') {
					steps {
						sh 'dotnet test -v normal -l:trx\\;LogFileName=TestOutput.trx --filter /p:CollectCoverage=true /p:CoverletOutput=CodeCoverageResults/ /p:CoverletOutputFormat=cobertura || true'
						mstest testResultsFile:"**/*.trx", keepLongStdio: true
						step([$class: 'CoberturaPublisher', autoUpdateHealth: false, autoUpdateStability: false, coberturaReportFile: '**/coverage.cobertura.xml', failUnhealthy: false, failUnstable: false, maxNumberOfBuilds: 0, onlyStable: false, sourceEncoding: 'UTF_8', zoomCoverageChart: false])				
					}
				}
			}
		}
		stage('Create artifacts') {
			when { branch 'master' }
			stages {
				stage('Build solution') {
					steps {
						sh 'dotnet restore'
						sh 'dotnet build -c Release'
						recordIssues(tools: [msBuild()])
					}
				}
				stage('Test solution') {
					steps {
						sh 'dotnet test -v normal -l:trx\\;LogFileName=TestOutput.trx --filter /p:CollectCoverage=true /p:CoverletOutput=CodeCoverageResults/ /p:CoverletOutputFormat=cobertura || true'
						mstest testResultsFile:"**/*.trx", keepLongStdio: true
						step([$class: 'CoberturaPublisher', autoUpdateHealth: false, autoUpdateStability: false, coberturaReportFile: '**/coverage.cobertura.xml', failUnhealthy: false, failUnstable: false, maxNumberOfBuilds: 0, onlyStable: false, sourceEncoding: 'UTF_8', zoomCoverageChart: false])				
					}
				}
				stage('Build artifacts') {
					steps {
						sh 'dotnet publish GnucashPlDataImportGeneratorApp -c Release -o Artifacts'
						archiveArtifacts artifacts: '**/Artifacts/**/*', followSymlinks: false, onlyIfSuccessful: true
					}
				}
			}
		}
	}
}
