@LoginCSH
Feature: Login EMS Application
@UserLogin
Scenario: Verify user logs in successfully and logsout
	Given user lands on EMS application
	And the user enters valid login credentials
	And the user clicks the Sign In button
	When Verify the page title is correct
	#Then User logout the application