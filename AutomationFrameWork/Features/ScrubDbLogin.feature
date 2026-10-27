@LoginCSH
Feature:Login ScruDB
@UserLogin
Scenario: Verify user logs in successfully and logsout
	Given user lands on ScrubDb application
	When Verify the page title is correct
	Then User logout the application